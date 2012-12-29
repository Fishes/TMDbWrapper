using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TmdbWrapper.Movies;
using TmdbWrapper.Search;
using TmdbWrapper.Utilities;

namespace TmdbWrapper
{
    public static partial class TheMovieDb
    {
        /// <summary>
        /// Gets a movie by the movie database id.
        /// </summary>
        /// <param name="MovieID">Id of the movie</param>
        /// <returns>The specified movie.</returns>
        public static async Task<Movie> GetMovie(int MovieID)
        {
            Request<Movie> request = new Request<Movie>("movie/" + MovieID.ToString());
            if (!string.IsNullOrEmpty(Language))
                request.AddParameter("language", Language);
            return await request.ProcesRequestAsync();
        }        
        
        /// <summary>
        /// Gets a movie by the IMDB id.
        /// </summary>
        /// <param name="IMDBId">The IMDB id</param>
        /// <returns>The specified movie.</returns>
        public static async Task<Movie> GetMovieByIMDB(string IMDBId)
        {
            Request<Movie> request = new Request<Movie>("movie/" + IMDBId);
            if (!string.IsNullOrEmpty(Language))
                request.AddParameter("language", Language);
            return await request.ProcesRequestAsync();
        }        
        
        /// <summary>
        /// Gets a list of altenative titles for the specified country
        /// </summary>
        /// <param name="MovieID">Id of the movie</param>
        /// <param name="Country">Code of the country</param>
        /// <returns>A list of alternative titles.</returns>
        public static async Task<IList<AlternativeTitle>> GetMovieAlternateTitles(int MovieID, string Country)
        {
            Request<AlternativeTitle> request = new Request<AlternativeTitle>("movie/" + MovieID.ToString() + "/alternative_titles");
            if (!string.IsNullOrEmpty(Country))
                request.AddParameter("country", Country);
            return await request.ProcesRequestListAsync("titles");
        }        
        
        /// <summary>
        /// Gets the credits of a specific movie.
        /// </summary>
        /// <param name="MovieID">The id of the movie.</param>
        /// <returns>The credits of the movie.</returns>
        public static async Task<Credits> GetMovieCast(int MovieID)
        {
            Request<Credits> request = new Request<Credits>(string.Format("movie/{0}/casts", MovieID));
            return await request.ProcesRequestAsync();
        }       
        
        /// <summary>
        /// All images of a specific movie.
        /// </summary>
        /// <param name="MovieID">The id of the movie.</param>
        /// <returns>The images.</returns>
        public static async Task<Images> GetMovieImages(int MovieID)
        {
            Request<Images> request = new Request<Images>("movie/" + MovieID.ToString() + "/images");
            if (!string.IsNullOrEmpty(Language))
                request.AddParameter("language", Language);
            return await request.ProcesRequestAsync();
        }        
        
        /// <summary>
        /// The keywords of a specific movie.
        /// </summary>
        /// <param name="MovieID">The id of the movie.</param>
        /// <returns>A list of movie keywords.</returns>
        public static async Task<IList<Keyword>> GetMovieKeywords(int MovieID)
        {
            Request<Keyword> request = new Request<Keyword>("movie/" + MovieID.ToString() + "/keywords");
            return await request.ProcesRequestListAsync("keywords");
        }
        
        /// <summary>
        /// Releases of a specific movie.
        /// </summary>
        /// <param name="MovieID">Id of the movie</param>
        /// <returns>A list of releases.</returns>
        public static async Task<IList<Release>> GetMovieReleases(int MovieID)
        {
            Request<Release> request = new Request<Release>("movie/" + MovieID.ToString() + "/releases");
            return await request.ProcesRequestListAsync("releases");
        }
        
        /// <summary>
        /// Gets the trailer of a specific movie.
        /// </summary>
        /// <param name="MovieID">The id of the movie.</param>
        /// <returns>The trailers of the movie.</returns>
        public static async Task<Trailers> GetMovieTrailers(int MovieID)
        {
            Request<Trailers> request = new Request<Trailers>("movie/" + MovieID.ToString() + "/trailers");
            if (!string.IsNullOrEmpty(Language))
                request.AddParameter("language", Language);
            return await request.ProcesRequestAsync();
        }
        
        /// <summary>
        /// Gets movies that are similiar to the specified movie.
        /// </summary>
        /// <param name="MovieID">The specific movie.</param>
        /// <param name="page">The request page of the search results, giving 0 will give all results.</param>
        /// <returns>A result set with movie summaries.</returns>
        public static async Task<SearchResult<MovieSummary>> GetSimilarMovies(int MovieID, int page = 1)
        {
            Request<MovieSummary> request = new Request<MovieSummary>("movie/" + MovieID.ToString() + "/similar_movies");
            request.AddParameter("page", page);
            if (!string.IsNullOrEmpty(Language))
                request.AddParameter("language", Language);

            return await request.ProcessSearchRequestAsync();
        }        
        
        /// <summary>
        /// Gets the languages that a specific movie is translated into.
        /// </summary>
        /// <param name="MovieID">The id of the movie.</param>
        /// <returns></returns>
        public static async Task<SpokenLanguage> GetMovieTranslations(int MovieID)
        {
            Request<SpokenLanguage> request = new Request<SpokenLanguage>("movie/" + MovieID.ToString() + "/translations");
            return await request.ProcesRequestAsync();
        }
         
    }
}
