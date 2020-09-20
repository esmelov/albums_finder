using System;

namespace ITunes.Contracts.Music
{
    public class AlbumResponse : MusicResponse
    {
        public Kind CollectionType { get; set; }
        public long CollectionId { get; set; }
        public long AmgArtistId { get; set; }
        public string CollectionName { get; set; }
        public string CollectionCensoredName { get; set; }
        public Uri ArtistViewUrl { get; set; }
        public Uri CollectionViewUrl { get; set; }
        public Uri ArtworkUrl60 { get; set; }
        public Uri ArtworkUrl100 { get; set; }
        public decimal CollectionPrice { get; set; }
        public string CollectionExplicitness { get; set; }
        public int TrackCount { get; set; }
        public string Copyright { get; set; }
        public string Country { get; set; }
        public string Currency { get; set; }
        public DateTime ReleaseDate { get; set; }
        public string ContentAdvisoryRating { get; set; }
    }
}
