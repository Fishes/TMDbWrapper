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
        #region properties
        /// <summary>
        /// Id of the company
        /// </summary>
        public int Id { get; private set; }
        /// <summary>
        /// Path of the logo image.
        /// </summary>
        public string LogoPath { get; private set; }
        /// <summary>
        /// Name of the company.
        /// </summary>
        public string Name { get; private set; }
        #endregion

        #region interface implementations
        void ITmdbObject.ProcessJson(Windows.Data.Json.JsonObject jsonObject)
        {
            Id = (int)jsonObject.GetSafeNumber("id");
            LogoPath = jsonObject.GetSafeString("logo_path");
            Name = jsonObject.GetSafeString("name");
        }
        #endregion

        #region overrides
        /// <summary>
        /// Returns this instance ToString.
        /// </summary>
        public override string ToString()
        {
            return Name;
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
        /// Retrieves the associated company.
        /// </summary>
        public async Task<Companies.Company> CompanyAsync()
        {
            return await TheMovieDb.GetCompanyAsync(Id);
        }
        #endregion
    }
}
