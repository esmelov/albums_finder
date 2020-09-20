using CommandLine;
using System.Threading.Tasks;

namespace AlbumsFinder
{
    class Program
    {
        static async Task Main(string[] args)
            => await Parser.Default.ParseArguments<Options>(args)
                .WithParsedAsync(
                    opt => Worker.GetAlbums(opt)
                        .ContinueWith(t => Worker.PrintAlbums(t.Result.Results)));
    }
}
