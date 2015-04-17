using System;
using System.Threading.Tasks;
using TmdbWrapper.Movies;
using TmdbWrapper.Utilities;

namespace TmdbWrapper.Persons
{
    /// <summary>
    /// Role in a movie
    /// </summary>
    public class Role : ITmdbObject
    {
        #region properties
        /// <summary>
        /// Id of the movie
        /// </summary>
        public int Id { get; private set; }
        /// <summary>
        /// Title of the movie
        /// </summary>
        public string Title { get; private set; }
        /// <summary>
        /// Name of the character.
        /// </summary>
        public string Character { get; private set; }
        /// <summary>
        /// Original title of the movie.
        /// </summary>
        public string OriginalTitle { get; private set; }
        /// <summary>
        /// Path of the poster image.
        /// </summary>
        public string PosterPath { get; private set; }
        /// <summary>
        /// Release date of the movie.
        /// </summary>
        public DateTime? ReleaseDate { get; private set; }
        /// <summary>
        /// Indicates if the title is an adult movie.
        /// </summary>
        public bool Adult { get; private set; }
        #endregion

        #region interface implementations
        void ITmdbObject.ProcessJson(JSONObject jsonObject)
        {
            Id = (int)jsonObject.GetSafeNumber("id");
            Title = jsonObject.GetSafeString("title");
            Character = jsonObject.GetSafeString("character");
            OriginalTitle = jsonObject.GetSafeString("original_title");
            PosterPath = jsonObject.GetSafeString("poster_path");
            ReleaseDate = jsonObject.GetSafeDateTime("release_date");
            Adult = jsonObject.GetSafeBoolean("adult");
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
        #endregion

        #region navigation properties
        /// <summary>
        /// Retrieves the associated movie.
        /// </summary>
        public async Task<Movie> MovieAsync()
        {
            return await TheMovieDb.GetMovieAsync(Id);
        }
        #endregion
    }
}
