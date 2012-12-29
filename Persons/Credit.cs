using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TmdbWrapper.Utilities;
using Windows.Data.Json;

namespace TmdbWrapper.Persons
{
    /// <summary>
    /// Credits for this person.
    /// </summary>
    public class Credit : ITmdbObject
    {
        #region properties
        /// <summary>
        /// List of the crew
        /// </summary>
        public IReadOnlyList<Crew> Crew { get; private set; }
        /// <summary>
        /// List of the cast
        /// </summary>
        public IReadOnlyList<Role> Cast { get; private set; }
        #endregion

        #region interface implementation
        void ITmdbObject.ProcessJson(JsonObject jsonObject)
        {
            Crew = jsonObject.GetNamedValue("crew").ProcessArray<Crew>();
            Cast = jsonObject.GetNamedValue("cast").ProcessArray<Role>();
        }
        #endregion
    }
}
