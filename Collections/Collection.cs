using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TmdbWrapper.Image;
using TmdbWrapper.Utilities;
using Windows.Data.Json;

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
        void ITmdbObject.ProcessJson(JsonObject jsonObject)
        {
            BackdropPath = jsonObject.GetNamedValue("backdrop_path").GetSafeString();
            Id = (int)jsonObject.GetNamedValue("id").GetSafeNumber();
            Name = jsonObject.GetNamedValue("name").GetSafeString();
            PosterPath = jsonObject.GetNamedValue("poster_path").GetSafeString();
            Parts = jsonObject.GetNamedValue("parts").ProcessArray<Part>();         
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
            return Utilities.Extensions.MakeImageUri(size.ToString(), PosterPath);
        }

        /// <summary>
        /// Uri to the backdrop image.
        /// </summary>
        /// <param name="size">The size for the image as required</param>
        /// <returns>The uri to the sized image</returns>
        public Uri Uri(BackdropSize size)
        {
            return Utilities.Extensions.MakeImageUri(size.ToString(), BackdropPath);
        }
        #endregion

        #region navigation properties

        /// <summary>
        /// Gets the images of this collection
        /// </summary>
        /// <returns>The image set of this collection.</returns>
        public async Task<Images> Images()
        {
            return await TheMovieDb.GetCollectionImages(Id);
        }
        #endregion
    }
}
