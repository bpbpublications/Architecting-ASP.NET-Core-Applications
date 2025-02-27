using System.Net.Mime;
using System.Text.Json;
using System.Text;
using Chapter9CqrsUI.ExtensionMethods;

namespace Chapter9CqrsUI.HttpServices;

public abstract class AbstractService : IDisposable
{
    protected HttpClient HttpClient { get; private set; }

    protected AbstractService(IHttpClientFactory httpClientFactory)
    {
        HttpClient = httpClientFactory.CreateClient();
    }

    protected async Task<HttpResponseMessage> PerformPostAsync(string endpoint, object? payload = null)
    {
        StringContent? requestContent = null;
        if (payload is not null)
        {
            var content = JsonSerializer.Serialize(payload);
            requestContent = new StringContent(content
                , Encoding.UTF8
                , MediaTypeNames.Application.Json);
        }
        var output = await HttpClient.PostAsync(endpoint, requestContent)
                                     .ConfigureAwait(false);
        return output;
    }

    public async Task<T?> PerformGetAsync<T>(string endpoint) where T : class
    {
        var response = await HttpClient.GetAsync(endpoint);
        if (response.IsSuccessStatusCode)
        {
            return await response.GetFromJsonAsync<T>();
        }
        return null;
    }

    public void Dispose() => GC.SuppressFinalize(this);
}
