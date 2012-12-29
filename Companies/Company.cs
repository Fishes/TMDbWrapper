using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TmdbWrapper.Utilities;
using Windows.Data.Json;

namespace TmdbWrapper.Companies
{
    /// <summary>
    /// A production company
    /// </summary>
    public class Company : ITmdbObject
    {
        /// <summary>
        /// Discription of this company.
        /// </summary>
        public string Description { get; set; }
        /// <summary>
        /// Location of the headquarters of this company.
        /// </summary>
        public string Headquarters { get; set; }
        /// <summary>
        /// Uri for the homepage of this company.
        /// </summary>
        public Uri Homepage { get; set; }
        /// <summary>
        /// Id of this company
        /// </summary>
        public int Id { get; set; }
        /// <summary>
        /// Path of the logo for this company
        /// </summary>
        public string LogoPath { get; set; }
        /// <summary>
        /// Name of this company.
        /// </summary>
        public string Name { get; set; }
        /// <summary>
        /// Id of the parentcompany.
        /// </summary>
        public int ParentCompany { get; set; }
        
        void ITmdbObject.ProcessJson(JsonObject jsonObject)
        {
            Description = jsonObject.GetNamedValue("description").GetSafeString();
            Headquarters = jsonObject.GetNamedValue("headquarters").GetSafeString();
            Homepage = new Uri(jsonObject.GetNamedValue("homepage").GetSafeString());
            Id = (int)jsonObject.GetNamedValue("id").GetSafeNumber();
            LogoPath = jsonObject.GetNamedValue("logo_path").GetSafeString();
            Name = jsonObject.GetNamedValue("name").GetSafeString();
            ParentCompany = (int)jsonObject.GetNamedValue("parent_company").GetSafeNumber();
        }
    }
}
