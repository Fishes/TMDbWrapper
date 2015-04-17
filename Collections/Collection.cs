using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using TmdbWrapper.Images;
using TmdbWrapper.Utilities;

namespace TmdbWrapper.Collections
{
    /// <summary>
    /// A collection of movies that appear to belong together.
    /// </summary>
    public class Collection : ITmdbObject
    {
        #region properties
        /// <summary>
        /// Backdrop image path
        /// </summary>
        public string BackdropPath { get; private set; }
        /// <summary>
        /// Id of the collection
        /// </summary>
        public int Id { get; private set; }
        /// <summary>
        /// Poster image path
        /// </summary>
        public string PosterPath { get; private set; }
        /// <summary>
        /// Title of the collection
        /// </summary>
        public string Name { get; private set; }
        /// <summary>
        /// A list with all the parts of this collection
        /// </summary>
        public IReadOnlyList<Part> Parts { get; private set; }
        #endregion

        
        #region Overrides
        /// <summary>
        /// Returns this instance ToString
        /// </summary>
        public override string ToString()
        {
            return Name;
        }
        #endregion

        #region Interface implementations
        void ITmdbObject.ProcessJson(JSONObject jsonObject)
        {
            BackdropPath = jsonObject.GetSafeString("backdrop_path");
            Id = (int)jsonObject.GetSafeNumber("id");
            Name = jsonObject.GetSafeString("name");
            PosterPath = jsonObject.GetSafeString("poster_path");
            Parts = jsonObject.ProcessObjectArray<Part>("parts");         
        }
        #endregion

        #region Image Uri's
        /// <summary>
        /// Uri to the poster image.
        /// </summary>
        /// <param name="size">The size for the image as required</param>
        /// <returns>The uri to the sized image</returns>
        public Uri Uri(PosterSize size)
        {
            return Extensions.MakeImageUri(size.ToString(), PosterPath);
        }

        /// <summary>
        /// Uri to the backdrop image.
        /// </summary>
        /// <param name="size">The size for the image as required</param>
        /// <returns>The uri to the sized image</returns>
        public Uri Uri(BackdropSize size)
        {
            return Extensions.MakeImageUri(size.ToString(), BackdropPath);
        }
        #endregion

        #region navigation properties

        /// <summary>
        /// Gets the images of this collection
        /// </summary>
        /// <returns>The image set of this collection.</returns>
        public async Task<Image> ImagesAsync()
        {
            return await TheMovieDb.GetCollectionImagesAsync(Id);
        }
        #endregion
    }
}
