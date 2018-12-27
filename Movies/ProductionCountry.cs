using TmdbWrapper.Utilities;

namespace TmdbWrapper.Movies
{
    /// <summary>
    /// A country where a movie was produced.
    /// </summary>
    public class ProductionCountry : ITmdbObject
    {
        #region properties

        /// <summary>
        /// Code of this country
        /// </summary>
        // ReSharper disable once InconsistentNaming
        public string Iso_3166_1 { get; private set; }

        /// <summary>
        /// Name of the contry.
        /// </summary>
        public string Name { get; private set; }

        #endregion properties

        #region overrides

        /// <summary>
        /// Return this instances ToString
        /// </summary>
        /// <returns></returns>
        public override string ToString()
        {
            return Name;
        }

        #endregion overrides

        #region interface implementations

        void ITmdbObject.ProcessJson(JsonObject jsonObject)
        {
            Name = jsonObject.GetSafeString("name");
            Iso_3166_1 = jsonObject.GetSafeString("iso_3166_1");
        }

        #endregion interface implementations
    }
}