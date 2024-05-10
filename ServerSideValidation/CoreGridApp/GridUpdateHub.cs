using Microsoft.AspNetCore.SignalR;

public class GridUpdateHub : Hub
{
    public async Task SendGridUpdateNotification()
    {
        await Clients.All.SendAsync("ReceiveGridUpdate");
    }
}