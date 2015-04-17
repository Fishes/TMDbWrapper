using System.Threading.Tasks;
using TmdbWrapper.Search;
using TmdbWrapper.Utilities;

namespace TmdbWrapper
{
    public static partial class TheMovieDb
    {
        /// <summary>
        /// Searches for movies that match the query string.
        /// </summary>
        /// <param name="query">The query string</param>
        /// <param name="page">The request page of the search results, giving 0 will give all results.</param>
        /// <param name="includeAdult">Indicates whether to include adult movies.</param>
        /// <param name="year">If specified the year the movies are released.</param>
        /// <returns>A search result set of movie summaries.</returns>
        public static async Task<SearchResult<MovieSummary>> SearchMovieAsync(string query, int page = 1, bool? includeAdult = null, int? year = null)
        {
            var request = new Request<MovieSummary>("search/movie");
            
            request.AddParameter("query", query.EscapeString());
            request.AddParameter("page", page);
            if (!string.IsNullOrEmpty(Language))
                request.AddParameter("language", Language);
            if (includeAdult.HasValue)
                request.AddParameter("include_adult", includeAdult.Value ? "true" : "false");
            if (year.HasValue)
                request.AddParameter("year", year.Value.ToString());

            return await request.ProcessSearchRequestAsync();
        }

        /// <summary>
        /// Searches for collections that match the query string.
        /// </summary>
        /// <param name="query">The query string</param>
        /// <param name="page">The request page of the search results, giving 0 will give all results.</param>
        /// <returns>The resultset with found collections.</returns>
        public static async Task<SearchResult<CollectionSummary>> SearchCollectionAsync(string query, int page = 1)
        {
            var request = new Request<CollectionSummary>("search/collection");
            request.AddParameter("query", query.EscapeString());
            request.AddParameter("page", page);
            if (!string.IsNullOrEmpty(Language))
                request.AddParameter("language", Language);
            return await request.ProcessSearchRequestAsync();
        }

        /// <summary>
        /// Searches for persons that match the query string.
        /// </summary>
        /// <param name="query">The query string</param>
        /// <param name="page">The request page of the search results, giving 0 will give all results.</param>
        /// <returns>The resultset with the found person summaries.</returns>
        public static async Task<SearchResult<PersonSummary>> SearchPersonAsync(string query, int page = 1)
        {
            var request = new Request<PersonSummary>("search/person");
            
            request.AddParameter("query", query.EscapeString());
            request.AddParameter("page", page);
            if (!string.IsNullOrEmpty(Language))
                request.AddParameter("language", Language);

            return await request.ProcessSearchRequestAsync();
        }

        /// <summary>
        /// Searches for companies that match the query string.
        /// </summary>
        /// <param name="query">The query string.</param>
        /// <param name="page">The request page of the search results, giving 0 will give all results.</param>
        /// <returns>The resultset with the found company summaries.</returns>
        public static async Task<SearchResult<CompanySummary>> SearchCompanyAsync(string query, int page = 1)
        {
            var request = new Request<CompanySummary>("search/company");
            request.AddParameter("query", query.EscapeString());
            request.AddParameter("page", page);
            return await request.ProcessSearchRequestAsync();
        }

        /// <summary>
        /// Searches for TV series that match the query string
        /// </summary>
        /// <param name="query">The query string</param>
        /// <param name="firstAirDateYear">The year of first air date</param>
        /// <param name="page">The request page of the search results, giving 0 will give all results.</param>
        /// <returns>The resultset with the found tv series.</returns>
        public static async Task<SearchResult<TVSummary>> SearchTVAsync(string query, int? firstAirDateYear = null, int page = 1)
        {
            var request = new Request<TVSummary>("search/tv");
            request.AddParameter("query", query.EscapeString());
            request.AddParameter("page", page);
            if (firstAirDateYear.HasValue)
            {
                request.AddParameter("first_air_date_year", firstAirDateYear.Value);
            }
            return await request.ProcessSearchRequestAsync();
        }
    }
}
