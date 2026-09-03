using Microsoft.AspNetCore.SignalR;

namespace Hotels.Application.Hubs
{
    public class NotificationHub : Hub
    {
        public override async Task OnConnectedAsync()
        {
            await base.OnConnectedAsync();
        }
    }
}
