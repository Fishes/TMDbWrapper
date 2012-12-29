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
        /// <summary>
        /// List of the crew
        /// </summary>
        public IList<Crew> Crew {get; set;}
        /// <summary>
        /// List of the cast
        /// </summary>
        public IList<Role> Cast {get; set;}

        void ITmdbObject.ProcessJson(JsonObject jsonObject)
        {
            Crew = jsonObject.GetNamedValue("crew").ProcessArray<Crew>();
            Cast = jsonObject.GetNamedValue("cast").ProcessArray<Role>();
        }
    }
}
