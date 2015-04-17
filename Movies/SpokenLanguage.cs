using TmdbWrapper.Utilities;

namespace TmdbWrapper.Movies
{
    /// <summary>
    /// Spoken language
    /// </summary>
    public class SpokenLanguage : ITmdbObject
    {
        #region properties
        /// <summary>
        /// Code of the language.
        /// </summary>
        public string Iso639_1 { get; private set; }
        /// <summary>
        /// Name of the language.
        /// </summary>
        public string Name { get; private set; }
        /// <summary>
        /// The english name of the language.
        /// </summary>
        public string EnglishName { get; private set; }
        #endregion

        #region overrides
        /// <summary>
        /// Returns of this instance the ToString
        /// </summary>
        public override string ToString()
        {
            return Name;
        }
        #endregion

        #region interface implementations
        void ITmdbObject.ProcessJson(JSONObject jsonObject)
        {
            Iso639_1 = jsonObject.GetSafeString("iso_639_1");
            Name = jsonObject.GetSafeString("name");
            EnglishName = jsonObject.GetSafeString("english_name");
        }
        #endregion
    }
}
