namespace ITunes.Contracts.ShortFilm
{
    public class ShortFilmRequest<TResponse> : ITunesRequest<ShortFilmEntity, ShortFilmAttribute, TResponse>
        where TResponse : ShortFilmResponse
    {
        public ShortFilmRequest(string term)
            : base(Media.ShortFilm, term)
        { }
    }
}
