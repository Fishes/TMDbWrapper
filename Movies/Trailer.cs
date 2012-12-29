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
    /// Trailer of this movie.
    /// </summary>
    public class Trailer : ITmdbObject
    {
        #region properties
        /// <summary>
        /// Name of the trailer
        /// </summary>
        public string Name { get; set; }
        /// <summary>
        /// Size of the trailer.
        /// </summary>
        public string Size { get; set; }
        /// <summary>
        /// Source of the trailer
        /// </summary>
        public string Source { get; set; }
        #endregion

        #region interface implementations
        void ITmdbObject.ProcessJson(JsonObject jsonObject)
        {
            Name = jsonObject.GetNamedValue("name").GetSafeString();
            Size = jsonObject.GetNamedValue("size").GetSafeString();
            Source = jsonObject.GetNamedValue("source").GetSafeString();
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
