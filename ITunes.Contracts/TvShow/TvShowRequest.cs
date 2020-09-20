namespace ITunes.Contracts.TvShow
{
    public class TvShowRequest<TResponse> : ITunesRequest<TvShowEntity, TvShowAttribute, TResponse>
        where TResponse : TvShowResponse
    {
        public TvShowRequest(string term)
            : base(Media.TvShow, term)
        { }
    }
}
