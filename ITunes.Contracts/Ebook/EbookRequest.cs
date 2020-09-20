namespace ITunes.Contracts.Ebook
{
    public class EbookRequest<TResponse> : ITunesRequest<EbookEntity, EbookAttribute, TResponse>
        where TResponse : EbookResponse
    {
        public EbookRequest(string term)
            : base(Media.Ebook, term)
        { }
    }
}
