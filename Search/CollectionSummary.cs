using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TmdbWrapper.Utilities;
using Windows.Data.Json;

namespace TmdbWrapper.Search
{
    /// <summary>
    /// Summary for a collection
    /// </summary>
    public class CollectionSummary : ITmdbObject
    {
        /// <summary>
        /// Id of the collection.
        /// </summary>
        public int Id { get; set; }
        /// <summary>
        /// Path of the backdrop path.
        /// </summary>
        public string BackdropPath { get; set; }
        /// <summary>
        /// Name of the collection
        /// </summary>
        public string Name { get; set; }
        /// <summary>
        /// Path of the poster for this collection
        /// </summary>
        public string PosterPath { get; set; }

        /// <summary>
        /// Return this instances ToString
        /// </summary>
        /// <returns></returns>
        public override string ToString()
        {
            return Name;
        }
        
        void ITmdbObject.ProcessJson(JsonObject jsonObject)
        {
            Id = (int)jsonObject.GetNamedValue("id").GetSafeNumber();
            BackdropPath = jsonObject.GetNamedValue("backdrop_path").GetSafeString();
            Name = jsonObject.GetNamedValue("name").GetSafeString();
            PosterPath = jsonObject.GetNamedValue("poster_path").GetSafeString();
        }
    }
}
