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

        #endregion properties

        #region interface implementations

        void ITmdbObject.ProcessJson(JsonObject jsonObject)
        {
            Name = jsonObject.GetSafeString("name");
            Size = jsonObject.GetSafeString("size");
            Source = jsonObject.GetSafeString("source");
        }

        #endregion interface implementations

        #region overrides

        /// <summary>
        /// Returns this instance ToString
        /// </summary>
        public override string ToString()
        {
            return Name;
        }

        #endregion overrides
    }
}