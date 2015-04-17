using TmdbWrapper.Utilities;

namespace TmdbWrapper.Movies
{
    /// <summary>
    /// Trailer of this movie.
    /// </summary>
    public class Trailer : ITmdbObject
    {
        #region properties
        /// <summary>
        /// Name of the trailer
        /// </summary>
        public string Name { get; private set; }
        /// <summary>
        /// Size of the trailer.
        /// </summary>
        public string Size { get; private set; }
        /// <summary>
        /// Source of the trailer
        /// </summary>
        public string Source { get; private set; }
        #endregion

        #region interface implementations
        void ITmdbObject.ProcessJson(JSONObject jsonObject)
        {
            Name = jsonObject.GetSafeString("name");
            Size = jsonObject.GetSafeString("size");
            Source = jsonObject.GetSafeString("source");
        }
        #endregion

        #region overrides
        /// <summary>
        /// Returns this instance ToString
        /// </summary>
        public override string ToString()
        {
            return Name;
        }
        #endregion
    }
}
