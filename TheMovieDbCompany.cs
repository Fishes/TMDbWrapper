using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TmdbWrapper.Cache;
using TmdbWrapper.Companies;
using TmdbWrapper.Search;
using TmdbWrapper.Utilities;

namespace TmdbWrapper
{
    public partial class TheMovieDb
    {
        /// <summary>
        /// Gets a specific company
        /// </summary>
        /// <param name="CompanyId">Id of the requested company.</param>
        /// <returns>The company that is associated to the id.</returns>
        public static async Task<Company> GetCompanyAsync(int CompanyId)
        {
            Company company = DatabaseCache.GetObject<Company>(CompanyId);
            if (company == null)
            {
                Request<Company> request = new Request<Company>("company/" + CompanyId.ToString());
                company = await request.ProcesRequestAsync();
                DatabaseCache.SetObject(CompanyId, company);
            }
            return company;
        }

        /// <summary>
        /// Gets the credits of the specified company.
        /// </summary>
        /// <param name="CompanyId">The id of the company</param>
        /// <param name="page">The request page of the search results, giving 0 will give all results.</param>
        /// <returns>A page of credits</returns>
        public static async Task<SearchResult<MovieSummary>> GetCompanyCreditsAsync(int CompanyId, int page = 1)
        {
            Request<MovieSummary> request = new Request<MovieSummary>("company/" + CompanyId.ToString() + "/movies");
            request.AddParameter("page", page);
            if(!string.IsNullOrEmpty(Language))
                request.AddParameter("language", Language);
            return await request.ProcessSearchRequestAsync();
        }
    }
}
