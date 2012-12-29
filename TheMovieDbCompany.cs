using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
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
        public static async Task<Company> GetCompany(int CompanyId)
        {
            Request request = new Request("company/"+ CompanyId.ToString());
            return await request.ProcesRequestAsync<Company>();            
        }

        /// <summary>
        /// Gets the credits of the specified company.
        /// </summary>
        /// <param name="CompanyId">The id of the company</param>
        /// <param name="page">The page number of the set that is requested.</param>
        /// <returns>A page of credits</returns>
        public static async Task<SearchResultBase<MovieSummary>> GetCompanyCredits(int CompanyId, int page = 1)
        {
            Request request = new Request("company/" + CompanyId.ToString() + "/movies");
            request.AddParameter("page", page);
            if(!string.IsNullOrEmpty(Language))
                request.AddParameter("language", Language);
            return await request.ProcessSearchRequestAsync<MovieSummary>();
        }
    }
}
