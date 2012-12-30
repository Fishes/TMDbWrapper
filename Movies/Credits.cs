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
        public int Id { get; private set; }
        /// <summary>
        /// Cast of movie
        /// </summary>
        public IReadOnlyList<CastPerson> Cast { get; private set; }
        /// <summary>
        /// Crew of this movie.
        /// </summary>
        public IReadOnlyList<CastPerson> Crew { get; private set; }
        #endregion

        #region interface implementations

        void ITmdbObject.ProcessJson(JsonObject jsonObject)
        {
            Id = (int)jsonObject.GetSafeNumber("id");
            Cast = jsonObject.ProcessArray<CastPerson>("cast");
            Crew = jsonObject.ProcessArray<CastPerson>("crew");
        }
        #endregion
    }
}
