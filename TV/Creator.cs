using System;
using TmdbWrapper.Utilities;

namespace TmdbWrapper.TV
{
    /// <summary>
    /// A creator of a tv series
    /// </summary>
    public class Creator : ITmdbObject
    {
        /// <summary>
        /// Id of the creator
        /// </summary>
        public int Id { get; private set; }
        /// <summary>
        /// Name of the creator
        /// </summary>
        public string Name { get; private set; }
        /// <summary>
        /// Path to the profile picture
        /// </summary>
        public string ProfilePath { get; private set; }
        /// <summary>
        /// Returns the name of the creator
        /// </summary>
        /// <returns>The name</returns>
        public override string ToString()
        {
            return Name;
        }

        void ITmdbObject.ProcessJson(JSONObject jsonObject)
        {
            Id = (int)jsonObject.GetSafeNumber("id");
            Name = jsonObject.GetSafeString("name");
            ProfilePath = jsonObject.GetSafeString("profile_path");
        }

        #region Image Uri's
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
