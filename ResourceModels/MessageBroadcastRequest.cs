namespace SignalRHubECS;

public class MessageBroadcastRequest
{
    public string GroupId { get; set; }
    public string Method { get; set; }
    public string Message { get; set; }
} 