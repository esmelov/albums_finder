namespace ITunes.Contracts.Audiobook
{
    public class AudiobookRequest<TResponse> : ITunesRequest<AudiobookEntity, AudiobookAttribute, TResponse>
        where TResponse : AudiobookResponse
    {
        public AudiobookRequest(string term)
            : base(Media.Audiobook, term)
        { }
    }
}
