using System;
using System.Collections.Generic;
using TmdbWrapper.Utilities;

namespace TmdbWrapper.TV
{
    /// <summary>
    /// A season of a tvseries
    /// </summary>
    public class Season : ITmdbObject
    {
        /// <summary>
        /// The first broadcast of the first episode
        /// </summary>
        public DateTime? AirDate { get; private set; }
        /// <summary>
        /// Episodes of the season
        /// </summary>
        public IReadOnlyList<Episode> EpisodeSummaries { get; private set; }
        /// <summary>
        /// The name of the season
        /// </summary>
        public string Name { get; private set; }
        /// <summary>
        /// A summary of the season
        /// </summary>
        public string Overview { get; private set; }
        /// <summary>
        /// Id of the season
        /// </summary>
        public int Id { get; private set; }
        /// <summary>
        /// Path to the poster for this season
        /// </summary>
        public string PosterPath { get; private set; }
        /// <summary>
        /// Sequence number of the season
        /// </summary>
        public int SeasonNumber { get; private set; }
        /// <summary>
        /// Returns the name of the season
        /// </summary>
        /// <returns>The name</returns>
        public override string ToString()
        {
            return Name;
        }

        void ITmdbObject.ProcessJson(JSONObject jsonObject)
        {
            AirDate = jsonObject.GetSafeDateTime("air_date");
            EpisodeSummaries = jsonObject.ProcessObjectArray<Episode>("episodes");
            Name = jsonObject.GetSafeString("name");
            Overview = jsonObject.GetSafeString("overview");
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
