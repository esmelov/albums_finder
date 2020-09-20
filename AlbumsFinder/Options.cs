using CommandLine;
using System;

namespace AlbumsFinder
{
    public class Options
    {
        [Option('a', "artist", Required = true, HelpText = "Name of the artist to search for albums for.")]
        public string ArtistName { get; set; }

        [Option('l', "limit", Required = false, HelpText = "Number of max records in result.")]
        public int Limit { get; set; }

        public Uri ITunesUrl => new Uri("https://itunes.apple.com/search/");

        public string ConnectionString => "filename=itunes.db; password=test";
    }
}
