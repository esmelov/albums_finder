namespace ITunes.Contracts.Software
{
    public class SoftwareRequest<TResponse> : ITunesRequest<SoftwareEntity, SoftwareAttribute, TResponse>
        where TResponse : SoftwareResponse
    {
        public SoftwareRequest(string term)
            : base(Media.Software, term)
        { }
    }
}
