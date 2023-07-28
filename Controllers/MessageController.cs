using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.SignalR;

namespace SignalRHubECS;

[Route("message")]
[Produces("application/json")]
[ApiController]
public class MessageController : ControllerBase
{
    private readonly IHubContext<MessageHub> _hubContext;

    public MessageController(IHubContext<MessageHub> hubContext)
    {
        _hubContext = hubContext;
    }

    [HttpPost("broadcast")]
    public async Task<ActionResult> BroadcastMessage([FromBody] MessageBroadcastRequest request)
    {
        await _hubContext.Clients.Group(request.UserId).SendAsync("NewEvent", request.Message);
        return Ok();
    }
}