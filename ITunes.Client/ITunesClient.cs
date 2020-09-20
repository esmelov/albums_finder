using ITunes.Contracts;
using System;
using System.Net.Http;
using System.Text.Json;
using System.Text.Json.Serialization;
using System.Threading;
using System.Threading.Tasks;

namespace ITunes.Client
{
    public class ITunesClient
    {
        private readonly HttpClient _httpClient;

        public ITunesClient(HttpClient httpClient)
        {
            _httpClient = httpClient ?? throw new ArgumentNullException(nameof(httpClient));
        }

        public async Task<Response<TResponse>> GetMediaInfo<TResponse>(IRequest<TResponse> request, CancellationToken cancellationToken = default)
            where TResponse : ITunesReponse
        {
            if (request == null) return default;

            var queryParameters = request.ToQueryString();

            using var searchReaquest = new HttpRequestMessage(HttpMethod.Get, $"?{queryParameters}");
            using var searchResponse = await _httpClient.SendAsync(searchReaquest, cancellationToken);

            searchResponse.EnsureSuccessStatusCode();

            return await Deserialize<Response<TResponse>>(searchResponse.Content, cancellationToken);
        }

        private static async Task<T> Deserialize<T>(HttpContent content, CancellationToken cancellationToken)
        {
            await using var contentStream = await content.ReadAsStreamAsync();

            var serializeOptions = new JsonSerializerOptions { PropertyNamingPolicy = JsonNamingPolicy.CamelCase };
            serializeOptions.Converters.Add(new JsonStringEnumConverter(JsonNamingPolicy.CamelCase));

            return await JsonSerializer.DeserializeAsync<T>(contentStream, serializeOptions, cancellationToken);
        }
    }
}
