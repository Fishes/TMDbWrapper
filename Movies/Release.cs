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
    /// Release of this movie
    /// </summary>
    public class Release : ITmdbObject
    {
        #region properties
        /// <summary>
        /// Country of this release
        /// </summary>
        public string Iso3166_1 { get; set; }
        /// <summary>
        /// Certification of this release.
        /// </summary>
        public string Certification { get; set; }
        /// <summary>
        /// Date of this release.
        /// </summary>
        public string ReleaseDate { get; set; }
        #endregion

        #region interface implementations
        void ITmdbObject.ProcessJson(JsonObject jsonObject)
        {
            Iso3166_1 = jsonObject.GetNamedValue("iso_3166_1").GetSafeString();
            Certification = jsonObject.GetNamedValue("certification").GetSafeString();
            ReleaseDate = jsonObject.GetNamedValue("release_date").GetSafeString();
        }
        #endregion

        #region overrides
        /// <summary>
        /// Returns this instances ToString.
        /// </summary>
        public override string ToString()
        {
            return Iso3166_1;
        }
        #endregion
    }
}
