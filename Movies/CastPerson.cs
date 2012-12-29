using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.Data.Json;
using TmdbWrapper.Utilities;

namespace TmdbWrapper.Movies
{
    /// <summary>
    /// A member of the cast in a movie.
    /// </summary>
    public class CastPerson : ITmdbObject
    {
        /// <summary>
        /// Id of the person.
        /// </summary>
        public int Id { get; set; }
        /// <summary>
        /// Name of the person.
        /// </summary>
        public string Name { get; set; }
        /// <summary>
        /// Name of the character that is played.
        /// </summary>
        public string Character { get; set; }
        /// <summary>
        /// Order of the character in the credits
        /// </summary>
        public int Order { get; set; }
        /// <summary>
        /// Path of the profile picture
        /// </summary>
        public string ProfilePath { get; set; }

        /// <summary>
        /// Returns this instances ToString
        /// </summary>        
        public override string ToString()
        {
            return Name;
        }

        void ITmdbObject.ProcessJson(JsonObject jsonObject)
        {
            Id = (int)jsonObject.GetNamedValue("id").GetSafeNumber();
            Name = jsonObject.GetNamedValue("name").GetSafeString();
            Character = jsonObject.GetNamedValue("character").GetSafeString();
            Order = (int)jsonObject.GetNamedValue("order").GetSafeNumber();
            ProfilePath = jsonObject.GetNamedValue("profile_path").GetSafeString();
        }
    }
}
