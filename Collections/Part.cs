using System;
using System.Threading.Tasks;
using TmdbWrapper.Movies;
using TmdbWrapper.Utilities;

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
        public DateTime? ReleaseDate { get; private set; }

        /// <summary>
        /// Title of the movie
        /// </summary>
        public string Title { get; private set; }

        #endregion properties

        #region overrides

        /// <summary>
        /// Returns this instance ToString
        /// </summary>
        public override string ToString()
        {
            return Title;
        }

        #endregion overrides

        #region Interface implementations

        void ITmdbObject.ProcessJson(JsonObject jsonObject)
        {
            BackdropPath = jsonObject.GetSafeString("backdrop_path");
            Id = (int)jsonObject.GetSafeNumber("id");
            PosterPath = jsonObject.GetSafeString("poster_path");
            ReleaseDate = jsonObject.GetSafeDateTime("release_date");
            Title = jsonObject.GetSafeString("title");
        }

        #endregion Interface implementations

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

        #endregion image uri's

        #region navigation properties

        /// <summary>
        /// Retrieves the associated movie.
        /// </summary>
        public async Task<Movie> MovieAsync()
        {
            return await TheMovieDb.GetMovieAsync(Id);
        }

        #endregion navigation properties
    }
}