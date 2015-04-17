using System.Threading.Tasks;
using TmdbWrapper.Cache;
using TmdbWrapper.Collections;
using TmdbWrapper.Images;
using TmdbWrapper.Utilities;

namespace TmdbWrapper
{
    public static partial class TheMovieDb
    {
        /// <summary>
        /// Get a specific collection
        /// </summary>
        /// <param name="collectionId">Id of the collection</param>
        /// <returns>The collection</returns>
        public static async Task<Collection> GetCollectionAsync(int collectionId)
        {
            var collection = DatabaseCache.GetObject<Collection>(collectionId);
            if (collection != null)
            {
                var request = new Request<Collection>("collection/" + collectionId);
                collection = await request.ProcesRequestAsync();
                DatabaseCache.SetObject(collectionId, collection);
            }
            return collection;
        }

        /// <summary>
        /// Gets the images that are associated with the collection.
        /// </summary>
        /// <param name="collectionId">Id of the collection</param>
        /// <returns>The image set of the collection.</returns>
        public static async Task<Image> GetCollectionImagesAsync(int collectionId)
        {
            var images = DatabaseCache.GetObject<Image>(collectionId);
            if (images != null)
            {
                var request = new Request<Image>("collection/" + collectionId + "/images");
                images = await request.ProcesRequestAsync();
                DatabaseCache.SetObject(collectionId, images);
            }
            return images;
        }
    }
}
