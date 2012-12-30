using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TmdbWrapper.Image;
using TmdbWrapper.Search;
using TmdbWrapper.Utilities;
using Windows.Data.Json;

namespace TmdbWrapper.Movies
{
    /// <summary>
    /// Movie
    /// </summary>
    public class Movie : ITmdbObject
    {
        #region Properties
        /// <summary>
        /// Indictates wether this is an adult title.
        /// </summary>
        public bool Adult { get; private set; }
        /// <summary>
        /// Path of the backdrop image.
        /// </summary>
        public string BackdropPath { get; private set; }
        /// <summary>
        /// The collection this movie might belong to.
        /// </summary>
        public BelongsToCollection BelongsToCollection { get; private set; }
        /// <summary>
        /// Production budget of this movie.
        /// </summary>
        public int Budget { get; private set; }
        /// <summary>
        /// Genres that are assoctiated to this title.
        /// </summary>
        public IReadOnlyList<Genre> Genres { get; private set; }
        /// <summary>
        /// Homepage of this movie.
        /// </summary>
        public Uri Homepage { get; private set; }
        /// <summary>
        /// Id of this movie.
        /// </summary>
        public int Id { get; private set; }
        /// <summary>
        /// Id of this movie in the IMDB.
        /// </summary>
        public string ImdbId { get; private set; }
        /// <summary>
        /// Original title.
        /// </summary>
        public string OriginalTitle { get; private set; }
        /// <summary>
        /// Overview of this movie.
        /// </summary>
        public string Overview { get; private set; }
        /// <summary>
        /// Popularity of this movie.
        /// </summary>
        public double Popularity { get; private set; }
        /// <summary>
        /// Path of the poster of this movie.
        /// </summary>
        public string PosterPath { get; private set; }
        /// <summary>
        /// List of companies that produced this movie.
        /// </summary>
        public IReadOnlyList<ProductionCompany> ProductionCompanies { get; private set; }
        /// <summary>
        /// List of countries where this movie was produced.
        /// </summary>
        public IReadOnlyList<ProductionCountry> ProductionCountries { get; private set; }
        /// <summary>
        /// Date this movie was released.
        /// </summary>
        public string ReleaseDate { get; private set; }
        /// <summary>
        /// Revenue that this movie gathered.
        /// </summary>
        public Int64 Revenue { get; private set; }
        /// <summary>
        /// Original runtime in minutes.
        /// </summary>
        public int Runtime { get; private set; }
        /// <summary>
        /// List of languages that is spoken in this movie.
        /// </summary>
        public IReadOnlyList<SpokenLanguage> SpokenLanguages { get; private set; }
        /// <summary>
        /// Status of this movie.
        /// </summary>
        public string Status { get; private set; }
        /// <summary>
        /// Tagline of the movie.
        /// </summary>
        public string Tagline { get; private set; }
        /// <summary>
        /// Title of this movie.
        /// </summary>
        public string Title { get; private set; }
        /// <summary>
        /// Average of votes.
        /// </summary>
        public double VoteAverage { get; private set; }
        /// <summary>
        /// Number of votes.
        /// </summary>
        public int VoteCount { get; private set; }
        #endregion

        #region Overrides
        /// <summary>
        /// Returns this instance ToString
        /// </summary>
        public override string ToString()
        {
            return Title;
        }
        #endregion

