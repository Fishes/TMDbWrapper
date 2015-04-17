using System;
using System.Threading.Tasks;
using TmdbWrapper.Persons;
using TmdbWrapper.Utilities;

namespace TmdbWrapper.Search
{
    /// <summary>
    /// Summary of a person.
    /// </summary>
    public class PersonSummary : ITmdbObject
    {
        #region properties
        /// <summary>
        /// Indicates wether this is an adult actor.
        /// </summary>
        public bool Adult { get; private set; }
        /// <summary>
        /// Id of this person.
        /// </summary>
        public int Id { get; private set; }
        /// <summary>
        /// Name of this person.
        /// </summary>
        public string Name { get; private set; }
        /// <summary>
        /// Path of an profile image of this person.
        /// </summary>
        public string ProfilePath { get; private set; }
        #endregion

        #region overrides
        /// <summary>
        /// Returns the ToString of this instance.
        /// </summary>
        /// <returns></returns>
        public override string ToString()
        {
            return Name;
        }
        #endregion

        #region interface implementations
        void ITmdbObject.ProcessJson(JSONObject jsonObject)
        {
            Adult = jsonObject.GetSafeBoolean("adult");
            Id = (int)jsonObject.GetSafeNumber("id");
            Name = jsonObject.GetSafeString("name");
            ProfilePath = jsonObject.GetSafeString("profile_path");
        }
        #endregion

        #region image uri's 
        /// <summary>
        /// Uri to the profile image.
        /// </summary>
        /// <param name="size">The size for the image as required</param>
        /// <returns>The uri to the sized image</returns>
        public Uri Uri(ProfileSize size)
        {
            return Extensions.MakeImageUri(size.ToString(), ProfilePath);
        }
        #endregion

        #region navigation properties
        /// <summary>
        /// Retrieves the associated person.
        /// </summary>
        public async Task<Person> PersonAsync()
        {
            return await TheMovieDb.GetPersonAsync(Id);
        }
        #endregion
    }
}
