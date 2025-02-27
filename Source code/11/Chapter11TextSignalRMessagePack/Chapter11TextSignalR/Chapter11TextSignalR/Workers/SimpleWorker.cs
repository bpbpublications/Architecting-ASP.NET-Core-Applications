using Chapter11TextSignalR.Models;
using Chapter11TextSignalR.Shared.Models;
using MessagePack;
using Microsoft.AspNetCore.SignalR.Client;
using Microsoft.AspNetCore.SignalR.Protocol;
using System.Text.Json;

namespace Chapter11TextSignalR.Server.Workers;
public class SimpleWorker
{
    private HubConnection? hubConnection1;
    string? apiKey;
    private string baseAddress;

    private int timeDelay = 10;

    /// <summary>
    /// Sets the hub.
    /// </summary>
    /// <param name="hub">The hub.</param>
    /// <param name="apiKey">The API key.</param>
    public void SetHub(HubConnection hub, string apiKey, string baseAddress)
    {
        hubConnection1 = hub;
        this.apiKey = apiKey;
        this.baseAddress = baseAddress;
    }

    /// <summary>
    /// Executes the task.
    /// </summary>
    /// <param name="stoppingToken">The stopping token.</param>
    public async void ExecuteAsync(CancellationToken stoppingToken)
    {
        var hubConnection = new HubConnectionBuilder()
                .WithUrl(new Uri(baseAddress))
                .AddMessagePackProtocol()
                .Build();
        await hubConnection.StartAsync();

        // Add to group
        await hubConnection.SendAsync("AddClientToGroup", "TIME");

        var i = 0;
        while (!stoppingToken.IsCancellationRequested)
        {
            await Task.Delay(timeDelay, stoppingToken);

            _ = hubConnection?.SendAsync("SendToGroup", "TIME", new NotificationTransport()
            {
                Message =DateTime.Now.Second + " - "+ DateTime.Now.Millisecond.ToString(),
                MessageType = "TIME"
            }, stoppingToken);
            if (i > 010000) i = 0;
        }
    }

    /// <summary>
    /// Gets the news from website.
    /// </summary>
    /// <param name="stoppingToken">The stopping token.</param>
    public async Task GetNews(CancellationToken stoppingToken) => await GetNewsCaller(stoppingToken);


    /// <summary>
    /// Gets the news caller.
    /// </summary>
    /// <param name="stoppingToken">The stopping token.</param>
    private async Task GetNewsCaller(CancellationToken stoppingToken)
    {
        try
        {
            var url = "https://newsapi.org/v2/everything?" +
              "sources=ansa&" +
              "apiKey=" + apiKey;

            HttpClient client = new HttpClient();
            client.DefaultRequestHeaders.Add("User-Agent", "C# App");
            var response = await client.GetAsync(url);
            var json = await response.Content.ReadAsStringAsync(); ;

            var news = JsonSerializer.Deserialize<Root>(json);

            if (news is not null && news.articles is not null)
            {
                foreach (var item in news.articles)
                {
                    await Task.Delay(55000, stoppingToken);
                    var serializedArticle = JsonSerializer.Serialize(item);

                    _ = hubConnection1?.SendAsync("SendToGroup", "NEWS", new NotificationTransport()
                    {
                        Message = serializedArticle,
                        MessageType = nameof(Article)
                    });



                    //hubConnection?.SendAsync(nameof(IHub.SendMessage), new NotificationTransport()
                    //{
                    //    SendMessage = serializedArticle,
                    //    MessageType = nameof(Article)
                    //});
                }
            }

            Console.WriteLine(json);
        }
        catch (Exception e)
        {
            var s = e.Message;
            Console.WriteLine(s);
        }
    }
}

