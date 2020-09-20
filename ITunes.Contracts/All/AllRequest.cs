namespace ITunes.Contracts.All
{
    public class AllRequest<TResponse> : ITunesRequest<AllEntity, AllAttribute, TResponse>
        where TResponse : AllResponse
    {
        public AllRequest(string term)
            : base(Media.All, term)
        { }
    }
}
