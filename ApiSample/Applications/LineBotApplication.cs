using Line.Messaging.Webhooks;
using Line.Messaging;

namespace LineBotTemplate.Applications
{

    public interface ILineBotApplication
    {
        public Task RunAsync(IEnumerable<WebhookEvent> webhookEvents);
    }
    public class LineBotApplication(LineMessagingClient _messagingClient) : WebhookApplication, ILineBotApplication
    {
        protected override async Task OnMessageAsync(MessageEvent ev)
        {
            List<ISendMessage> result = new List<ISendMessage>();

            string userId = ev.Source.UserId;
            string channelId = ev.Source.Id;

            switch (ev.Message)
            {
                case TextEventMessage textMessage:
                    break;
            }

            if (result.Count != 0)
                await _messagingClient.ReplyMessageAsync(ev.ReplyToken, result);
        }
    }
}
