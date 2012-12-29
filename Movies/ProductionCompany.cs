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
    /// A production company
    /// </summary>
    public class ProductionCompany : ITmdbObject
    {
        /// <summary>
        /// Name of the production company
        /// </summary>
        public string Name { get; set; }
        /// <summary>
        /// Id of the production company
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public override string ToString()
        {
            return Name;
        }
        
        void ITmdbObject.ProcessJson(JsonObject jsonObject)
        {
            Id = (int)jsonObject.GetNamedValue("id").GetSafeNumber();
            Name = jsonObject.GetNamedValue("name").GetSafeString();
        }
    }
}
