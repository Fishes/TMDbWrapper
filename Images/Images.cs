using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TmdbWrapper.Utilities;
using Windows.Data.Json;

namespace TmdbWrapper.Image
{
    /// <summary>
    /// Images of a movie
    /// </summary>
    public class Images : ITmdbObject
    {
        #region properties
        /// <summary>
        /// Id of the movie
        /// </summary>
        public int Id { get; private set; }
        /// <summary>
        /// List of backdrops
        /// </summary>
        public IReadOnlyList<Backdrop> Backdrops { get; private set; }
        /// <summary>
        /// List of posters
        /// </summary>
        public IReadOnlyList<Poster> Posters { get; private set; }
        #endregion

        #region interface implementations
        void ITmdbObject.ProcessJson(JsonObject jsonObject)
        {
            Id = (int)jsonObject.GetNamedValue("id").GetSafeNumber();
            Backdrops = jsonObject.GetNamedValue("backdrops").ProcessArray<Backdrop>();
            Posters = jsonObject.GetNamedValue("posters").ProcessArray<Poster>();
        }
        #endregion
    }
}
