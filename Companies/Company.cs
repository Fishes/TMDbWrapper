using System;
using System.Threading.Tasks;
using TmdbWrapper.Search;
using TmdbWrapper.Utilities;

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
        public string Description { get; private set; }
        /// <summary>
        /// Location of the headquarters of this company.
        /// </summary>
        public string Headquarters { get; private set; }
        /// <summary>
        /// Uri for the homepage of this company.
        /// </summary>
        public Uri Homepage { get; private set; }
        /// <summary>
        /// Id of this company
        /// </summary>
        public int Id { get; private set; }
        /// <summary>
        /// Path of the logo for this company
        /// </summary>
        public string LogoPath { get; private set; }
        /// <summary>
        /// Name of this company.
        /// </summary>
        public string Name { get; private set; }
        /// <summary>
        /// Id of the parentcompany.
        /// </summary>
        public int ParentCompany { get; private set; }
        #endregion

        #region interface implementations
        void ITmdbObject.ProcessJson(JSONObject jsonObject)
        {
            Description = jsonObject.GetSafeString("description");
            Headquarters = jsonObject.GetSafeString("headquarters");
            Homepage = jsonObject.GetSafeUri("homepage");
            Id = (int)jsonObject.GetSafeNumber("id");
            LogoPath = jsonObject.GetSafeString("logo_path");
            Name = jsonObject.GetSafeString("name");
            ParentCompany = (int)jsonObject.GetSafeNumber("parent_company");
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
            return Extensions.MakeImageUri(size.ToString(), LogoPath);
        }
        #endregion

        #region navigation properties
        /// <summary>
        /// Retrieves the credits for this company
        /// </summary>
        /// <param name="page">The request page of the search results, giving 0 will give all results.</param>
        /// <returns>A page of credits</returns>
        public async Task<SearchResult<MovieSummary>> CreditsAsync(int page = 1)
        {
            return await TheMovieDb.GetCompanyCreditsAsync(Id, page);
        }
        #endregion
    }
}
