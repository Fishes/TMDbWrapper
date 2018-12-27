using System;
using System.Threading.Tasks;
using TmdbWrapper.Utilities;

namespace TmdbWrapper.Persons
{
    /// <summary>
    /// A member of the cast in a movie.
    /// </summary>
    public class CastPerson : ITmdbObject
    {
        #region properties

        /// <summary>
        /// Id of the person.
        /// </summary>
        public int Id { get; private set; }

        /// <summary>
        /// Name of the person.
        /// </summary>
        public string Name { get; private set; }

        /// <summary>
        /// Name of the character that is played.
        /// </summary>
        public string Character { get; private set; }

        /// <summary>
        /// Order of the character in the credits
        /// </summary>
        public int Order { get; private set; }

        /// <summary>
        /// Path of the profile picture
        /// </summary>
        public string ProfilePath { get; private set; }

        public Gender Gender { get; private set; }

        public MediaType Media { get; internal set; }

        #endregion properties

        #region overrides

        /// <summary>
        /// Returns this instances ToString
        /// </summary>
        public override string ToString()
        {
            return Name;
        }

        #endregion overrides

        #region interface implementations

        void ITmdbObject.ProcessJson(JsonObject jsonObject)
        {
            Id = (int)jsonObject.GetSafeNumber("id");
            Name = jsonObject.GetSafeString("name");
            Character = jsonObject.GetSafeString("character");
            Order = (int)jsonObject.GetSafeNumber("order");
            ProfilePath = jsonObject.GetSafeString("profile_path");
            Gender = (Gender)jsonObject.GetSafeInteger("gender");
            //Media = (MediaType)jsonObject.GetSafeInteger("media_type");
        }

        #endregion interface implementations

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

        #endregion image uri's

        #region navigation properties

        /// <summary>
        /// Retrieves the associated person
        /// </summary>
        public async Task<Person> PersonAsync()
        {
            return await TheMovieDb.GetPersonAsync(Id);
        }

        #endregion navigation properties
    }
}