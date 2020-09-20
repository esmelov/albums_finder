using System.Collections.Generic;

namespace ITunes.Contracts
{
    public class Response<T>
        where T : ITunesReponse
    {
        public int ResultCount { get; set; }

        public IReadOnlyList<T> Results { get; set; }
    }
}
