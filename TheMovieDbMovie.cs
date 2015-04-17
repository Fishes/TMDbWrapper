using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using TmdbWrapper.Cache;
using TmdbWrapper.Images;
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
        Casts = 1        
    }                                                                                            

    public static partial class TheMovieDb
    {        
        /// <summary>
        /// Gets a movie by the movie database id.
        /// </summary>
        /// <param name="movieId">Id of the movie</param>
        /// <param name="extra">Indicates which parts should be prefetched.</param>
        /// <returns>The specified movie.</returns>
        public static async Task<Movie> GetMovieAsync(int movieId, MovieExtras extra = 0)
        {
            var movie = DatabaseCache.GetObject<Movie>(movieId);
            if (movie == null)
            {
                var request = new Request<Movie>("movie/" + movieId);
                if (!string.IsNullOrEmpty(Language))
                    request.AddParameter("language", Language);
                if (extra != 0)
                {
                    request.AddParameter("append_to_response", extra.ToString().Replace(" ", ""));
                }
                movie = await request.ProcesRequestAsync();
                DatabaseCache.SetObject(movieId, movie);
            }
            return movie;
        }

        /// <summary>
        /// Gets a movie by the IMDB id.
        /// </summary>
        /// <param name="imdbId">The IMDB id</param>
        /// <returns>The specified movie.</returns>
        public static async Task<Movie> GetMovieByImdbAsync(string imdbId)
        {
            var request = new Request<Movie>("movie/" + imdbId);
            if (!string.IsNullOrEmpty(Language))
                request.AddParameter("language", Language);
            return await request.ProcesRequestAsync();
        }        
        
        /// <summary>
        /// Gets a list of altenative titles for the specified country
        /// </summary>
        /// <param name="movieId">Id of the movie</param>
        /// <param name="country">Code of the country</param>
        /// <returns>A list of alternative titles.</returns>
        public static async Task<IReadOnlyList<AlternativeTitle>> GetMovieAlternateTitlesAsync(int movieId, string country)
        {
            var request = new Request<AlternativeTitle>("movie/" + movieId + "/alternative_titles");
            if (!string.IsNullOrEmpty(country))
                request.AddParameter("country", country);
            return await request.ProcesRequestListAsync("titles");
        }        
        
        /// <summary>
        /// Gets the credits of a specific movie.
        /// </summary>
        /// <param name="movieId">The id of the movie.</param>
        /// <returns>The credits of the movie.</returns>
        public static async Task<Credits> GetMovieCastAsync(int movieId)
        {
            var credits = DatabaseCache.GetObject<Credits>(movieId);
            if (credits == null)
            {
                var request = new Request<Credits>(string.Format("movie/{0}/casts", movieId));
                credits = await request.ProcesRequestAsync();
                DatabaseCache.SetObject(movieId, credits);
            }
            return credits;
        }       
        
        /// <summary>
        /// All images of a specific movie.
        /// </summary>
        /// <param name="movieId">The id of the movie.</param>
        /// <returns>The images.</returns>
        public static async Task<Image> GetMovieImagesAsync(int movieId)
        {
            var images = DatabaseCache.GetObject<Image>(movieId);
            if (images == null)
            {
                var request = new Request<Image>("movie/" + movieId + "/images");
                if (!string.IsNullOrEmpty(Language))
                    request.AddParameter("language", Language);
                images = await request.ProcesRequestAsync();
                DatabaseCache.SetObject(movieId, images);
            }
            return images;
        }        
        
        /// <summary>
        /// The keywords of a specific movie.
        /// </summary>
        /// <param name="movieId">The id of the movie.</param>
        /// <returns>A list of movie keywords.</returns>
        public static async Task<IReadOnlyList<Keyword>> GetMovieKeywordsAsync(int movieId)
        {
            var request = new Request<Keyword>("movie/" + movieId + "/keywords");
            return await request.ProcesRequestListAsync("keywords");
        }
        
        /// <summary>
        /// Releases of a specific movie.
        /// </summary>
        /// <param name="movieId">Id of the movie</param>
        /// <returns>A list of releases.</returns>
        public static async Task<IReadOnlyList<Release>> GetMovieReleasesAsync(int movieId)
        {
            var request = new Request<Release>("movie/" + movieId + "/releases");
            return await request.ProcesRequestListAsync("releases");
        }
        
        /// <summary>
        /// Gets the trailer of a specific movie.
        /// </summary>
        /// <param name="movieId">The id of the movie.</param>
        /// <returns>The trailers of the movie.</returns>
        public static async Task<Trailers> GetMovieTrailersAsync(int movieId)
        {
            var request = new Request<Trailers>("movie/" + movieId + "/trailers");
            if (!string.IsNullOrEmpty(Language))
                request.AddParameter("language", Language);
            return await request.ProcesRequestAsync();
        }
        
        /// <summary>
        /// Gets movies that are similiar to the specified movie.
        /// </summary>
        /// <param name="movieId">The specific movie.</param>
        /// <param name="page">The request page of the search results, giving 0 will give all results.</param>
        /// <returns>A result set with movie summaries.</returns>
        public static async Task<SearchResult<MovieSummary>> GetSimilarMoviesAsync(int movieId, int page = 1)
        {
            var request = new Request<MovieSummary>("movie/" + movieId + "/similar_movies");
            request.AddParameter("page", page);
            if (!string.IsNullOrEmpty(Language))
                request.AddParameter("language", Language);

            return await request.ProcessSearchRequestAsync();
        }        
        
        /// <summary>
        /// Gets the languages that a specific movie is translated into.
        /// </summary>
        /// <param name="movieId">The id of the movie.</param>
        /// <returns></returns>
        public static async Task<SpokenLanguage> GetMovieTranslationsAsync(int movieId)
        {
            var request = new Request<SpokenLanguage>("movie/" + movieId + "/translations");
            return await request.ProcesRequestAsync();
        }
         
    }
}
