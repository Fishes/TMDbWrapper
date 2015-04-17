using System;
using System.Collections.Generic;
using TmdbWrapper.Movies;
using TmdbWrapper.Utilities;

namespace TmdbWrapper.TV
{
    /// <summary>
    /// A episode of a tv series.
    /// </summary>
    public class Episode : ITmdbObject
    {
        /// <summary>
        /// Date of the first broadcast
        /// </summary>
        public DateTime? AirDate { get; private set; }
        /// <summary>
        /// List of all the crew members
        /// </summary>
        public IReadOnlyList<CrewPerson> Crew { get; private set; }
        /// <summary>
        /// Sequence number of the episode
        /// </summary>
        public int EpisodeNumber { get; private set; }
        /// <summary>
        /// List of all the guest stars
        /// </summary>
        public IReadOnlyList<CastPerson> GuestStars { get; private set; }
        /// <summary>
        /// Name of the episode
        /// </summary>
        public string Name { get; private set; }
        /// <summary>
        /// Summary of the episode
        /// </summary>
        public string Overview { get; private set; }
        /// <summary>
        /// Id of the episode
        /// </summary>
        public int Id { get; private set; }
        /// <summary>
        /// Production code of the episode
        /// </summary>
        public string ProductionCode { get; private set; }
        /// <summary>
        /// Sequence number of the season this episode was broadcast in.
        /// </summary>
        public int SeasonNumber { get; private set; }
        /// <summary>
        /// Path to the stils of this episodes
        /// </summary>
        public string StillPath { get; private set; }
        /// <summary>
        /// The average vote for this episode
        /// </summary>
        public double VoteAverage { get; private set; }
        /// <summary>
        /// The number of votes
        /// </summary>
        public int VoteCount { get; private set; }
        /// <summary>
        /// The name of the episode.
        /// </summary>
        /// <returns>The name.</returns>
        public override string ToString()
        {
            return Name;
        }
        
        void ITmdbObject.ProcessJson(JSONObject jsonObject)
        {
            AirDate = jsonObject.GetSafeDateTime("air_date");
            Crew = jsonObject.ProcessObjectArray<CrewPerson>("crew");
            EpisodeNumber = (int)jsonObject.GetSafeNumber("episode_number");
            GuestStars = jsonObject.ProcessObjectArray<CastPerson>("guest_stars");
            Name = jsonObject.GetSafeString("name");
            Overview = jsonObject.GetSafeString("overview");
            Id = (int)jsonObject.GetSafeNumber("id");
            ProductionCode = jsonObject.GetSafeString("production_code");
            SeasonNumber = (int)jsonObject.GetSafeNumber("season_number");
            StillPath = jsonObject.GetSafeString("still_path");
            VoteAverage = jsonObject.GetSafeNumber("vote_average");
            VoteCount = (int)jsonObject.GetSafeNumber("vote_count");
        }

        #region Image Uri's
        /// <summary>
        /// Uri to the still image.
        /// </summary>
        /// <param name="size">The size for the image as required</param>
        /// <returns>The uri to the sized image</returns>
        public Uri Uri(StillSize size)
        {
            return Extensions.MakeImageUri(size.ToString(), StillPath);
        }
        #endregion
    }
}
