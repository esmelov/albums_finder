using ITunes.Client;
using ITunes.Client.Extensions;
using ITunes.Contracts;
using ITunes.Contracts.Music;
using LiteDB;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;

namespace AlbumsFinder
{
    internal static class Worker
    {
        public static async Task<Response<AlbumResponse>> GetAlbums (Options options)
        {
            using var db = new LiteDatabase(options.ConnectionString);
            var cacheCollection = db.GetCollection<Cache>();
            cacheCollection.EnsureIndex(x => x.Key);
            var handler = new FallbackHandler(cacheCollection);
            using var httpClient = new HttpClient(handler) { BaseAddress = options.ITunesUrl };
            var iTunesClient = new ITunesClient(httpClient);

            return await iTunesClient.GetAlbums(options.ArtistName, options.Limit);
        }

        public static void PrintAlbums(IReadOnlyList<AlbumResponse> albums)
        {
            Console.WriteLine("Finded: {0}", albums.Count);

            if (albums.Count == 0) return;

            var (skip, take, moveNext) = (0, 10, true);
            var ordered = albums.OrderBy(x => x.ReleaseDate).ToArray();
            while (moveNext)
            {
                foreach (var album in ordered.Skip(skip).Take(take))
                    Console.WriteLine("{0} {1}", album.ReleaseDate.ToString("yyyy MMMM dd").PadRight(20, '.'), album.CollectionName);

                skip += take;
                moveNext = skip < albums.Count;
                if (moveNext)
                {
                    Console.WriteLine("For next press <Enter> or <q> for break");
                    var k = Console.ReadKey(true);
                    Console.SetCursorPosition(0, Console.CursorTop - 1);
                    if (k.Key == ConsoleKey.Q) break;
                }
            }

            Console.WriteLine("For exit press <Enter>");
            Console.ReadKey();
        }
    }
}
