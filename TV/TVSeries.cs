using System;
using System.Collections.Generic;
using TmdbWrapper.Movies;
using TmdbWrapper.Utilities;

namespace TmdbWrapper.TV
{
    /// <summary>
    /// A tv series
    /// </summary>
    public class TVSeries : ITmdbObject
    {
        /// <summary>
        /// Path to the backdrop image
        /// </summary>
        public string BackdropPath { get; private set; }
        /// <summary>
        /// A list of creators
        /// </summary>
        public IReadOnlyList<Creator> CreatedBy { get; private set; }
        /// <summary>
        /// A list of runtimes
        /// </summary>
        public IReadOnlyList<int> EpisodeRuntime { get; private set; }
        /// <summary>
        /// The date of the first broadcast
        /// </summary>
        public DateTime? FirstAirDate { get; private set; }
        /// <summary>
        /// A list of genres
        /// </summary>
        public IReadOnlyList<Genre> Genres {get; private set;}
        /// <summary>
        /// The adres of the homepage
        /// </summary>
        public Uri Homepage { get; private set; }
        /// <summary>
        /// The id of the series
        /// </summary>
        public int Id { get; private set; }
        /// <summary>
        /// A boolean indicating if the series is in production
        /// </summary>
        public bool InProduction { get; private set; }
        /// <summary>
        /// A list of spoken languages
        /// </summary>
        public IReadOnlyList<string> Languages { get; private set;}
        /// <summary>
        /// The broadcast date of the last episode
        /// </summary>
        public DateTime? LastAirDate { get; private set; }
        /// <summary>
        /// The name of the tv series
        /// </summary>
        public string Name { get; private set; }
        /// <summary>
        /// A list of networks that broadcasted the series
        /// </summary>
        public IReadOnlyList<Network> Networks { get; private set; }
        /// <summary>
        /// The total number of broadcasted episodes
        /// </summary>
        public int NumberOfEpisodes { get; private set; }
        /// <summary>
        /// The total number of broadcasted seasons
        /// </summary>
        public int NumberOfSeasons { get; private set; }
        /// <summary>
        /// The original name of the series
        /// </summary>
        public string OriginalName { get; private set; }
        /// <summary>
        /// A list of countries where the series originated.
        /// </summary>
        public IReadOnlyList<string> OriginCountry { get; private set; }
        /// <summary>
        /// A overview of the series plot/setting
        /// </summary>
        public string Overview { get; private set; }
        /// <summary>
        /// The popularity ranking
        /// </summary>
        public int Popularity { get; private set; }
        /// <summary>
        /// The path to the poster image
        /// </summary>
        public string PosterPath { get; private set; }
        /// <summary>
        /// A list of production companies
        /// </summary>
        public IReadOnlyList<ProductionCompany> ProductionCompanies { get; private set; }
        /// <summary>
        /// A list of season summaries
        /// </summary>
        public IReadOnlyList<SeasonSummary> SeasonSummaries { get; private set; }
        /// <summary>
        /// The status of the series
        /// </summary>
        public string Status { get; private set; }
        /// <summary>
        /// The voting average
        /// </summary>
        public double VoteAverage { get; private set; }
        /// <summary>
        /// The number of votes
        /// </summary>
        public int VoteCount { get; private set; }

        #region Overrides
        /// <summary>
        /// Returns the name of the series
        /// </summary>
        /// <returns>The name</returns>
        public override string ToString()
        {
            return Name;
        }
        #endregion

        void ITmdbObject.ProcessJson(JSONObject jsonObject)
        {
            BackdropPath = jsonObject.GetSafeString("backdrop_path");
            CreatedBy = jsonObject.ProcessObjectArray<Creator>("created_by");
            EpisodeRuntime = jsonObject.ProcessIntArray("episode_run_time");
            FirstAirDate = jsonObject.GetSafeDateTime("first_air_date");
            Genres = jsonObject.ProcessObjectArray<Genre>("genres");
            Homepage = jsonObject.GetSafeUri("homepage");
            Id = (int)jsonObject.GetSafeNumber("id");
            InProduction = jsonObject.GetSafeBoolean("in_production");
            Languages = jsonObject.ProcessStringArray("languages");
            LastAirDate = jsonObject.GetSafeDateTime("last_air_date");
            Name = jsonObject.GetSafeString("name");
            Networks = jsonObject.ProcessObjectArray<Network>("networks");
            NumberOfEpisodes = (int)jsonObject.GetSafeNumber("number_of_episodes");
            NumberOfSeasons = (int)jsonObject.GetSafeNumber("number_of_seasons");
            OriginalName = jsonObject.GetSafeString("original_name");
            OriginCountry = jsonObject.ProcessStringArray("origin_country");
            Overview = jsonObject.GetSafeString("overview");
            Popularity = (int)jsonObject.GetSafeNumber("popularity");
            PosterPath = jsonObject.GetSafeString("poster_path");
            ProductionCompanies = jsonObject.ProcessObjectArray<ProductionCompany>("production_companies");
            SeasonSummaries = jsonObject.ProcessObjectArray<SeasonSummary>("seasons");
            Status = jsonObject.GetSafeString("status");
            VoteAverage = jsonObject.GetSafeNumber("vote_average");
            VoteCount = (int)jsonObject.GetSafeNumber("vote_count");
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

        /// <summary>
        /// Uri to the backdrop image.
        /// </summary>
        /// <param name="size">The size for the image as required</param>
        /// <returns>The uri to the sized image</returns>
        public Uri Uri(BackdropSize size)
        {
            return Extensions.MakeImageUri(size.ToString(), BackdropPath);
        }
        #endregion


    }
}
