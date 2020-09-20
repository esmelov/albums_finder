using LiteDB;
using System;
using System.Net.Http;
using System.Net.Sockets;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

[assembly: InternalsVisibleTo("AlbumFinder.Tests.Unit")]
namespace AlbumsFinder
{
    public class Cache : IEquatable<Cache>
    {
        [BsonCtor]
        public Cache(string key, string value)
        {
            Key = key;
            Value = value;
        }

        [BsonId]
        public string Key { get; }

        public string Value { get; }

        public bool Equals(Cache other)
        {
            if (ReferenceEquals(this, other))
            {
                return true;
            }

            return other != null
                && Key == other.Key
                && Value == other.Value;
        }

        public override bool Equals(object obj)
            => Equals(obj as Cache);

        public override int GetHashCode()
            => HashCode.Combine(Key, Value);

        public override string ToString()
            => Key;
    }

    internal class FallbackHandler : HttpClientHandler
    {
        private readonly ILiteCollection<Cache> _cache;

        public FallbackHandler(ILiteCollection<Cache> cache)
        {
            _cache = cache ?? throw new ArgumentNullException(nameof(cache));
        }

        protected override async Task<HttpResponseMessage> SendAsync(HttpRequestMessage request, CancellationToken cancellationToken)
        {
            try
            {
                var response = await base.SendAsync(request, cancellationToken);
                if (response.IsSuccessStatusCode && response.Content.Headers.ContentLength > 0)
                {
                    var value = await response.Content.ReadAsStringAsync();
                    _cache.Upsert(new Cache(request.RequestUri.Query, value));
                }

                return response;
            }
            catch (HttpRequestException ex) when (ex.InnerException is SocketException e && e.NativeErrorCode == 11001)
            {
                if (!_cache.Exists(x => x.Key == request.RequestUri.Query)) throw;

                var record = _cache.FindById(new BsonValue(request.RequestUri.Query));

                return new HttpResponseMessage(System.Net.HttpStatusCode.OK)
                {
                    Content = new StringContent(record.Value, Encoding.UTF8, "application/json")
                };
            }
        }
    }
}
