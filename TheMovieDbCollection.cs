using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TmdbWrapper.Cache;
using TmdbWrapper.Collections;
using TmdbWrapper.Image;
using TmdbWrapper.Utilities;

namespace TmdbWrapper
{
    public static partial class TheMovieDb
    {
        /// <summary>
        /// Get a specific collection
        /// </summary>
        /// <param name="CollectionId">Id of the collection</param>
        /// <returns>The collection</returns>
        public static async Task<Collection> GetCollectionAsync(int CollectionId)
        {
            Collection collection = DatabaseCache.GetObject<Collection>(CollectionId);
            if (collection != null)
            {
                Request<Collection> request = new Request<Collection>("collection/" + CollectionId.ToString());
                collection = await request.ProcesRequestAsync();
                DatabaseCache.SetObject(CollectionId, collection);
            }
            return collection;
        }

        /// <summary>
        /// Gets the images that are associated with the collection.
        /// </summary>
        /// <param name="CollectionId">Id of the collection</param>
        /// <returns>The image set of the collection.</returns>
        public static async Task<Images> GetCollectionImagesAsync(int CollectionId)
        {
            Images images = DatabaseCache.GetObject<Images>(CollectionId);
            if (images != null)
            {
                Request<Images> request = new Request<Images>("collection/" + CollectionId.ToString() + "/images");
                images = await request.ProcesRequestAsync();
                DatabaseCache.SetObject(CollectionId, images);
            }
            return images;
        }
    }
}
