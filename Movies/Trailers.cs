using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TmdbWrapper.Utilities;
using Windows.Data.Json;

namespace TmdbWrapper.Movies
{
    /// <summary>
    /// Collection of trailers for a movie
    /// </summary>
    public class Trailers : ITmdbObject
    {
        #region properties
        /// <summary>
        /// Id of the movie.
        /// </summary>
        public int Id { get; private set; }
        /// <summary>
        /// List of trailers hosted by quicktime.
        /// </summary>
        public IReadOnlyList<Trailer> QuickTime { get; private set; }
        /// <summary>
        /// List of trailers hoster by youtube.
        /// </summary>
        public IReadOnlyList<Trailer> Youtube { get; private set; }
        #endregion

        #region interface implementations
        void ITmdbObject.ProcessJson(JsonObject jsonObject)
        {
            Id = (int)jsonObject.GetSafeNumber("id");
            QuickTime = jsonObject.ProcessArray<Trailer>("quicktime");
            Youtube = jsonObject.ProcessArray<Trailer>("youtube");
        }
        #endregion
    }
}
