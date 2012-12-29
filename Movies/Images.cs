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
    /// Images of a movie
    /// </summary>
    public class Images : ITmdbObject
    {
        /// <summary>
        /// Id of the movie
        /// </summary>
        public int Id { get; set; }
        /// <summary>
        /// List of backdrops
        /// </summary>
        public IList<Image> Backdrops { get; set; }
        /// <summary>
        /// List of posters
        /// </summary>
        public IList<Image> Posters { get; set; }
    
        void ITmdbObject.ProcessJson(JsonObject jsonObject)
        {
            Id = (int)jsonObject.GetNamedValue("id").GetSafeNumber();
            Backdrops = jsonObject.GetNamedValue("backdrops").ProcessArray<Image>();
            Posters = jsonObject.GetNamedValue("posters").ProcessArray<Image>();            
        }
    }
}
