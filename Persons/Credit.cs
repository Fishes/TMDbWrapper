using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TmdbWrapper.Utilities;

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
        void ITmdbObject.ProcessJson(JSONObject jsonObject)
        {
            Crew = jsonObject.ProcessArray<Crew>("crew");
            Cast = jsonObject.ProcessArray<Role>("cast");
        }
        #endregion
    }
}
