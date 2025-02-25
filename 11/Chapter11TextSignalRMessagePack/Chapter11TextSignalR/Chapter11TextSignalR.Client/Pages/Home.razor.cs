using Chapter11TextSignalR.Client.Services;
using Microsoft.AspNetCore.Components;

namespace Chapter11TextSignalR.Client.Pages;

public partial class Home: IDisposable
{
    private string Message = string.Empty;
    private INotificationService? notificationService;

    protected override Task OnAfterRenderAsync(bool firstRender)
    {
        if(firstRender)
        {
            notificationService= new NotificationService();
            notificationService.OnMessageReceived += NotificationService_OnMessageReceived;
        }
        return base.OnAfterRenderAsync(firstRender);
    }

    private async Task NotificationService_OnMessageReceived(string e)
    {
        Message = e;
        await InvokeAsync(() => StateHasChanged());
    }

    public void Dispose()
    {
        if (notificationService is not null)
        {
            notificationService.OnMessageReceived -= NotificationService_OnMessageReceived;
        }
    }
}
