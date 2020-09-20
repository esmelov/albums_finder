namespace ITunes.Contracts.Podcast
{
    public class PodcastRequest<TResponse> : ITunesRequest<PodcastEntity, PodcastAttribute, TResponse>
        where TResponse : PodcastResponse
    {
        public PodcastRequest(string term)
            : base(Media.Podcast, term)
        { }
    }
}
