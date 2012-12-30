using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TmdbWrapper.Utilities;
using Windows.Data.Json;

namespace TmdbWrapper.Collections
{
    /// <summary>
    /// A part of a collection
    /// </summary>
    public class Part : ITmdbObject
    {
        #region properties
        /// <summary>
        /// Backdrop image path
        /// </summary>
        public string BackdropPath { get; private set; }
        /// <summary>
        /// Id of the movie
        /// </summary>
        public int Id { get; private set; }
        /// <summary>
        /// Poster image path
        /// </summary>
        public string PosterPath { get; private set; }
        /// <summary>
        /// Release date of the movie
        /// </summary>
        public string ReleaseDate { get; private set; }
        /// <summary>
        /// Title of the movie
        /// </summary>
        public string Title { get; private set; }
        #endregion

        #region overrides
        /// <summary>
        /// Returns this instance ToString
        /// </summary>
        public override string ToString()
        {
            return Title;
        }
        #endregion

        #region Interface implementations
        void ITmdbObject.ProcessJson(JsonObject jsonObject)
        {
            BackdropPath = jsonObject.GetNamedValue("backdrop_path").GetSafeString();
            Id = (int)jsonObject.GetNamedValue("id").GetSafeNumber();
            PosterPath = jsonObject.GetNamedValue("poster_path").GetSafeString();
            ReleaseDate = jsonObject.GetNamedValue("release_date").GetSafeString();
            Title = jsonObject.GetNamedValue("title").GetSafeString();
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

        #region navigation properties
        /// <summary>
        /// Retrieves the associated movie.
        /// </summary>        
        public async Task<Movies.Movie> MovieAsync()
        {
            return await TheMovieDb.GetMovieAsync(Id);
        }
        #endregion
    }
}
