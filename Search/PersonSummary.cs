using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TmdbWrapper.Utilities;

namespace TmdbWrapper.Search
{
    /// <summary>
    /// Summary of a person.
    /// </summary>
    public class PersonSummary : ITmdbObject
    {
        /// <summary>
        /// Indicates wether this is an adult actor.
        /// </summary>
        public bool Adult { get; set; }
        /// <summary>
        /// Id of this person.
        /// </summary>
        public int Id { get; set; }
        /// <summary>
        /// Name of this person.
        /// </summary>
        public string Name { get; set; }
        /// <summary>
        /// Path of an profile image of this person.
        /// </summary>
        public string ProfilePath { get; set; }

        /// <summary>
        /// Returns the ToString of this instance.
        /// </summary>
        /// <returns></returns>
        public override string ToString()
        {
            return Name;
        }
        
        void ITmdbObject.ProcessJson(Windows.Data.Json.JsonObject jsonObject)
        {
            Adult = jsonObject.GetNamedValue("adult").GetSafeBoolean();
            Id = (int)jsonObject.GetNamedValue("id").GetSafeNumber();
            Name = jsonObject.GetNamedValue("name").GetSafeString();
            ProfilePath = jsonObject.GetNamedValue("profile_path").GetSafeString();
        }

        /// <summary>
        /// Uri to the profile image.
        /// </summary>
        /// <param name="size">The size for the image as required</param>
        /// <returns>The uri to the sized image</returns>
        public Uri Uri(ProfileSize size)
        {
            return Utilities.Extensions.MakeImageUri(size.ToString(), ProfilePath);
        }
        /// <summary>
        /// Retrieves the associated person.
        /// </summary>
        public async Task<Persons.Person> Person()
        {
            return await TheMovieDb.GetPerson(Id);
        }
    }
}
