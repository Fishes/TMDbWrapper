using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.Data.Json;
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
        public bool Adult { get; set; }
        /// <summary>
        /// Path of the backdrop image
        /// </summary>
        public string BackdropPath { get; set; }
        /// <summary>
        /// Id of this movie
        /// </summary>
        public int Id { get; set; }
        /// <summary>
        /// Original title of this movie.
        /// </summary>
        public string OriginalTitle { get; set; }
        /// <summary>
        /// Original date of this release.
        /// </summary>
        public string ReleaseDate { get; set; }
        /// <summary>
        /// Path of the poster for this movie.
        /// </summary>
        public string PosterPath { get; set; }
        /// <summary>
        /// Popularity of this movie.
        /// </summary>
        public double Popularity { get; set; }
        /// <summary>
        /// Title of this movie.
        /// </summary>
        public string Title { get; set; }
        /// <summary>
        /// Average of the votes.
        /// </summary>
        public double VoteAverage { get; set; }
        /// <summary>
        /// Number of votes cast.
        /// </summary>
        public int VoteCount { get; set; }
        #endregion

        #region overrides
        /// <summary>
        /// Returns the ToString of this instance.
        /// </summary>
        public override string ToString()
        {
            return Title;
        }
        #endregion

        #region interface implementations
        void ITmdbObject.ProcessJson(JsonObject jsonObject)
        {
            Adult = jsonObject.GetNamedValue("adult").GetSafeBoolean();
            BackdropPath = jsonObject.GetNamedValue("backdrop_path").GetSafeString();
            Id = (int)jsonObject.GetNamedValue("id").GetSafeNumber();
            OriginalTitle = jsonObject.GetNamedValue("release_date").GetSafeString();
            PosterPath = jsonObject.GetNamedValue("poster_path").GetSafeString();
            Popularity = jsonObject.GetNamedValue("popularity").GetSafeNumber();
            Title = jsonObject.GetNamedValue("title").GetSafeString();
            VoteAverage = jsonObject.GetNamedValue("vote_average").GetSafeNumber();
            VoteCount = (int)jsonObject.GetNamedValue("vote_count").GetSafeNumber();
        }
        #endregion

        #region Image uri's
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

        #region Navigation properties
        /// <summary>
        /// Retrieves the associated movie.
        /// </summary>        
        public async Task<Movies.Movie> Movie()
        {
            return await TheMovieDb.GetMovie(Id);
        }
        #endregion
    }    
}
