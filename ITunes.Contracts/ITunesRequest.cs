﻿using ITunes.Contracts.Extensions;
using System;
using System.Collections.Specialized;
using System.Web;

namespace ITunes.Contracts
{
    public abstract class ITunesRequest<TEntity, TAttribute, TResponse> : IRequest<TResponse>
        where TEntity : struct, Enum
        where TAttribute : struct, Enum
        where TResponse : ITunesReponse
    {
        protected ITunesRequest(Media media, string term)
        {
            Media = media;
            Term = term;
        }

        /// <summary>
        /// The URL-encoded text string you want to search for. For example: jack+johnson.
        /// </summary>
        public string Term { get; }

        /// <summary>
        /// The media type you want to search for.
        /// For example: movie.
        /// </summary>
        public Media Media { get; }

        /// <summary>
        /// The two-letter country code for the store you want to search.
        /// The search uses the default store front for the specified country.
        /// For example: US. The default is US.
        /// See http://en.wikipedia.org/wiki/ ISO_3166-1_alpha-2 for a list of ISO Country Codes.
        /// </summary>
        public string CountryCode { get; set; }

        /// <summary>
        /// The type of results you want returned, relative to the specified media type.
        /// For example: movieArtist for a movie media type search.
        /// The default is the track entity associated with the specified media type.
        /// </summary>
        public TEntity? Entity { get; set; }

        /// <summary>
        /// The attribute you want to search for in the stores, relative to the specified media type.
        /// For example, if you want to search for an artist by name specify entity=allArtist&attribute=allArtistTerm.
        /// In this example, if you search for term=maroon, iTunes returns “Maroon 5” in the search results, instead of all artists who have ever recorded a song with the word “maroon” in the title.
        /// The default is all attributes associated with the specified media type.
        /// </summary>
        public TAttribute? Attribute { get; set; }

        /// <summary>
        /// The number of search results you want the iTunes Store to return. For example: 25. The default is 50.
        /// </summary>
        public int Limit { get; set; }

        /// <summary>
        /// The language, English or Japanese, you want to use when returning search results.
        /// Specify the language using the five-letter codename.
        /// For example: en_us. The default is en_us (English).
        /// </summary>
        public string Language { get; set; }

        public virtual string ToQueryString()
            => GetPropertiesCollection().ToString();

        protected NameValueCollection GetPropertiesCollection()
        {
            if (string.IsNullOrEmpty(Term)) throw new ArgumentNullException(nameof(Term));

            var collection = HttpUtility.ParseQueryString(string.Empty);
            collection.Add("term", Term);
            collection.Add("media", Media.WithFirstLowChar());
            if (!string.IsNullOrEmpty(CountryCode)) collection.Add("country", CountryCode);
            if (Entity.HasValue) collection.Add("entity", Entity.Value.WithFirstLowChar());
            if (Attribute.HasValue) collection.Add("attribute", Attribute.Value.WithFirstLowChar());
            if (Limit > 0) collection.Add("limit", Limit.ToString());
            if (!string.IsNullOrEmpty(Language)) collection.Add("lang", Language);

            return collection;
        }
    }
}
