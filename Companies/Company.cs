using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TmdbWrapper.Search;
using TmdbWrapper.Utilities;
using Windows.Data.Json;

namespace TmdbWrapper.Companies
{
    /// <summary>
    /// A production company
    /// </summary>
    public class Company : ITmdbObject
    {
        #region properties
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
        #endregion

        #region interface implementations
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
        #endregion
        
        #region image uri's
        /// <summary>
        /// Uri to the logo image.
        /// </summary>
        /// <param name="size">The size for the image as required</param>
        /// <returns>The uri to the sized image</returns>
        public Uri Uri(LogoSize size)
        {
            return Utilities.Extensions.MakeImageUri(size.ToString(), LogoPath);
        }
        #endregion

        #region navigation properties
        /// <summary>
        /// Retrieves the credits for this company
        /// </summary>
        /// <param name="page">The request page of the search results, giving 0 will give all results.</param>
        /// <returns>A page of credits</returns>
        public async Task<SearchResult<MovieSummary>> Credits(int page = 1)
        {
            return await TheMovieDb.GetCompanyCredits(Id, page);
        }
        #endregion
    }
}
