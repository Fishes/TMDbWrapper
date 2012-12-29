using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.Data.Json;
using TmdbWrapper.Utilities;

namespace TmdbWrapper.Movies
{
    /// <summary>
    /// An alternative title.
    /// </summary>
    public class AlternativeTitle : ITmdbObject
    {
        #region properties
        /// <summary>
        /// Designation of the language.
        /// </summary>
        public string Iso3166_1 { get; set; }
        /// <summary>
        /// Alternative version of the title.
        /// </summary>
        public string Title { get; set; }
        #endregion

        #region interface implementations
        void ITmdbObject.ProcessJson(JsonObject jsonObject)
        {
            Iso3166_1 = jsonObject.GetNamedValue("iso_3166_1").GetSafeString();
            Title = jsonObject.GetNamedValue("title").GetSafeString();            
        }
        #endregion

        #region overrides
        /// <summary>
        /// Returns this instance of ToString()
        /// </summary>
        public override string ToString()
        {
            return Title;
        }
        #endregion
    }


    
}
