using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TmdbWrapper.Cache;
using TmdbWrapper.Image;
using TmdbWrapper.Movies;
using TmdbWrapper.Search;
using TmdbWrapper.Utilities;

namespace TmdbWrapper
{
    /// <summary>
    /// Enumeration of extras that should be prefilled on retrieving the movie info
    /// </summary>
    [Flags]
    public enum MovieExtras
    {
        /// <summary>
        /// Retrieve the cast
        /// </summary>
        casts = 1        
    }

    public static partial class TheMovieDb
    {        
        /// <summary>
        /// Gets a movie by the movie database id.
        /// </summary>
        /// <param name="MovieID">Id of the movie</param>
        /// <param name="extra">Indicates which parts should be prefetched.</param>
        /// <returns>The specified movie.</returns>
        public static async Task<Movie> GetMovieAsync(int MovieID, MovieExtras extra = 0)
        {
            Movie movie = DatabaseCache.GetObject<Movie>(MovieID);
            if (movie == null)
            {
                Request<Movie> request = new Request<Movie>("movie/" + MovieID.ToString());
                if (!string.IsNullOrEmpty(Language))
                    request.AddParameter("language", Language);
                if (extra != 0)
                {
                    request.AddParameter("append_to_response", extra.ToString().Replace(" ", ""));
                }
                movie = await request.ProcesRequestAsync();
                DatabaseCache.SetObject(MovieID, movie);
            }
            return movie;
        }

        /// <summary>
        /// Gets a movie by the IMDB id.
        /// </summary>
        /// <param name="IMDBId">The IMDB id</param>
        /// <returns>The specified movie.</returns>
        public static async Task<Movie> GetMovieByIMDBAsync(string IMDBId)
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
        public static async Task<IReadOnlyList<AlternativeTitle>> GetMovieAlternateTitlesAsync(int MovieID, string Country)
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
        public static async Task<Credits> GetMovieCastAsync(int MovieID)
        {
            Credits credits = DatabaseCache.GetObject<Credits>(MovieID);
            if (credits == null)
            {
                Request<Credits> request = new Request<Credits>(string.Format("movie/{0}/casts", MovieID));
                credits = await request.ProcesRequestAsync();
                DatabaseCache.SetObject(MovieID, credits);
            }
            return credits;
        }       
        
        /// <summary>
        /// All images of a specific movie.
        /// </summary>
        /// <param name="MovieID">The id of the movie.</param>
        /// <returns>The images.</returns>
        public static async Task<Images> GetMovieImagesAsync(int MovieID)
        {
            Images images = DatabaseCache.GetObject<Images>(MovieID);
            if (images == null)
            {
                Request<Images> request = new Request<Images>("movie/" + MovieID.ToString() + "/images");
                if (!string.IsNullOrEmpty(Language))
                    request.AddParameter("language", Language);
                images = await request.ProcesRequestAsync();
                DatabaseCache.SetObject(MovieID, images);
            }
            return images;
        }        
        
        /// <summary>
        /// The keywords of a specific movie.
        /// </summary>
        /// <param name="MovieID">The id of the movie.</param>
        /// <returns>A list of movie keywords.</returns>
        public static async Task<IReadOnlyList<Keyword>> GetMovieKeywordsAsync(int MovieID)
        {
            Request<Keyword> request = new Request<Keyword>("movie/" + MovieID.ToString() + "/keywords");
            return await request.ProcesRequestListAsync("keywords");
        }
        
        /// <summary>
        /// Releases of a specific movie.
        /// </summary>
        /// <param name="MovieID">Id of the movie</param>
        /// <returns>A list of releases.</returns>
        public static async Task<IReadOnlyList<Release>> GetMovieReleasesAsync(int MovieID)
        {
            Request<Release> request = new Request<Release>("movie/" + MovieID.ToString() + "/releases");
            return await request.ProcesRequestListAsync("releases");
        }
        
        /// <summary>
        /// Gets the trailer of a specific movie.
        /// </summary>
        /// <param name="MovieID">The id of the movie.</param>
        /// <returns>The trailers of the movie.</returns>
        public static async Task<Trailers> GetMovieTrailersAsync(int MovieID)
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
        public static async Task<SearchResult<MovieSummary>> GetSimilarMoviesAsync(int MovieID, int page = 1)
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
        public static async Task<SpokenLanguage> GetMovieTranslationsAsync(int MovieID)
        {
            Request<SpokenLanguage> request = new Request<SpokenLanguage>("movie/" + MovieID.ToString() + "/translations");
            return await request.ProcesRequestAsync();
        }
         
    }
}
