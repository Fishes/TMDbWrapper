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
    /// Member of the crew.
    /// </summary>
    public class Crew : ITmdbObject
    {
        /// <summary>
        /// Id of the person.
        /// </summary>
        public int Id { get; set; }
        /// <summary>
        /// Title of the movie.
        /// </summary>
        public string Title { get; set; }
        /// <summary>
        /// Name of the department.
        /// </summary>
        public string Department { get; set; }
        /// <summary>
        /// Original title
        /// </summary>
        public string OriginalTitle { get; set; }
        /// <summary>
        /// Name of the job.
        /// </summary>
        public string Job { get; set; }
        /// <summary>
        /// Path of the poster of the movie.
        /// </summary>
        public string PosterPath { get; set; }
        /// <summary>
        /// Date of the original release.
        /// </summary>
        public string ReleaseDate { get; set; }
        /// <summary>
        /// Indictates if then movie is an adult title.
        /// </summary>
        public bool Adult { get; set; }

        void ITmdbObject.ProcessJson(JsonObject jsonObject)
        {
            Id = (int)jsonObject.GetNamedValue("id").GetSafeNumber();
            Title = jsonObject.GetNamedValue("title").GetSafeString();
            Department = jsonObject.GetNamedValue("department").GetSafeString();
            OriginalTitle = jsonObject.GetNamedValue("original_title").GetSafeString();
            Job = jsonObject.GetNamedValue("job").GetSafeString();
            PosterPath = jsonObject.GetNamedValue("poster_path").GetSafeString();
            ReleaseDate = jsonObject.GetNamedValue("release_date").GetSafeString();
            Adult = jsonObject.GetNamedValue("adult").GetSafeBoolean();
        }
    }
}
