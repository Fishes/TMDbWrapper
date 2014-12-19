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
    }
}
