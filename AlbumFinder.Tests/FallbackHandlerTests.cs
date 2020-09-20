using AlbumsFinder;
using ITunes.Client;
using ITunes.Client.Extensions;
using LiteDB;
using Moq;
using NUnit.Framework;
using System;
using System.Linq.Expressions;
using System.Net.Http;
using System.Threading.Tasks;

namespace AlbumFinder.Tests.Unit
{
    [TestFixture]
    public class Tests
    {
        [Test]
        public async Task SendAsync_GetRecords_UpsertInCache_ShouldBeOk()
        {
            var key = "?term=Iron+Maiden&media=music&entity=album&attribute=artistTerm&limit=1";
            var value = "\n\n\n{\n \"resultCount\":1,\n \"results\": [\n{\"wrapperType\":\"collection\", \"collectionType\":\"Album\", \"artistId\":546381, \"collectionId\":1147164122, \"amgArtistId\":4560, \"artistName\":\"Iron Maiden\", \"collectionName\":\"Piece of Mind (Remastered)\", \"collectionCensoredName\":\"Piece of Mind (Remastered)\", \"artistViewUrl\":\"https://music.apple.com/us/artist/iron-maiden/546381?uo=4\", \"collectionViewUrl\":\"https://music.apple.com/us/album/piece-of-mind-remastered/1147164122?uo=4\", \"artworkUrl60\":\"https://is4-ssl.mzstatic.com/image/thumb/Music71/v4/8e/87/49/8e8749c5-a06d-3f97-978e-08ca147c3629/source/60x60bb.jpg\", \"artworkUrl100\":\"https://is4-ssl.mzstatic.com/image/thumb/Music71/v4/8e/87/49/8e8749c5-a06d-3f97-978e-08ca147c3629/source/100x100bb.jpg\", \"collectionPrice\":9.99, \"collectionExplicitness\":\"notExplicit\", \"trackCount\":9, \"copyright\":\"℗ 1998 2015 Iron Maiden Holdings Ltd. under exclusive license to Sanctuary Records Group Ltd., a BMG Company\", \"country\":\"USA\", \"currency\":\"USD\", \"releaseDate\":\"1983-05-16T07:00:00Z\", \"primaryGenreName\":\"Metal\"}]\n}\n\n\n";
            var element = new Cache(key, value);

            var cacheCollectionMock = GetCollectionMock(element, true);
            var iTunesClient = GetClient(cacheCollectionMock.Object, "https://itunes.apple.com/search/");

            var results = await iTunesClient.GetAlbums("Iron Maiden", 1);

            Assert.IsNotNull(results);
            Assert.AreEqual(1, results.ResultCount);
            Assert.AreEqual(1, results.Results.Count);

            cacheCollectionMock.Verify(x => x.Upsert(It.Is<Cache>(x => x.Equals(element))), Times.Once);
            cacheCollectionMock.Verify(x => x.Exists(It.IsAny<Expression<Func<Cache, bool>>>()), Times.Never);
            cacheCollectionMock.Verify(x => x.FindById(It.Is<BsonValue>(x => x == new BsonValue(key))), Times.Never);
        }

        [Test]
        public async Task SendAsync_GetRecords_ExistsInCache_ShouldBeOk()
        {
            var key = "?term=Iron+Maiden&media=music&entity=album&attribute=artistTerm";
            var value = "{\"resultCount\":0, \"results\":[]}";
            var element = new Cache(key, value);

            var cacheCollectionMock = GetCollectionMock(element, true);
            var iTunesClient = GetClient(cacheCollectionMock.Object);

            var results = await iTunesClient.GetAlbums("Iron Maiden");

            Assert.IsNotNull(results);
            Assert.AreEqual(0, results.ResultCount);

            cacheCollectionMock.Verify(x => x.Exists(It.IsAny<Expression<Func<Cache, bool>>>()), Times.Once);
            cacheCollectionMock.Verify(x => x.FindById(It.Is<BsonValue>(x => x == new BsonValue(key))), Times.Once);
        }

        [Test]
        public void SendAsync_GetRecords_NotExistsInCache_ExcepError()
        {
            var cacheCollectionMock = GetCollectionMock();
            var iTunesClient = GetClient(cacheCollectionMock.Object);

            Assert.ThrowsAsync<HttpRequestException>(() => iTunesClient.GetAlbums("Iron+Maiden"));

            cacheCollectionMock.Verify(x => x.Exists(It.IsAny<Expression<Func<Cache, bool>>>()), Times.Once);
            cacheCollectionMock.Verify(x => x.FindById(It.IsAny<BsonValue>()), Times.Never);
        }

        private Mock<ILiteCollection<Cache>> GetCollectionMock(Cache returnElement = null, bool withExistElement = false)
        {
            var cacheCollectionMock = new Mock<ILiteCollection<Cache>>();
            cacheCollectionMock.Setup(x => x.Exists(It.IsAny<Expression<Func<Cache, bool>>>()))
                .Returns(withExistElement);

            if (returnElement != null)
            {
                cacheCollectionMock.Setup(x => x.Upsert(It.Is<Cache>(x => x != null && x.Equals(returnElement))))
                    .Returns(true);
                cacheCollectionMock.Setup(x => x.FindById(It.Is<BsonValue>(x => x == new BsonValue(returnElement.Key))))
                    .Returns(returnElement);
            }                

            return cacheCollectionMock;
        }

        private FallbackHandler GetHandler(ILiteCollection<Cache> collection)
            => new FallbackHandler(collection);

        private ITunesClient GetClient(ILiteCollection<Cache> collection, string url = null)
        {
            var handler = GetHandler(collection);
            var httpClient = new HttpClient(handler) { BaseAddress = new Uri(url ?? "https://test-itunes.com") };

            return new ITunesClient(httpClient);
        }
    }
}