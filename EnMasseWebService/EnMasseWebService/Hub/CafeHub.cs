using EnMasseWebService.Models;
using EnMasseWebService.Models.Entities;
using EnMasseWebService.Services;
using Microsoft.AspNetCore.SignalR;
using MongoDB.Driver;

namespace SignalRChat
{
    public class CafeHub : Hub
    {
        private readonly EnteractDbContext _dbContext;
        private readonly CafeService _cafeService; // Add CafeService dependency
        private readonly IDictionary<string, CafeUser> _connection;
        private readonly IMongoCollection<SessionMessage> _sessionMessagesCollection;

        public CafeHub(EnteractDbContext dbContext, IDictionary<string, CafeUser> connection, IMongoCollection<SessionMessage> sessionMessagesCollection, CafeService cafeService) // Add CafeService to the constructor
        {
            _dbContext = dbContext;
            _connection = connection;
            _sessionMessagesCollection = sessionMessagesCollection;
            _cafeService = cafeService; // Initialize CafeService
        }

        public async Task JoinCafeGroup(String cafeId, String userId) // Adjust parameters as necessary based on your app's logic
        {
            // Call the new method in CafeService to add the user to the cafe
            var success = await _cafeService.AddUserToCafeAsync(Guid.Parse(cafeId), Guid.Parse(userId));

            if (success)
            {
                await Groups.AddToGroupAsync(Context.ConnectionId, $"Cafe_{cafeId}");
                await Clients.Group($"Cafe_{cafeId}").SendAsync("ReceiveMessage", "System", $"{Context.ConnectionId} has joined the cafe.");

                var cafeUser = new CafeUser { CafeId = Guid.Parse(cafeId), UserId = Guid.Parse(userId) };
                _connection[Context.ConnectionId] = cafeUser; // Update connection mapping
            }
            else
            {
                // Handle failure (e.g., send error message to client)
            }
        }

        public override Task OnDisconnectedAsync(Exception? exception)
        {
            if(!_connection.TryGetValue(Context.ConnectionId, out CafeUser cafeUser))
            {
                return base.OnDisconnectedAsync(exception);
            }

            Clients.Group($"Cafe_{cafeUser.CafeId}").SendAsync("ReceiveMessage", "System", $"{Context.ConnectionId} has left the cafe.");

            SendConnectorUser(cafeUser.CafeId.ToString());
            return base.OnDisconnectedAsync(exception);
        }

        public async Task SendMessage(string message)
        {
            if (_connection.TryGetValue(Context.ConnectionId, out CafeUser cafeUser))
            {
                // Create a new SessionMessage entity
                var sessionMessage = new SessionMessage
                {
                    CafeId = cafeUser.CafeId,
                    SenderId = cafeUser.UserId,
                    SentAt = DateTime.Now,
                    Content = message
                };

                // Insert the message into the MongoDB collection
                await _sessionMessagesCollection.InsertOneAsync(sessionMessage);

                // Send the message to the group
                await Clients.Group($"Cafe_{cafeUser.CafeId}")
                    .SendAsync("ReceiveMessage", cafeUser.UserId, message, DateTime.Now);
            }
        }

        public Task SendConnectorUser(String cafeId)
        {
            var users = _connection.Values.Where(q => q.CafeId == Guid.Parse(cafeId)).ToList();

            return Clients.Group($"Cafe_{cafeId}").SendAsync("ConnectedUser", users);
        }
    }
}
