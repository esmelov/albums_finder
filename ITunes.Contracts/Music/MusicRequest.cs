namespace ITunes.Contracts.Music
{
    public class MusicRequest<TResponse> : ITunesRequest<MusicEntity, MusicAttribute, TResponse>
        where TResponse : MusicResponse
    {
        public MusicRequest(string term)
            : base(Media.Music, term)
        { }
    }
}
