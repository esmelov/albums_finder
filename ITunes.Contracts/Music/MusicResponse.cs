namespace ITunes.Contracts.Music
{
    public class MusicResponse : ITunesReponse
    {
        public long ArtistId { get; set; }
        public string ArtistName { get; set; }
        public string PrimaryGenreName { get; set; }
    }
}
