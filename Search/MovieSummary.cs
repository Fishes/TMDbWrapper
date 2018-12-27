using System;
using System.Threading.Tasks;
using TmdbWrapper.Movies;
using TmdbWrapper.Utilities;

namespace TmdbWrapper.Search
{
    /// <summary>
    /// Summary of a movie in the results of a search.
    /// </summary>
    public class MovieSummary : ITmdbObject
    {
        #region properties

        /// <summary>
        /// Indictas if this is an adult movie.
        /// </summary>
        public bool Adult { get; private set; }

        /// <summary>
        /// Path of the backdrop image
        /// </summary>
        public string BackdropPath { get; private set; }

        /// <summary>
        /// Id of this movie
        /// </summary>
        public int Id { get; private set; }

        /// <summary>
        /// Original title of this movie.
        /// </summary>
        public string OriginalTitle { get; private set; }

        /// <summary>
        /// Original date of this release.
        /// </summary>
        public DateTime? ReleaseDate { get; private set; }

        /// <summary>
        /// Path of the poster for this movie.
        /// </summary>
        public string PosterPath { get; private set; }

        /// <summary>
        /// Popularity of this movie.
        /// </summary>
        public double Popularity { get; private set; }

        /// <summary>
        /// Title of this movie.
        /// </summary>
        public string Title { get; private set; }

        /// <summary>
        /// Average of the votes.
        /// </summary>
        public double VoteAverage { get; private set; }

        /// <summary>
        /// Number of votes cast.
        /// </summary>
        public int VoteCount { get; private set; }

        #endregion properties

        #region overrides

        /// <summary>
        /// Returns the ToString of this instance.
        /// </summary>
        public override string ToString()
        {
            return Title;
        }

        #endregion overrides

        #region interface implementations

        void ITmdbObject.ProcessJson(JsonObject jsonObject)
        {
            Adult = jsonObject.GetSafeBoolean("adult");
            BackdropPath = jsonObject.GetSafeString("backdrop_path");
            Id = (int)jsonObject.GetSafeNumber("id");
            ReleaseDate = jsonObject.GetSafeDateTime("release_date");
            OriginalTitle = jsonObject.GetSafeString("original_title");
            PosterPath = jsonObject.GetSafeString("poster_path");
            Popularity = jsonObject.GetSafeNumber("popularity");
            Title = jsonObject.GetSafeString("title");
            VoteAverage = jsonObject.GetSafeNumber("vote_average");
            VoteCount = (int)jsonObject.GetSafeNumber("vote_count");
        }

        #endregion interface implementations

        #region Image uri's

        /// <summary>
        /// Uri to the poster image.
        /// </summary>
        /// <param name="size">The size for the image as required</param>
        /// <returns>The uri to the sized image</returns>
        public Uri Uri(PosterSize size = PosterSize.w342)
        {
            return Extensions.MakeImageUri(size.ToString(), PosterPath);
        }

        /// <summary>
        /// Uri to the backdrop image.
        /// </summary>
        /// <param name="size">The size for the image as required</param>
        /// <returns>The uri to the sized image</returns>
        public Uri Uri(BackdropSize size = BackdropSize.w300)
        {
            return Extensions.MakeImageUri(size.ToString(), BackdropPath);
        }

        #endregion Image uri's

        #region Navigation properties

        /// <summary>
        /// Retrieves the associated movie.
        /// </summary>
        public async Task<Movie> MovieAsync()
        {
            return await TheMovieDb.GetMovieAsync(Id);
        }

        #endregion Navigation properties
    }
}