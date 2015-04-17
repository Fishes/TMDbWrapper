using System;
using System.Threading.Tasks;
using TmdbWrapper.Collections;
using TmdbWrapper.Utilities;

namespace TmdbWrapper.Movies
{
    /// <summary>
    /// Collection that a movie belongs to.
    /// </summary>
    public class BelongsToCollection : ITmdbObject
    {
        #region properties
        /// <summary>
        /// Id of the collection
        /// </summary>
        public int Id { get; private set; }
        /// <summary>
        /// Name of the collection
        /// </summary>
        public string Name { get; private set; }
        /// <summary>
        /// Path to the poster
        /// </summary>
        public string PosterPath { get; private set; }
        /// <summary>
        /// Path to the backdrop
        /// </summary>
        public string BackdropPath { get; private set; }
        #endregion

        #region interface implementations

        void ITmdbObject.ProcessJson(JSONObject jsonObject)
        {
            Id = (int)jsonObject.GetSafeNumber("id");
            Name = jsonObject.GetSafeString("name");
            PosterPath = jsonObject.GetSafeString("poster_path");
            BackdropPath = jsonObject.GetSafeString("backdrop_path");
        }
        #endregion

        #region overrides
        /// <summary>
        /// Returns this instance ToString()
        /// </summary>
        public override string ToString()
        {
            return Name;
        }
        #endregion

        #region image uri's 
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
        /// Gets the collection
        /// </summary>
        /// <returns>The collection</returns>
        public async Task<Collection> CollectionAsync()
        {
            return await TheMovieDb.GetCollectionAsync(Id);
        }

        #endregion
    }
}
