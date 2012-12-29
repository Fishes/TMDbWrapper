using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TmdbWrapper.Utilities;

namespace TmdbWrapper.Search
{
    /// <summary>
    /// Summary of the company in the search results.
    /// </summary>
    public class CompanySummary : ITmdbObject
    {
        /// <summary>
        /// Id of the company
        /// </summary>
        public int Id { get; set; }
        /// <summary>
        /// Path of the logo image.
        /// </summary>
        public string LogoPath { get; set; }
        /// <summary>
        /// Name of the company.
        /// </summary>
        public string Name { get; set; }
        
        void ITmdbObject.ProcessJson(Windows.Data.Json.JsonObject jsonObject)
        {
            Id = (int)jsonObject.GetNamedValue("id").GetSafeNumber();
            LogoPath = jsonObject.GetNamedValue("logo_path").GetSafeString();
            Name = jsonObject.GetNamedValue("name").GetSafeString();
        }

        /// <summary>
        /// Returns this instance ToString.
        /// </summary>
        public override string ToString()
        {
            return Name;
        }

        /// <summary>
        /// Uri to the logo image.
        /// </summary>
        /// <param name="size">The size for the image as required</param>
        /// <returns>The uri to the sized image</returns>
        public Uri Uri(LogoSize size)
        {
            return Utilities.Extensions.MakeImageUri(size.ToString(), LogoPath);
        }
    }
}
