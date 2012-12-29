using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
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
        public int Id { get; set; }
        /// <summary>
        /// Name of the collection
        /// </summary>
        public string Name { get; set; }
        /// <summary>
        /// Path to the poster
        /// </summary>
        public string PosterPath { get; set; }
        /// <summary>
        /// Path to the backdrop
        /// </summary>
        public string BackdropPath { get; set; }
        #endregion

        #region interface implementations

        void ITmdbObject.ProcessJson(Windows.Data.Json.JsonObject jsonObject)
        {
            Id = (int)jsonObject.GetNamedValue("id").GetSafeNumber();
            Name = jsonObject.GetNamedValue("name").GetSafeString();
            PosterPath = jsonObject.GetNamedValue("poster_path").GetSafeString();
            BackdropPath = jsonObject.GetNamedValue("backdrop_path").GetSafeString();
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
    }
}
