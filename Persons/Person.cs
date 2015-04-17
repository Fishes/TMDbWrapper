using System;
using System.Collections.Generic;
using TmdbWrapper.Utilities;

namespace TmdbWrapper.Persons
{
    /// <summary>
    /// Person
    /// </summary>
    public class Person : ITmdbObject
    {
        #region private fields
        private Credit _credits;
        #endregion

        #region properties
        /// <summary>
        /// Indicates wether this person is an adult actor
        /// </summary>
        public bool Adult { get; private set; }
        /// <summary>
        /// Aliases this person is known by.
        /// </summary>
        public IReadOnlyList<string> Aliases { get; private set; }
        /// <summary>
        /// Biography of this person.
        /// </summary>
        public string Biography { get; private set; }
        /// <summary>
        /// Birthday 
        /// </summary>
        public DateTime? Birthday { get; private set; }
        /// <summary>
        /// Date of death
        /// </summary>
        public DateTime? Deathday { get; private set; }
        /// <summary>
        /// Uri of possible homepage.
        /// </summary>
        public Uri Homepage { get; private set; }
        /// <summary>
        /// Id of this person
        /// </summary>
        public int Id { get; private set; }
        /// <summary>
        /// Name of this person.
        /// </summary>
        public string Name { get; private set; }
        /// <summary>
        /// Place of birth
        /// </summary>
        public string PlaceOfBirth { get; private set; }
        /// <summary>
        /// Path of the profile.
        /// </summary>
        public string ProfilePath { get; private set; }
        /// <summary>
        /// Gets the credits associated to this person.
        /// </summary>
        public Credit Credits
        {
            get { return _credits ?? (_credits = TheMovieDb.GetCreditsAsync(Id).Result); }
        }
        #endregion

        #region interface implementations
        void ITmdbObject.ProcessJson(JSONObject jsonObject)
        {
            Adult = jsonObject.GetSafeBoolean("adult");
            Aliases = jsonObject.ProcessStringArray("also_known_as");
            Biography = jsonObject.GetSafeString("biography");
            Birthday = jsonObject.GetSafeDateTime("birthday");
            Deathday = jsonObject.GetSafeDateTime("deathday");
            Homepage = new Uri(jsonObject.GetSafeString("homepage"));
            Id = (int)jsonObject.GetSafeNumber("id");
            Name = jsonObject.GetSafeString("name");
            PlaceOfBirth = jsonObject.GetSafeString("place_of_birth");
            ProfilePath = jsonObject.GetSafeString("profile_path");
            _credits = jsonObject.ProcessObject<Credit>("credits");
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
    }
}
