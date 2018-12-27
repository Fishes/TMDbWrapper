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
        public string Iso31661 { get; private set; }

        /// <summary>
        /// Alternative version of the title.
        /// </summary>
        public string Title { get; private set; }

        #endregion properties

        #region interface implementations

        void ITmdbObject.ProcessJson(JsonObject jsonObject)
        {
            Iso31661 = jsonObject.GetSafeString("iso_3166_1");
            Title = jsonObject.GetSafeString("title");
        }

        #endregion interface implementations

        #region overrides

        /// <summary>
        /// Returns this instance of ToString()
        /// </summary>
        public override string ToString()
        {
            return Title;
        }

        #endregion overrides
    }
}