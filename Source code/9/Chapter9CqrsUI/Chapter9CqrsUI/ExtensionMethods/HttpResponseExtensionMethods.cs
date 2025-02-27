using System.Text.Json;

namespace Chapter9CqrsUI.ExtensionMethods;


public static class HttpReponseExtensionMethods
{
    private static readonly JsonSerializerOptions defaultJsonOptions =
        new()
        {
            AllowTrailingCommas = true,
            PropertyNameCaseInsensitive = true,
        };

    public static async Task<T?> GetFromJsonAsync<T>(
        this HttpResponseMessage response,
        JsonSerializerOptions? jsonOptions = null,
        CancellationToken token = default) where T : class
    {
        var option = jsonOptions ?? defaultJsonOptions;
        return await response.Content.ReadFromJsonAsync<T>(option, token);
    }

    public static async Task<IEnumerable<T>?> GetListFromJsonAsync<T>(
        this HttpResponseMessage response,
        JsonSerializerOptions? jsonOptions = null,
        CancellationToken token = default) where T : class
    {
        var option = jsonOptions ?? defaultJsonOptions;
        return await response.Content.ReadFromJsonAsync<IEnumerable<T>>(option, token);
    }
}
