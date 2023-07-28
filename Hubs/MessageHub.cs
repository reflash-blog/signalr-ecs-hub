using System.Threading.Tasks;
using Microsoft.AspNetCore.SignalR;
using Microsoft.Extensions.Logging;

namespace SignalRHubECS;

public class MessageHub: Hub 
{
    public async Task ClientConnected(string userId) 
    {
        await Groups.AddToGroupAsync(Context.ConnectionId, userId);
    }

    public async Task BroadcastMessage(string userId, string message) 
    {
        await Clients.Group(userId).SendAsync("NewMessage", message);
    }
}