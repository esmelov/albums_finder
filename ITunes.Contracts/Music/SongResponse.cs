using System;

namespace ITunes.Contracts.Music
{
    public class SongResponse : MusicResponse
    {
        public Kind Kind { get; set; }
        public long CollectionId { get; set; }
        public long TrackId { get; set; }
        public string CollectionName { get; set; }
        public string TrackName { get; set; }
        public string CollectionCensoredName { get; set; }
        public string TrackCensoredName { get; set; }
        public Uri ArtistViewUrl { get; set; }
        public Uri CollectionViewUrl { get; set; }
        public Uri TrackViewUrl { get; set; }
        public Uri PreviewUrl { get; set; }
        public Uri ArtworkUrl30 { get; set; }
        public Uri ArtworkUrl60 { get; set; }
        public Uri ArtworkUrl100 { get; set; }
        public decimal CollectionPrice { get; set; }
        public decimal TrackPrice { get; set; }
        public DateTime ReleaseDate { get; set; }
        public string CollectionExplicitness { get; set; }
        public string TrackExplicitness { get; set; }
        public int DiscCount { get; set; }
        public int DiscNumber { get; set; }
        public int TrackCount { get; set; }
        public int TrackNumber { get; set; }
        public int TrackTimeMillis { get; set; }
        public string Country { get; set; }
        public string Currency { get; set; }
        public bool IsStreamable { get; set; }
        public string CollectionArtistName { get; set; }
        public string ContentAdvisoryRating { get; set; }
    }

}
