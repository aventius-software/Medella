#region Namespaces

using Microsoft.AspNetCore.JsonPatch;
using Newtonsoft.Json;
using System.Text;

#endregion

namespace Services.Client.Extensions;

public static class HttpClientExtensions
{
    public static async Task<HttpResponseMessage> PatchAsync<T>(this HttpClient client, string requestUri, JsonPatchDocument<T> patchDocument, CancellationToken cancellationToken = default) where T : class
    {
        var writer = new StringWriter();
        var serializer = new JsonSerializer();
        serializer.Serialize(writer, patchDocument);
        var json = writer.ToString();

        var content = new StringContent(json, Encoding.UTF8, "application/json-patch+json");

        return await client.PatchAsync(requestUri, content, cancellationToken);
    }
}
