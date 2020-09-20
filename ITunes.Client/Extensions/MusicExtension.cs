using ITunes.Contracts;
using ITunes.Contracts.Music;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace ITunes.Client.Extensions
{
    public static class MusicExtension
    {
        /// <summary>
        /// Find Artist
        /// </summary>
        /// <param name="client"> iTunes client <see cref="ITunesClient"/></param>
        /// <param name="artist"> Artist name </param>
        /// <param name="limit"> Max number of records in result </param>
        /// <param name="country"> The two-letter country ISO code for the store you want to search.
        /// See http://en.wikipedia.org/wiki/%20ISO_3166-1_alpha-2 for a list of ISO country codes</param>
        /// <param name="lang"> The language, English or Japanese, you want to use when returning search results.
        /// Specify the language using the five-letter codename.
        /// For example: en_us. The default is en_us (English). </param>
        /// <param name="cancellationToken"> Canecellation token <see cref="CancellationToken"/></param>
        /// <returns></returns>
        public static Task<Response<ArtistResponse>> GetArtists(this ITunesClient client, string artist,
            int limit = 0, string country = null, string lang = null, CancellationToken cancellationToken = default)
            => client.GetMusicEntity<ArtistResponse>(MusicEntity.MusicArtist, artist, MusicAttribute.ArtistTerm, limit, country, lang, cancellationToken);

        /// <summary>
        /// Find Track
        /// </summary>
        /// <param name="client"> iTunes client <see cref="ITunesClient"/></param>
        /// <param name="keyword"> Keyword for search </param>
        /// <param name="attribute"> Music attribute <see cref="MusicAttribute"/> </param>
        /// <param name="limit"> Max number of records in result </param>
        /// <param name="country"> The two-letter country ISO code for the store you want to search.
        /// See http://en.wikipedia.org/wiki/%20ISO_3166-1_alpha-2 for a list of ISO country codes</param>
        /// <param name="lang"> The language, English or Japanese, you want to use when returning search results.
        /// Specify the language using the five-letter codename.
        /// For example: en_us. The default is en_us (English). </param>
        /// <param name="cancellationToken"> Canecellation token <see cref="CancellationToken"/></param>
        /// <returns></returns>
        public static Task<Response<AlbumResponse>> GetTracks(this ITunesClient client, string keyword,
            MusicAttribute? attribute = null, int limit = 0, string country = null, string lang = null,
            CancellationToken cancellationToken = default)
            => client.GetMusicEntity<AlbumResponse>(MusicEntity.Album, keyword, attribute, limit, country, lang, cancellationToken);

        /// <summary>
        /// Find Albums for Artist
        /// </summary>
        /// <param name="client"> iTunes client <see cref="ITunesClient"/></param>
        /// <param name="artist"> Artist name for search </param>
        /// <param name="limit"> Max number of records in result </param>
        /// <param name="country"> The two-letter country ISO code for the store you want to search.
        /// See http://en.wikipedia.org/wiki/%20ISO_3166-1_alpha-2 for a list of ISO country codes</param>
        /// <param name="lang"> The language, English or Japanese, you want to use when returning search results.
        /// Specify the language using the five-letter codename.
        /// For example: en_us. The default is en_us (English). </param>
        /// <param name="cancellationToken"> Canecellation token <see cref="CancellationToken"/></param>
        /// <returns></returns>
        public static Task<Response<AlbumResponse>> GetAlbums(this ITunesClient client, string artist,
            int limit = 0, string country = null, string lang = null, CancellationToken cancellationToken = default)
            => client.GetMusicEntity<AlbumResponse>(MusicEntity.Album, artist, MusicAttribute.ArtistTerm, limit, country, lang, cancellationToken);

        /// <summary>
        /// Find Music Video
        /// </summary>
        /// <param name="client"> iTunes client <see cref="ITunesClient"/></param>
        /// <param name="keyword"> Keyword for search </param>
        /// <param name="attribute"> Music attribute <see cref="MusicAttribute"/> </param>
        /// <param name="limit"> Max number of records in result </param>
        /// <param name="country"> The two-letter country ISO code for the store you want to search.
        /// See http://en.wikipedia.org/wiki/%20ISO_3166-1_alpha-2 for a list of ISO country codes</param>
        /// <param name="lang"> The language, English or Japanese, you want to use when returning search results.
        /// Specify the language using the five-letter codename.
        /// For example: en_us. The default is en_us (English). </param>
        /// <param name="cancellationToken"> Canecellation token <see cref="CancellationToken"/></param>
        /// <returns></returns>
        public static Task<Response<MusicVideoResponse>> GetMusicVideos(this ITunesClient client, string keyword,
            MusicAttribute? attribute = null, int limit = 0, string country = null, string lang = null,
            CancellationToken cancellationToken = default)
            => client.GetMusicEntity<MusicVideoResponse>(MusicEntity.MusicVideo, keyword, attribute, limit, country, lang, cancellationToken);

        /// <summary>
        /// Find Song
        /// </summary>
        /// <param name="client"> iTunes client <see cref="ITunesClient"/></param>
        /// <param name="song"> Name of song for search </param>
        /// <param name="limit"> Max number of records in result </param>
        /// <param name="country"> The two-letter country ISO code for the store you want to search.
        /// See http://en.wikipedia.org/wiki/%20ISO_3166-1_alpha-2 for a list of ISO country codes</param>
        /// <param name="lang"> The language, English or Japanese, you want to use when returning search results.
        /// Specify the language using the five-letter codename.
        /// For example: en_us. The default is en_us (English). </param>
        /// <param name="cancellationToken"> Canecellation token <see cref="CancellationToken"/></param>
        /// <returns></returns>
        public static Task<Response<SongResponse>> GetSongs(this ITunesClient client, string song,
            int limit = 0, string country = null, string lang = null, CancellationToken cancellationToken = default)
            => client.GetMusicEntity<SongResponse>(MusicEntity.Song, song, null, limit, country, lang, cancellationToken);

        private static Task<Response<T>> GetMusicEntity<T>(this ITunesClient client, MusicEntity entity, string term,
            MusicAttribute? musicAttribute = null, int limit = 0, string country = null, string lang = null,
            CancellationToken cancellationToken = default)
            where T : MusicResponse
        {
            if (client == null) throw new ArgumentNullException(nameof(client));

            var request = new MusicRequest<T>(term)
            {
                Entity = entity,
                Attribute = musicAttribute,
                Limit = limit,
                CountryCode = country,
                Language = lang
            };

            return client.GetMediaInfo(request, cancellationToken);
        }
    }
}
