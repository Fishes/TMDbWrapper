using System.Collections.Generic;
using TmdbWrapper.Utilities;

namespace TmdbWrapper.Persons
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
        public IReadOnlyList<CrewPerson> Crew { get; private set; }

        #endregion properties

        #region interface implementations

        void ITmdbObject.ProcessJson(JsonObject jsonObject)
        {
            Id = (int)jsonObject.GetSafeNumber("id");
            Cast = jsonObject.ProcessObjectArray<CastPerson>("cast");
            Crew = jsonObject.ProcessObjectArray<CrewPerson>("crew");
        }

        #endregion interface implementations
    }
}