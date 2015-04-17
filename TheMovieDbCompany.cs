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
        /// <param name="companyId">Id of the requested company.</param>
        /// <returns>The company that is associated to the id.</returns>
        public static async Task<Company> GetCompanyAsync(int companyId)
        {
            var company = DatabaseCache.GetObject<Company>(companyId);
            if (company == null)
            {
                var request = new Request<Company>("company/" + companyId);
                company = await request.ProcesRequestAsync();
                DatabaseCache.SetObject(companyId, company);
            }
            return company;
        }

        /// <summary>
        /// Gets the credits of the specified company.
        /// </summary>
        /// <param name="companyId">The id of the company</param>
        /// <param name="page">The request page of the search results, giving 0 will give all results.</param>
        /// <returns>A page of credits</returns>
        public static async Task<SearchResult<MovieSummary>> GetCompanyCreditsAsync(int companyId, int page = 1)
        {
            var request = new Request<MovieSummary>("company/" + companyId + "/movies");
            request.AddParameter("page", page);
            if(!string.IsNullOrEmpty(Language))
                request.AddParameter("language", Language);
            return await request.ProcessSearchRequestAsync();
        }
    }
}
