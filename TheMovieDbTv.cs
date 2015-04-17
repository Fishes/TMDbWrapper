using System;
using System.Linq;
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
        Casts = 1        
    }

    public static partial class TheMovieDb
    {
        /// <summary>
        /// Gets a TvSeries by the movie database id.
        /// </summary>
        /// <param name="tvSeriesId">The id of the tv series</param>
        /// <param name="extra">Indicates which parts should be prefetched.</param>
        /// <returns>The specified tv series</returns>
        public static async Task<TVSeries> GetTvAsync(int tvSeriesId, TVSeriesExtras extra = 0)
        {
            var tvSeries = DatabaseCache.GetObject<TVSeries>(tvSeriesId);

            if(tvSeries == null){
                var request = new Request<TVSeries>("tv/" + tvSeriesId);

                if (!string.IsNullOrEmpty(Language))
                    request.AddParameter("language", Language);
                if (extra != 0)
                {
                    request.AddParameter("append_to_response", extra.ToString().Replace(" ", ""));
                }
                tvSeries = await request.ProcesRequestAsync();
                DatabaseCache.SetObject(tvSeriesId, tvSeries);
            }
            return tvSeries;
        }

        /// <summary>
        /// Gets a season of a tv series by the database id of the series and the sequence number of the season.
        /// </summary>
        /// <param name="tvSeriesId">The if of the tv series.</param>
        /// <param name="seasonNumber">The sequence number of the season.</param>
        /// <returns>The season</returns>
        public static async Task<Season> GetSeasonAsync(int tvSeriesId, int seasonNumber)
        {
            Season season = null;
            var series = DatabaseCache.GetObject<TVSeries>(tvSeriesId);
            if (series != null)
            {
                var summary = series.SeasonSummaries.FirstOrDefault(s => s.SeasonNumber == seasonNumber);
                if(summary != null){
                    season = DatabaseCache.GetObject<Season>(summary.Id);
                }
            }
            if (season == null)
            {
                var request = new Request<Season>("tv/" + tvSeriesId + "/season/" + seasonNumber);
                if (!string.IsNullOrEmpty(Language))
                    request.AddParameter("language", Language);
                season = await request.ProcesRequestAsync();
                DatabaseCache.SetObject(season.Id, season);
            }
            return season;
        }
    }
}
