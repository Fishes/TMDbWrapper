using System;
using TmdbWrapper.Utilities;

namespace TmdbWrapper.Search
{
    /// <summary>
    /// Summary of found tv series
    /// </summary>
    public class TVSummary : ITmdbObject
    {
        #region properties
        /// <summary>
        /// Path of the backdrop image
        /// </summary>
        public string BackdropPath { get; private set; }
        /// <summary>
        /// Id of this series
        /// </summary>
        public int Id { get; private set; }
        /// <summary>
        /// Original name of this series.
        /// </summary>
        public string OriginalName { get; private set; }
        /// <summary>
        /// Original date of first broadcast.
        /// </summary>
        public DateTime? FirstAirDate { get; private set; }
        /// <summary>
        /// Path of the poster for this series.
        /// </summary>
        public string PosterPath { get; private set; }
        /// <summary>
        /// Popularity of this series.
        /// </summary>
        public double Popularity { get; private set; }
        /// <summary>
        /// Name of this series.
        /// </summary>
        public string Name { get; private set; }
        /// <summary>
        /// Average of the votes.
        /// </summary>
        public double VoteAverage { get; private set; }
        /// <summary>
        /// Number of votes cast.
        /// </summary>
        public int VoteCount { get; private set; }
        #endregion

        #region overrides
        /// <summary>
        /// Returns the ToString of this instance.
        /// </summary>
        public override string ToString()
        {
            return Name;
        }
        #endregion

        #region interface implementations
        void ITmdbObject.ProcessJson(JSONObject jsonObject)
        {
            BackdropPath = jsonObject.GetSafeString("backdrop_path");
            Id = (int)jsonObject.GetSafeNumber("id");
            FirstAirDate = jsonObject.GetSafeDateTime("first_air_date");
            OriginalName = jsonObject.GetSafeString("original_name");
            PosterPath = jsonObject.GetSafeString("poster_path");
            Popularity = jsonObject.GetSafeNumber("popularity");
            Name = jsonObject.GetSafeString("name");
            VoteAverage = jsonObject.GetSafeNumber("vote_average");
            VoteCount = (int)jsonObject.GetSafeNumber("vote_count");
        }
        #endregion

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
        #endregion

        #region Navigation properties
        //// <summary>
        //// Retrieves the associated movie.
        //// </summary>        
        ////public async Task<Movies.Movie> MovieAsync()
        ////{
        ////    return await TheMovieDb.GetMovieAsync(Id);
        ////}
        #endregion
    }
}
