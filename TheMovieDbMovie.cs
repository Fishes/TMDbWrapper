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
            Request request = new Request("movie/" + MovieID.ToString());
            if (!string.IsNullOrEmpty(Language))
                request.AddParameter("language", Language);
            return await request.ProcesRequestAsync<Movie>();
        }        
        
        /// <summary>
        /// Gets a movie by the IMDB id.
        /// </summary>
        /// <param name="IMDBId">The IMDB id</param>
        /// <returns>The specified movie.</returns>
        public static async Task<Movie> GetMovieByIMDB(string IMDBId)
        {
            Request request = new Request("movie/" + IMDBId);
            if (!string.IsNullOrEmpty(Language))
                request.AddParameter("language", Language);
            return await request.ProcesRequestAsync<Movie>();
        }        
        
        /// <summary>
        /// Gets a list of altenative titles for the specified country
        /// </summary>
        /// <param name="MovieID">Id of the movie</param>
        /// <param name="Country">Code of the country</param>
        /// <returns>A list of alternative titles.</returns>
        public static async Task<IList<AlternativeTitle>> GetMovieAlternateTitles(int MovieID, string Country)
        {
            Request request = new Request("movie/" + MovieID.ToString() + "/alternative_titles");
            if (!string.IsNullOrEmpty(Country))
                request.AddParameter("country", Country);
            return await request.ProcesRequestListAsync<AlternativeTitle>("titles");
        }        
        
        /// <summary>
        /// Gets the credits of a specific movie.
        /// </summary>
        /// <param name="MovieID">The id of the movie.</param>
        /// <returns>The credits of the movie.</returns>
        public static async Task<Credits> GetMovieCast(int MovieID)
        {
            Request request = new Request(string.Format("movie/{0}/casts", MovieID));
            return await request.ProcesRequestAsync<Credits>();
        }       
        
        /// <summary>
        /// All images of a specific movie.
        /// </summary>
        /// <param name="MovieID">The id of the movie.</param>
        /// <returns>The images.</returns>
        public static async Task<Images> GetMovieImages(int MovieID)
        {
            Request request = new Request("movie/" + MovieID.ToString() + "/images");
            if (!string.IsNullOrEmpty(Language))
                request.AddParameter("language", Language);
            return await request.ProcesRequestAsync<Images>();
        }        
        
        /// <summary>
        /// The keywords of a specific movie.
        /// </summary>
        /// <param name="MovieID">The id of the movie.</param>
        /// <returns>A list of movie keywords.</returns>
        public static async Task<IList<Keyword>> GetMovieKeywords(int MovieID)
        {
            Request request = new Request("movie/" + MovieID.ToString() + "/keywords");
            return await request.ProcesRequestListAsync<Keyword>("keywords");
        }
        
        /// <summary>
        /// Releases of a specific movie.
        /// </summary>
        /// <param name="MovieID">Id of the movie</param>
        /// <returns>A list of releases.</returns>
        public static async Task<IList<Release>> GetMovieReleases(int MovieID)
        {
            Request request = new Request("movie/" + MovieID.ToString() + "/releases");
            return await request.ProcesRequestListAsync<Release>("releases");
        }
        
        /// <summary>
        /// Gets the trailer of a specific movie.
        /// </summary>
        /// <param name="MovieID">The id of the movie.</param>
        /// <returns>The trailers of the movie.</returns>
        public static async Task<Trailers> GetMovieTrailers(int MovieID)
        {
            Request request = new Request("movie/" + MovieID.ToString() + "/trailers");
            if (!string.IsNullOrEmpty(Language))
                request.AddParameter("language", Language);
            return await request.ProcesRequestAsync<Trailers>();
        }
        
        /// <summary>
        /// Gets movies that are similiar to the specified movie.
        /// </summary>
        /// <param name="MovieID">The specific movie.</param>
        /// <param name="page">The page of the results</param>
        /// <returns>A result set with movie summaries.</returns>
        public static async Task<SearchResultBase<MovieSummary>> GetSimilarMovies(int MovieID, int page = 1)
        {
            Request request = new Request("movie/" + MovieID.ToString() + "/similar_movies");
            request.AddParameter("page", page);
            if (!string.IsNullOrEmpty(Language))
                request.AddParameter("language", Language);

            return await request.ProcessSearchRequestAsync<MovieSummary>();
        }        
        
        /// <summary>
        /// Gets the languages that a specific movie is translated into.
        /// </summary>
        /// <param name="MovieID">The id of the movie.</param>
        /// <returns></returns>
        public static async Task<SpokenLanguage> GetMovieTranslations(int MovieID)
        {
            Request request = new Request("movie/" + MovieID.ToString() + "/translations");
            return await request.ProcesRequestAsync<SpokenLanguage>();
        }
         
    }
}
