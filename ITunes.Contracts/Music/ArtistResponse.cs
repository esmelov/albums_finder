using System;

namespace ITunes.Contracts.Music
{
    public class ArtistResponse : MusicResponse
    {
        public Kind ArtistType { get; set; }
        public Uri ArtistLinkUrl { get; set; }
        public long AmgArtistId { get; set; }
        public long PrimaryGenreId { get; set; }
    }

}
