namespace ITunes.Contracts.MusicVideo
{
    public class MusicVideoRequest<TResponse> : ITunesRequest<MusicVideoEntity, MusicVideoAttribute, TResponse>
        where TResponse : MusicVideoResponse
    {
        public MusicVideoRequest(string term)
            : base(Media.MusicVideo, term)
        { }
    }
}
