using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TmdbWrapper.Cache;
using TmdbWrapper.TV;
using TmdbWrapper.Utilities;

namespace TmdbWrapper
{
    /// <summary>
    /// Enumeration of extras that should be prefilled on retrieving the movie info
    /// </summary>
    [Flags]
    public enum TVSeriesExtras
    {
        /// <summary>
        /// Retrieve the cast
        /// </summary>
        casts = 1        
    }

    public static partial class TheMovieDb
    {
        /// <summary>
        /// Gets a TvSeries by the movie database id.
        /// </summary>
        /// <param name="TVSeriesID">The id of the tv series</param>
        /// <param name="extra">Indicates which parts should be prefetched.</param>
        /// <returns>The specified tv series</returns>
        public static async Task<TVSeries> GetTvAsync(int TVSeriesID, TVSeriesExtras extra = 0)
        {
            TVSeries tvSeries = DatabaseCache.GetObject<TVSeries>(TVSeriesID);

            if(tvSeries == null){
                Request<TVSeries> request = new Request<TVSeries>("tv/" + TVSeriesID.ToString());

                if (!string.IsNullOrEmpty(Language))
                    request.AddParameter("language", Language);
                if (extra != 0)
                {
                    request.AddParameter("append_to_response", extra.ToString().Replace(" ", ""));
                }
                tvSeries = await request.ProcesRequestAsync();
                DatabaseCache.SetObject(TVSeriesID, tvSeries);
            }
            return tvSeries;
        }

        /// <summary>
        /// Gets a season of a tv series by the database id of the series and the sequence number of the season.
        /// </summary>
        /// <param name="TVSeriesID">The if of the tv series.</param>
        /// <param name="SeasonNumber">The sequence number of the season.</param>
        /// <returns>The season</returns>
        public static async Task<Season> GetSeasonAsync(int TVSeriesID, int SeasonNumber)
        {
            Season season = null;
            TVSeries series = DatabaseCache.GetObject<TVSeries>(TVSeriesID);
            if (series != null)
            {
                SeasonSummary summary = series.SeasonSummaries.FirstOrDefault(s => s.SeasonNumber == SeasonNumber);
                if(summary != null){
                    season = DatabaseCache.GetObject<Season>(summary.Id);
                }
            }
            if (season == null)
            {
                Request<Season> request = new Request<Season>("tv/" + TVSeriesID.ToString() + "/season/" + SeasonNumber.ToString());
                if (!string.IsNullOrEmpty(Language))
                    request.AddParameter("language", Language);
                season = await request.ProcesRequestAsync();
                DatabaseCache.SetObject(season.Id, season);
            }
            return season;
        }
    }
}
