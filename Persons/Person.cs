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
    /// Person
    /// </summary>
    public class Person : ITmdbObject
    {
        /// <summary>
        /// Indicates wether this person is an adult actor
        /// </summary>
        public bool Adult { get; set; }
        /// <summary>
        /// Aliases this person is known by.
        /// </summary>
        public string[] Aliases { get; set; }
        /// <summary>
        /// Biography of this person.
        /// </summary>
        public string Biography { get; set; }
        /// <summary>
        /// Birthday 
        /// </summary>
        public string Birthday { get; set; }
        /// <summary>
        /// Date of death
        /// </summary>
        public string Deathday { get; set; }
        /// <summary>
        /// Uri of possible homepage.
        /// </summary>
        public Uri Homepage { get; set; }
        /// <summary>
        /// Id of this person
        /// </summary>
        public int Id { get; set; }
        /// <summary>
        /// Name of this person.
        /// </summary>
        public string Name { get; set; }
        /// <summary>
        /// Place of birth
        /// </summary>
        public string PlaceOfBirth { get; set; }
        /// <summary>
        /// Path of the profile.
        /// </summary>
        public string ProfilePath { get; set; }

        void ITmdbObject.ProcessJson(JsonObject jsonObject)
        {
            Adult = jsonObject.GetNamedValue("adult").GetSafeBoolean();
            Biography = jsonObject.GetNamedValue("biography").GetSafeString();
            Birthday = jsonObject.GetNamedValue("birthday").GetSafeString();
            Deathday = jsonObject.GetNamedValue("deathday").GetSafeString();
            Homepage = new Uri(jsonObject.GetNamedValue("homepage").GetSafeString());
            Id = (int)jsonObject.GetNamedValue("id").GetSafeNumber();
            Name = jsonObject.GetNamedValue("name").GetSafeString();
            PlaceOfBirth = jsonObject.GetNamedValue("place_of_birth").GetSafeString();
            ProfilePath = jsonObject.GetNamedValue("profile_path").GetSafeString();
        }
    }
}
