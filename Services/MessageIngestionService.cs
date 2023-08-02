using System.Threading;
using System.Threading.Tasks;

using Amazon;
using Amazon.SQS;
using Amazon.SQS.Model;
using Newtonsoft.Json;
using Microsoft.AspNetCore.SignalR;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;

namespace SignalRHubECS;

public class MessageIngestionService: BackgroundService 
{
    private readonly IHubContext<MessageHub> _hubContext;
    private readonly IAmazonSQS _sqsClient;
    private readonly string _sqsEndpoint;

     public MessageIngestionService(IHubContext<MessageHub> hubContext)
    {
        _hubContext = hubContext;
        _sqsClient = new AmazonSQSClient(RegionEndpoint.EUWest1);
        _sqsEndpoint = "https://sqs.eu-west-1.amazonaws.com/937371417888/signalr-sqs-queue";
    }

    protected override async Task ExecuteAsync(CancellationToken token) 
    {
        var receiveRequest = GetReceiveMessageRequest();

        while(!token.IsCancellationRequested)
        {
            var response = await _sqsClient.ReceiveMessageAsync(receiveRequest);

            foreach (var message in response.Messages)
            {
                var ev = JsonConvert.DeserializeObject<MessageBroadcastRequest>(message.Body);
                await SendSignalRMessage(ev);
                await DeleteMessage(message.ReceiptHandle);
            }
        }
    }

    private ReceiveMessageRequest GetReceiveMessageRequest() =>
        new ReceiveMessageRequest { QueueUrl = _sqsEndpoint };

    private async Task SendSignalRMessage(MessageBroadcastRequest req) =>
        await _hubContext.Clients.Group(req.GroupId).SendAsync(req.Method, req.Message);

    private async Task DeleteMessage(string receiptHandle) =>
        await _sqsClient.DeleteMessageAsync(_sqsEndpoint, receiptHandle);
}