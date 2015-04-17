using System;
using System.Threading.Tasks;
using TmdbWrapper.Persons;
using TmdbWrapper.Utilities;

namespace TmdbWrapper.Movies
{
    /// <summary>
    /// A member of the cast in a movie.
    /// </summary>
    public class CrewPerson : ITmdbObject
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
        /// Name of the job that is fullfilled.
        /// </summary>
        public string Job { get; private set; }
        /// <summary>
        /// The department of the job.
        /// </summary>
        public string Department { get; private set; }
        /// <summary>
        /// Path of the profile picture
        /// </summary>
        public string ProfilePath { get; private set; }
        #endregion

        #region overrides
        /// <summary>
        /// Returns this instances ToString
        /// </summary>        
        public override string ToString()
        {
            return Name;
        }
        #endregion

        #region interface implementations
        void ITmdbObject.ProcessJson(JSONObject jsonObject)
        {
            Id = (int)jsonObject.GetSafeNumber("id");
            Name = jsonObject.GetSafeString("name");
            Job = jsonObject.GetSafeString("Job");
            Department = jsonObject.GetSafeString("department");
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
        /// Retrieves the associated person
        /// </summary>
        public async Task<Person> PersonAsync()
        {
            return await TheMovieDb.GetPersonAsync(Id);
        }
        #endregion
    }
}
