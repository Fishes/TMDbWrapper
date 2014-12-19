using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TmdbWrapper.Movies;
using TmdbWrapper.Utilities;

namespace TmdbWrapper.TV
{
    public class TVSeries : ITmdbObject
    {
        public string BackdropPath { get; private set; }

        public IReadOnlyList<Creator> CreatedBy { get; private set; }

        public IReadOnlyList<int> EpisodeRuntime { get; private set; }

        public string FirstAirDate { get; private set; }

        public IReadOnlyList<Genre> Genres {get; private set;}

        public Uri Homepage { get; private set; }

        public int Id { get; private set; }

        public bool InProduction { get; private set; }

        public IReadOnlyList<string> Languages { get; private set;}

        public string LastAirDate { get; private set; }

        public string Name { get; private set; }

        public IReadOnlyList<Network> Networks { get; private set; }

        public int NumberOfEpisodes { get; private set; }

        public int NumberOfSeasons { get; private set; }

        public string OriginalName { get; private set; }

        public IReadOnlyList<string> OriginCountry { get; private set; }

        public string Overview { get; private set; }

        public int Popularity { get; private set; }

        public string PosterPath { get; private set; }

        public IReadOnlyList<ProductionCompany> ProductionCompanies { get; private set; }

        public IReadOnlyList<Season> Seasons { get; private set; }

        public string Status { get; private set; }

        public int VoteAverage { get; private set; }

        public int VoteCount { get; private set; }

        #region Overrides

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
            FirstAirDate = jsonObject.GetSafeString("first_air_date");
            Genres = jsonObject.ProcessObjectArray<Genre>("genres");
            Homepage = jsonObject.GetSafeUri("homepage");
            Id = (int)jsonObject.GetSafeNumber("id");
            InProduction = jsonObject.GetSafeBoolean("in_production");
            Languages = jsonObject.ProcessStringArray("languages");
            LastAirDate = jsonObject.GetSafeString("last_air_date");
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
            Seasons = jsonObject.ProcessObjectArray<Season>("seasons");
            Status = jsonObject.GetSafeString("status");
            VoteAverage = (int)jsonObject.GetSafeNumber("vote_average");
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


    }
}
