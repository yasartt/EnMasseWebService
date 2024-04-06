using EnMasseWebService.Models;
using EnMasseWebService.Models.Entities;
using EnMasseWebService.Services;
using Microsoft.AspNetCore.SignalR;
using Microsoft.EntityFrameworkCore;
using MongoDB.Bson;
using MongoDB.Driver;
using System.Collections.Concurrent;

namespace SignalRChat
{
    public class CafeHub : Hub
    {
        private readonly EnteractDbContext _dbContext;
        private readonly CafeService _cafeService; // Add CafeService dependency
        private readonly ConcurrentDictionary<string, CafeUser> _connection;
        private readonly IMongoCollection<SessionMessage> _sessionMessagesCollection;

        public CafeHub(EnteractDbContext dbContext, ConcurrentDictionary<string, CafeUser> connection, IMongoCollection<SessionMessage> sessionMessagesCollection, CafeService cafeService) // Add CafeService to the constructor
        {
            _dbContext = dbContext;
            _connection = connection;
            _sessionMessagesCollection = sessionMessagesCollection;
            _cafeService = cafeService; // Initialize CafeService
        }

        public async Task JoinCafeGroupAndRetrieveMessages(string cafeId, string userId)
        {
            var success = await _cafeService.AddUserToCafeAsync(Guid.Parse(cafeId), Guid.Parse(userId));
            if (success)
            {
                await Groups.AddToGroupAsync(Context.ConnectionId, $"Cafe_{cafeId}");
                var cafeUser = new CafeUser { CafeId = Guid.Parse(cafeId), UserId = Guid.Parse(userId) };
                _connection[Context.ConnectionId] = cafeUser;

                var filter = Builders<SessionMessage>.Filter.Eq(m => m.CafeId, Guid.Parse(cafeId));
                var allMessages = await _sessionMessagesCollection.Find(filter)
                                                .Sort(Builders<SessionMessage>.Sort.Ascending(m => m.Id))
                                                .ToListAsync();
                await Clients.Caller.SendAsync("ReceiveMessages", allMessages);

                // Assuming allMessages is not empty and has the last message at the end
                if (allMessages.Any())
                {
                    var lastMessageId = allMessages.Last().Id;
                    var lastCollected = new CafeUserLastCollectedMessages
                    {
                        CafeId = Guid.Parse(cafeId),
                        UserId = Guid.Parse(userId),
                        LastCollectedMessageId = lastMessageId
                    };
                    _dbContext.UserLastCollectedMessages.Add(lastCollected);
                    await _dbContext.SaveChangesAsync();
                }
            }
            else
            {
                // Handle failure
            }
        }

        public override Task OnDisconnectedAsync(Exception? exception)
        {
            // Use ConcurrentDictionary's thread-safe methods for removal
            if (_connection.TryRemove(Context.ConnectionId, out CafeUser cafeUser))
            {
                Clients.Group($"Cafe_{cafeUser.CafeId}").SendAsync("ReceiveMessage", "System", $"{Context.ConnectionId} has left the cafe.");
            }

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


        public async Task ReconnectToAllGroupsAndRetrieveNewMessages(string userId)
        {
            // Fetch all groups that the user is a part of
            var userGroups = await _dbContext.CafeUsers
                                             .Where(cu => cu.UserId == Guid.Parse(userId))
                                             .Select(cu => cu.CafeId)
                                             .ToListAsync();

            // Reconnect to each group
            foreach (var cafeId in userGroups)
            {
                await Groups.AddToGroupAsync(Context.ConnectionId, $"Cafe_{cafeId}");
            }

            // Proceed to fetch new messages for each group
            await FetchAndSendNewMessagesForAllGroups(userId, userGroups);
        }

        private async Task FetchAndSendNewMessagesForAllGroups(string userId, List<Guid> cafeIds)
        {
            foreach (var cafeId in cafeIds)
            {
                var lastCollectedMessage = await _dbContext.UserLastCollectedMessages
                                                            .Where(lcm => lcm.CafeId == cafeId && lcm.UserId == Guid.Parse(userId))
                                                            .OrderByDescending(lcm => lcm.Id)
                                                            .FirstOrDefaultAsync();

                ObjectId lastId = lastCollectedMessage != null && ObjectId.TryParse(lastCollectedMessage.LastCollectedMessageId, out var objId)
                                  ? objId
                                  : ObjectId.Empty; // Consider ObjectId.Empty as a way to fetch all messages if no last message was recorded

                var filter = Builders<SessionMessage>.Filter.And(
                                Builders<SessionMessage>.Filter.Eq(m => m.CafeId, cafeId),
                                Builders<SessionMessage>.Filter.Gt(m => m.Id, lastId.ToString()));

                var newMessages = await _sessionMessagesCollection.Find(filter)
                                        .Sort(Builders<SessionMessage>.Sort.Ascending(m => m.Id))
                                        .ToListAsync();

                if (newMessages.Any())
                {
                    // Update the last collected message for the cafe group
                    if (lastCollectedMessage != null)
                    {
                        lastCollectedMessage.LastCollectedMessageId = newMessages.Last().Id;
                        _dbContext.UserLastCollectedMessages.Update(lastCollectedMessage);
                    }
                    else
                    {
                        _dbContext.UserLastCollectedMessages.Add(new CafeUserLastCollectedMessages
                        {
                            CafeId = cafeId,
                            UserId = Guid.Parse(userId),
                            LastCollectedMessageId = newMessages.Last().Id
                        });
                    }
                    await _dbContext.SaveChangesAsync();

                    // Send new messages to the user
                    await Clients.Caller.SendAsync("ReceiveNewMessages", cafeId.ToString(), newMessages);
                }
            }
        }


        public Task SendConnectorUser(String cafeId)
        {
            var users = _connection.Values.Where(q => q.CafeId == Guid.Parse(cafeId)).ToList();

            return Clients.Group($"Cafe_{cafeId}").SendAsync("ConnectedUser", users);
        }
    }
}
