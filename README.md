# Perform issue fixes and version upgrade for Line.Messaging
* Upgraded the framework from .NET Framework 4.6 to .NET Core 6 and addressed compatibility issues.
* Updated URLs for image, audio, and video messages to use a different domain name.
* Created message types for image, audio, and video.


## The original project URL 
* https://github.com/pierre3/LineMessagingApi/

## Modified content references to the data source at
* https://ithelp.ithome.com.tw/users/20106865/ironman/2732

## Development environment
* Visual Stdio 2022

## TargetFramework
* .net 6

### Sample Code

```cs
using Line.Messaging.Webhooks;
using Line.Messaging;
using Microsoft.AspNetCore.Mvc;
using Imgur.API.Authentication;
using Imgur.API.Endpoints;

namespace LineBot.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class LineBotController : ControllerBase
    {
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly HttpContext _httpContext;
        private readonly string channelSecret = "{Your channelSecret}";
        private readonly string accessToken = "{Your accessToken}";

        public LineBotController(IServiceProvider serviceProvider)
        {
            _httpContextAccessor = serviceProvider.GetRequiredService<IHttpContextAccessor>();
            _httpContext = _httpContextAccessor.HttpContext;
        }

        //full URL https://xxx/linebot/run
        [HttpPost("run")]
        public async Task<IActionResult> Post()
        {
            var events = await _httpContext.Request.GetWebhookEventsAsync(channelSecret);
            var lineMessagingClient = new LineMessagingClient(accessToken);

            var lineBotApp = new LineBotApp(lineMessagingClient);
            await lineBotApp.RunAsync(events);

            return Ok();
        }
        public class LineBotApp : WebhookApplication
        {
            private LineMessagingClient _messagingClient;
            public LineBotApp(LineMessagingClient lineMessagingClient)
            {
                _messagingClient = lineMessagingClient;
            }

            protected override async Task OnMessageAsync(MessageEvent ev)
            {
                List<ISendMessage> result = new List<ISendMessage>();

                string userId = ev.Source.UserId;
                string channelId = ev.Source.Id;

                switch (ev.Message)
                {
                    case TextEventMessage textMessage:
                        {
                            string text = textMessage.Text;
                            switch (text)
                            {
                                case "Image_A":
                                    break;
                                case "Image_B":
                                    break;
                                default:
                                    break;
                            }
                            result.Add(new TextMessage($"You say:{text}"));
                        }
                        break;
                    case ImageEventMessage imageMessage:
                        {
                            if (imageMessage.ContentProvider.Type != ContentProviderType.Line)
                                break;

                            using (var stream = await _messagingClient.GetContentStreamAsync(imageMessage.Id))
                            {
                                string ext = stream.ContentHeaders.ContentType.MediaType.ToString().Split('/')[1];
                                string fileName = $"{imageMessage.Id}.{ext}";

                                string imageUrl = await UploadToImgurAsync(stream);

                                var message = new ImageMessage(imageUrl, imageUrl);
                                message.QuickReply = new QuickReply
                                {
                                    Items = new List<QuickReplyButtonObject>
                                    {
                                        new QuickReplyButtonObject(
                                            new MessageTemplateAction("This is Image_A!", "Image_A")),
                                        new QuickReplyButtonObject(
                                            new MessageTemplateAction("This is Image_B!", "Image_B"))
                                    }
                                };
                                result.Add(message);
                            }
                        }
                        break;
                }

                if (result.Count != 0)
                    await _messagingClient.ReplyMessageAsync(ev.ReplyToken, result);
            }

            public async Task<string> UploadToImgurAsync(Stream stream)
            {
                var apiClient = new ApiClient("{Your Imgur clientId}");

                var httpClient = new HttpClient();

                var imageEndpoint = new ImageEndpoint(apiClient, httpClient);

                var imageUpload = await imageEndpoint.UploadImageAsync(stream);

                return imageUpload.Link;

            }
        }

    }
}

```
