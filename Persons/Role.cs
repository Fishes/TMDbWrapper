using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TmdbWrapper.Utilities;
using Windows.Data.Json;

namespace TmdbWrapper.Persons
{
    /// <summary>
    /// Role in a movie
    /// </summary>
    public class Role : ITmdbObject
    {
        /// <summary>
        /// Id of the movie
        /// </summary>
        public int Id { get; set; }
        /// <summary>
        /// Title of the movie
        /// </summary>
        public string Title { get; set; }
        /// <summary>
        /// Name of the character.
        /// </summary>
        public string Character { get; set; }
        /// <summary>
        /// Original title of the movie.
        /// </summary>
        public string OriginalTitle { get; set; }
        /// <summary>
        /// Path of the poster image.
        /// </summary>
        public string PosterPath { get; set; }
        /// <summary>
        /// Release date of the movie.
        /// </summary>
        public string ReleaseDate { get; set; }
        /// <summary>
        /// Indicates if the title is an adult movie.
        /// </summary>
        public bool Adult { get; set; }

        void ITmdbObject.ProcessJson(JsonObject jsonObject)
        {
            Id = (int)jsonObject.GetNamedValue("id").GetSafeNumber();
            Title = jsonObject.GetNamedValue("title").GetSafeString();
            Character = jsonObject.GetNamedValue("character").GetSafeString();
            OriginalTitle = jsonObject.GetNamedValue("original_title").GetSafeString();
            PosterPath = jsonObject.GetNamedValue("poster_path").GetSafeString();
            ReleaseDate = jsonObject.GetNamedValue("release_date").GetSafeString();
            Adult = jsonObject.GetNamedValue("adult").GetSafeBoolean();
        }
    }
}
