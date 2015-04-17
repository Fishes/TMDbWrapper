using System;
using TmdbWrapper.Utilities;

namespace TmdbWrapper.TV
{
    /// <summary>
    /// A season summary of a TV Series
    /// </summary>
    public class SeasonSummary : ITmdbObject
    {
        /// <summary>
        /// The air date of the the season
        /// </summary>
        public DateTime? AirDate { get; private set; }
        /// <summary>
        /// The id of the season
        /// </summary>
        public int Id { get; private set; }
        /// <summary>
        /// The path to the poster of the season
        /// </summary>
        public string PosterPath { get; private set; }
        /// <summary>
        /// The sequence number of the season
        /// </summary>
        public int SeasonNumber { get; private set; }

        void ITmdbObject.ProcessJson(JSONObject jsonObject)
        {
            AirDate = jsonObject.GetSafeDateTime("air_date");
            Id = (int)jsonObject.GetSafeNumber("id");
            PosterPath = jsonObject.GetSafeString("poster_path");
            SeasonNumber = (int)jsonObject.GetSafeNumber("season_number");
        }

        #region Image Uri's
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
    }
}
