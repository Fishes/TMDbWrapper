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
        public string Iso3166_1 { get; private set; }
        /// <summary>
        /// Alternative version of the title.
        /// </summary>
        public string Title { get; private set; }
        #endregion

        #region interface implementations
        void ITmdbObject.ProcessJson(JSONObject jsonObject)
        {
            Iso3166_1 = jsonObject.GetSafeString("iso_3166_1");
            Title = jsonObject.GetSafeString("title");            
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
