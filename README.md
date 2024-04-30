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
You can see my sample code in 

https://github.com/roy55688/LineBot_Sample

```cs
using LineBotTemplate.Applications;
using Line.Messaging;
using Line.Messaging.Webhooks;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddSingleton<LineMessagingClient>(provider =>
{
    var AccessToken = "Your AccessToken";
    return new LineMessagingClient(AccessToken);
});

builder.Services.AddHttpContextAccessor();

var app = builder.Build();

app.MapPost("/linebot", async (IServiceProvider serviceProvider, ILineBotApplication lineBotApplication) =>
{
    HttpContext _httpContext = serviceProvider.GetRequiredService<IHttpContextAccessor>().HttpContext;
    var ChannelSecret = "Your ChannelSecret";
    var events = await _httpContext.Request.GetWebhookEventsAsync(ChannelSecret);
    await lineBotApplication.RunAsync(events);
    return Results.NoContent();
});

app.Run();

```
