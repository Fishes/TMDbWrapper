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
    /// Cast of this movie.
    /// </summary>
    public class Credits : ITmdbObject
    {
        #region properties
        /// <summary>
        /// Id of the movie of this cast.
        /// </summary>
        public int Id { get; set; }
        /// <summary>
        /// Cast of movie
        /// </summary>
        public IList<CastPerson> Cast { get; set; }
        /// <summary>
        /// Crew of this movie.
        /// </summary>
        public IList<CastPerson> Crew { get; set; }
        #endregion

        #region interface implementations

        void ITmdbObject.ProcessJson(JsonObject jsonObject)
        {
            Id = (int)jsonObject.GetNamedValue("id").GetSafeNumber();
            Cast = jsonObject.GetNamedValue("cast").ProcessArray<CastPerson>();
            Crew = jsonObject.GetNamedValue("crew").ProcessArray<CastPerson>();
        }
        #endregion
    }
}
