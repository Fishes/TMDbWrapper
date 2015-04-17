using System;
using TmdbWrapper.Utilities;

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
        public string Iso3166_1 { get; private set; }
        /// <summary>
        /// Certification of this release.
        /// </summary>
        public string Certification { get; private set; }
        /// <summary>
        /// Date of this release.
        /// </summary>
        public DateTime? ReleaseDate { get; private set; }
        #endregion

        #region interface implementations
        void ITmdbObject.ProcessJson(JSONObject jsonObject)
        {
            Iso3166_1 = jsonObject.GetSafeString("iso_3166_1");
            Certification = jsonObject.GetSafeString("certification");
            ReleaseDate = jsonObject.GetSafeDateTime("release_date");
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
