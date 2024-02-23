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


