namespace ITunes.Contracts
{
    public interface IRequest<TReponse>
    {
        public string Term { get; }

        public Media Media { get; }

        string ToQueryString();
    }
}