        #region Interface implementations
        void ITmdbObject.ProcessJson(JsonObject jsonObject)
        {
            Adult = jsonObject.GetNamedValue("adult").GetSafeBoolean();
            BackdropPath = jsonObject.GetNamedValue("backdrop_path").GetSafeString();
            BelongsToCollection = jsonObject.GetNamedValue("belongs_to_collection").ProcessObject<BelongsToCollection>();
            Budget = (int)jsonObject.GetNamedValue("budget").GetSafeNumber();
            Genres = jsonObject.GetNamedValue("genres").ProcessArray<Genre>();            
            Homepage = new Uri(jsonObject.GetNamedValue("homepage").GetSafeString());
            Id = (int)jsonObject.GetNamedValue("id").GetSafeNumber();
            ImdbId = jsonObject.GetNamedValue("imdb_id").GetSafeString();
            OriginalTitle = jsonObject.GetNamedValue("original_title").GetSafeString();
            Overview = jsonObject.GetNamedValue("overview").GetSafeString();
            Popularity = (int)jsonObject.GetNamedValue("popularity").GetSafeNumber();
            PosterPath = jsonObject.GetNamedValue("poster_path").GetSafeString();
            ProductionCompanies = jsonObject.GetNamedValue("production_companies").ProcessArray<ProductionCompany>();
            ProductionCountries = jsonObject.GetNamedValue("production_countries").ProcessArray<ProductionCountry>();
            ReleaseDate = jsonObject.GetNamedValue("release_date").GetSafeString();
            Revenue = (int)jsonObject.GetNamedValue("revenue").GetSafeNumber();
            Runtime = (int)jsonObject.GetNamedValue("runtime").GetSafeNumber();
            SpokenLanguages = jsonObject.GetNamedValue("spoken_languages").ProcessArray<SpokenLanguage>();
            Status = jsonObject.GetNamedValue("status").GetSafeString();
            Tagline = jsonObject.GetNamedValue("tagline").GetSafeString();
            Title = jsonObject.GetNamedValue("title").GetSafeString();
            VoteAverage = jsonObject.GetNamedValue("vote_average").GetSafeNumber();
            VoteCount = (int)jsonObject.GetNamedValue("vote_count").GetSafeNumber();
        }
        #endregion

        #region Image Uri's
        /// <summary>
        /// Uri to the poster image.
        /// </summary>
        /// <param name="size">The size for the image as required</param>
        /// <returns>The uri to the sized image</returns>
        public Uri Uri(PosterSize size)
        {
            return Utilities.Extensions.MakeImageUri(size.ToString(), PosterPath);
        }

        /// <summary>
        /// Uri to the backdrop image.
        /// </summary>
        /// <param name="size">The size for the image as required</param>
        /// <returns>The uri to the sized image</returns>
        public Uri Uri(BackdropSize size)
        {
            return Utilities.Extensions.MakeImageUri(size.ToString(), BackdropPath);
        }
        #endregion

        #region Navigation Properties
        /// <summary>
        /// Gets a list of altenative titles for the specified country
        /// </summary>
        /// <param name="Country">Code of the country</param>
        /// <returns>A list of alternative titles.</returns>
        public async Task<IReadOnlyList<AlternativeTitle>> AlternateTitlesAsync(string Country)
        {
            return await TheMovieDb.GetMovieAlternateTitlesAsync(Id, Country);
        }

        /// <summary>
        /// Gets the credits of this movie.
        /// </summary>
        /// <returns>The credits of the movie.</returns>
        public async Task<Credits> CastAsync()
        {
            return await TheMovieDb.GetMovieCastAsync(Id);
        }

        /// <summary>
        /// All images of this movie.
        /// </summary>
        /// <returns>The images.</returns>
        public async Task<Images> ImagesAsync()
        {
            return await TheMovieDb.GetMovieImagesAsync(Id);
        }

        /// <summary>
        /// The keywords of a specific movie.
        /// </summary>
        /// <returns>A list of movie keywords.</returns>
        public async Task<IReadOnlyList<Keyword>> KeywordsAsync()
        {
            return await TheMovieDb.GetMovieKeywordsAsync(Id);
        }

        /// <summary>
        /// Releases of a specific movie.
        /// </summary>
        /// <returns>A list of releases.</returns>
        public async Task<IReadOnlyList<Release>> ReleasesAsync()
        {
            return await TheMovieDb.GetMovieReleasesAsync(Id);
        }

        /// <summary>
        /// Gets the trailer of a specific movie.
        /// </summary>
        /// <returns>The trailers of the movie.</returns>
        public async Task<Trailers> TrailersAsync()
        {
            return await TheMovieDb.GetMovieTrailersAsync(Id);
        }

        /// <summary>
        /// Gets movies that are similiar to the specified movie.
        /// </summary>
        /// <param name="page">The request page of the search results, giving 0 will give all results.</param>
        /// <returns>A result set with movie summaries.</returns>
        public async Task<SearchResult<MovieSummary>> SimilarMoviesAsync(int page = 1)
        {
            return await TheMovieDb.GetSimilarMoviesAsync(Id, page);
        }

        /// <summary>
        /// Gets the languages that a specific movie is translated into.
        /// </summary>
        /// <returns></returns>
        public async Task<SpokenLanguage> TranslationsAsync()
        {
            return await TheMovieDb.GetMovieTranslationsAsync(Id);
        }

        #endregion
    }
}
