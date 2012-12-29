using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TmdbWrapper.Utilities;
using Windows.Data.Json;

namespace TmdbWrapper.Movies
{
    /// <summary>
    /// Movie
    /// </summary>
    public class Movie : ITmdbObject
    {
        /// <summary>
        /// Indictates wether this is an adult title.
        /// </summary>
        public bool Adult { get; set; }
        /// <summary>
        /// Path of the backdrop image.
        /// </summary>
        public string BackdropPath { get; set; }
        /// <summary>
        /// The collection this movie might belong to.
        /// </summary>
        public BelongsToCollection BelongsToCollection { get; set; }
        /// <summary>
        /// Production budget of this movie.
        /// </summary>
        public int Budget { get; set; }
        /// <summary>
        /// Genres that are assoctiated to this title.
        /// </summary>
        public IList<Genre> Genres { get; set; }
        /// <summary>
        /// Homepage of this movie.
        /// </summary>
        public Uri Homepage { get; set; }
        /// <summary>
        /// Id of this movie.
        /// </summary>
        public int Id { get; set; }
        /// <summary>
        /// Id of this movie in the IMDB.
        /// </summary>
        public string ImdbId { get; set; }
        /// <summary>
        /// Original title.
        /// </summary>
        public string OriginalTitle { get; set; }
        /// <summary>
        /// Overview of this movie.
        /// </summary>
        public string Overview { get; set; }
        /// <summary>
        /// Popularity of this movie.
        /// </summary>
        public double Popularity { get; set; }
        /// <summary>
        /// Path of the poster of this movie.
        /// </summary>
        public string PosterPath { get; set; }
        /// <summary>
        /// List of companies that produced this movie.
        /// </summary>
        public IList<ProductionCompany> ProductionCompanies { get; set; }
        /// <summary>
        /// List of countries where this movie was produced.
        /// </summary>
        public IList<ProductionCountry> ProductionCountries { get; set; }
        /// <summary>
        /// Date this movie was released.
        /// </summary>
        public string ReleaseDate { get; set; }
        /// <summary>
        /// Revenue that this movie gathered.
        /// </summary>
        public Int64 Revenue { get; set; }
        /// <summary>
        /// Original runtime in minutes.
        /// </summary>
        public int Runtime { get; set; }
        /// <summary>
        /// List of languages that is spoken in this movie.
        /// </summary>
        public IList<SpokenLanguage> SpokenLanguages { get; set; }
        /// <summary>
        /// Status of this movie.
        /// </summary>
        public string Status { get; set; }
        /// <summary>
        /// Tagline of the movie.
        /// </summary>
        public string Tagline { get; set; }
        /// <summary>
        /// Title of this movie.
        /// </summary>
        public string Title { get; set; }
        /// <summary>
        /// Average of votes.
        /// </summary>
        public double VoteAverage { get; set; }
        /// <summary>
        /// Number of votes.
        /// </summary>
        public int VoteCount { get; set; }

        /// <summary>
        /// Returns this instance ToString
        /// </summary>
        public override string ToString()
        {
            return Title;
        }

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
    }
}
