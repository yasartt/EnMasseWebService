using EnMasseWebService.Models;
using EnMasseWebService.Models.Entities;
using Microsoft.AspNetCore.SignalR;

namespace SignalRChat
{
    public class CafeHub : Hub
    {
        private readonly EnteractDbContext _dbContext;
        private readonly IDictionary<string, CafeUser> _connection;

        public CafeHub(EnteractDbContext dbContext, IDictionary<string, CafeUser> connection)
        {
            _dbContext = dbContext;
            _connection = connection;
        }

        public async Task JoinCafeGroup(CafeUser cafeUser)
        {
            await Groups.AddToGroupAsync(Context.ConnectionId, $"Cafe_{cafeUser.CafeId}");
            await Clients.Group($"Cafe_{cafeUser.CafeId}").SendAsync("ReceiveMessage", "System", $"{Context.ConnectionId} has joined the cafe.");

            _connection[Context.ConnectionId] = cafeUser;
        }

        public override Task OnDisconnectedAsync(Exception? exception)
        {
            if(!_connection.TryGetValue(Context.ConnectionId, out CafeUser cafeUser))
            {
                return base.OnDisconnectedAsync(exception);
            }

            Clients.Group($"Cafe_{cafeUser.CafeId}").SendAsync("ReceiveMessage", "System", $"{Context.ConnectionId} has left the cafe.");

            SendConnectorUser(cafeUser.CafeId);
            return base.OnDisconnectedAsync(exception);
        }

        public async Task SendMessage(string message)
        {
            if(_connection.TryGetValue(Context.ConnectionId, out CafeUser cafeUser))
            {
                await Clients.Group($"Cafe_{cafeUser.CafeId}"!)
                    .SendAsync("ReceiveMessage", cafeUser.UserId, message, DateTime.Now);
            }   
        }

        public Task SendConnectorUser(int cafeId)
        {
            var users = _connection.Values.Where(q => q.CafeId == cafeId).ToList();

            return Clients.Group($"Cafe_{cafeId}").SendAsync("ConnectedUser", users);
        }
    }
}
