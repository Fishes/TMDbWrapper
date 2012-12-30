using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
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
            Request<Collection> request = new Request<Collection>("collection/" + CollectionId.ToString());
            return await request.ProcesRequestAsync();
        }

        /// <summary>
        /// Gets the images that are associated with the collection.
        /// </summary>
        /// <param name="CollectionId">Id of the collection</param>
        /// <returns>The image set of the collection.</returns>
        public static async Task<Images> GetCollectionImagesAsync(int CollectionId)
        {
            Request<Images> request = new Request<Images>("collection/" + CollectionId.ToString() + "/images");
            return await request.ProcesRequestAsync();
        }
    }
}
