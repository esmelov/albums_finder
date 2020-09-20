using ITunes.Contracts.Movie;

namespace ITunes.Contracts.Movie
{
    public class MovieRequest<TResponse> : ITunesRequest<MovieEntity, MovieAttribute, TResponse>
        where TResponse : MovieResponse
    {
        public MovieRequest(string term)
            : base(Media.Movie, term)
        { }
    }
}
