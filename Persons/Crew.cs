using System;
using System.Threading.Tasks;
using TmdbWrapper.Movies;
using TmdbWrapper.Utilities;

namespace TmdbWrapper.Persons
{
    /// <summary>
    /// Member of the crew.
    /// </summary>
    public class Crew : ITmdbObject
    {
        #region properties
        /// <summary>
        /// Id of the movie.
        /// </summary>
        public int Id { get; private set; }
        /// <summary>
        /// Title of the movie.
        /// </summary>
        public string Title { get; private set; }
        /// <summary>
        /// Name of the department.
        /// </summary>
        public string Department { get; private set; }
        /// <summary>
        /// Original title
        /// </summary>
        public string OriginalTitle { get; private set; }
        /// <summary>
        /// Name of the job.
        /// </summary>
        public string Job { get; private set; }
        /// <summary>
        /// Path of the poster of the movie.
        /// </summary>
        public string PosterPath { get; private set; }
        /// <summary>
        /// Date of the original release.
        /// </summary>
        public DateTime? ReleaseDate { get; private set; }
        /// <summary>
        /// Indictates if then movie is an adult title.
        /// </summary>
        public bool Adult { get; private set; }
        #endregion

        #region interface implementations
        void ITmdbObject.ProcessJson(JSONObject jsonObject)
        {
            Id = (int)jsonObject.GetSafeNumber("id");
            Title = jsonObject.GetSafeString("title");
            Department = jsonObject.GetSafeString("department");
            OriginalTitle = jsonObject.GetSafeString("original_title");
            Job = jsonObject.GetSafeString("job");
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
