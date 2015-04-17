using System.Collections.Generic;
using TmdbWrapper.Utilities;

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
        void ITmdbObject.ProcessJson(JSONObject jsonObject)
        {
            Id = (int)jsonObject.GetSafeNumber("id");
            QuickTime = jsonObject.ProcessObjectArray<Trailer>("quicktime");
            Youtube = jsonObject.ProcessObjectArray<Trailer>("youtube");
        }
        #endregion
    }
}
