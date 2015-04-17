using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using TmdbWrapper.Images;
using TmdbWrapper.Search;
using TmdbWrapper.Utilities;

namespace TmdbWrapper.Movies
{
    /// <summary>
    /// Movie
    /// </summary>
    public class Movie : ITmdbObject
    {
        #region private fields
        private Credits _credits;
        #endregion

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
        public DateTime? ReleaseDate { get; private set; }
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

        /// <summary>
        /// Gets the credits associated to this movie.
        /// </summary>
        public Credits Credits {
            get
            {
                if (_credits == null)
                {
                    _credits = TheMovieDb.GetMovieCastAsync(Id).Result;
                }
                return _credits;
            }
        }

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
        void ITmdbObject.ProcessJson(JSONObject jsonObject)
        {
            Adult = jsonObject.GetSafeBoolean("adult");
            BackdropPath = jsonObject.GetSafeString("backdrop_path");
            BelongsToCollection = jsonObject.ProcessObject<BelongsToCollection>("belongs_to_collection");
            Budget = (int)jsonObject.GetSafeNumber("budget");
            Genres = jsonObject.ProcessObjectArray<Genre>("genres");            
            Homepage = jsonObject.GetSafeUri("homepage");
            Id = (int)jsonObject.GetSafeNumber("id");
            ImdbId = jsonObject.GetSafeString("imdb_id");
            OriginalTitle = jsonObject.GetSafeString("original_title");
            Overview = jsonObject.GetSafeString("overview");
            Popularity = (int)jsonObject.GetSafeNumber("popularity");
            PosterPath = jsonObject.GetSafeString("poster_path");
            ProductionCompanies = jsonObject.ProcessObjectArray<ProductionCompany>("production_companies");
            ProductionCountries = jsonObject.ProcessObjectArray<ProductionCountry>("production_countries");
            ReleaseDate = jsonObject.GetSafeDateTime("release_date");
            Revenue = (int)jsonObject.GetSafeNumber("revenue");
            Runtime = (int)jsonObject.GetSafeNumber("runtime");
            SpokenLanguages = jsonObject.ProcessObjectArray<SpokenLanguage>("spoken_languages");
            Status = jsonObject.GetSafeString("status");
            Tagline = jsonObject.GetSafeString("tagline");
            Title = jsonObject.GetSafeString("title");
            VoteAverage = jsonObject.GetSafeNumber("vote_average");
            VoteCount = (int)jsonObject.GetSafeNumber("vote_count");
            _credits = jsonObject.ProcessObject<Credits>("casts");

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
            return Extensions.MakeImageUri(size.ToString(), PosterPath);
        }

        /// <summary>
        /// Uri to the backdrop image.
        /// </summary>
        /// <param name="size">The size for the image as required</param>
        /// <returns>The uri to the sized image</returns>
        public Uri Uri(BackdropSize size)
        {
            return Extensions.MakeImageUri(size.ToString(), BackdropPath);
        }
        #endregion

        #region Navigation Properties
        /// <summary>
        /// Gets a list of altenative titles for the specified country
        /// </summary>
        /// <param name="country">Code of the country</param>
        /// <returns>A list of alternative titles.</returns>
        public async Task<IReadOnlyList<AlternativeTitle>> AlternateTitlesAsync(string country)
        {
            return await TheMovieDb.GetMovieAlternateTitlesAsync(Id, country);
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
        public async Task<Image> ImagesAsync()
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
