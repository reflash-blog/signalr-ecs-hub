using System.Threading.Tasks;
using Microsoft.AspNetCore.SignalR;
using Microsoft.Extensions.Logging;

namespace SignalRHubECS;

public class MessageHub: Hub 
{
    public async Task JoinGroup(string groupId) 
    {
        await Groups.AddToGroupAsync(Context.ConnectionId, groupId);
    }

    public async Task BroadcastMessage(string groupId, string method, string message) 
    {
        await Clients.Group(groupId).SendAsync(method, message);
    }
}