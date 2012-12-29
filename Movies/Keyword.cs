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
    /// Keywords for this movie
    /// </summary>
    public class Keyword : ITmdbObject
    {
        #region properties
        /// <summary>
        /// Id of the keyword
        /// </summary>
        public int Id { get; set; }
        /// <summary>
        /// Name of the keyword
        /// </summary>
        public string Name { get; set; }
        #endregion

        #region interface implementations
        void ITmdbObject.ProcessJson(JsonObject jsonObject)
        {
            Id = (int)jsonObject.GetNamedValue("id").GetSafeNumber();
            Name = jsonObject.GetNamedValue("name").GetSafeString();
        }
        #endregion

        #region overrides
        /// <summary>
        /// Returns this instances ToString
        /// </summary>
        public override string ToString()
        {
            return Name;
        }
        #endregion
    }
}
