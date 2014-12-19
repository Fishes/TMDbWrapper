using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using TmdbWrapper.Utilities;

namespace TmdbWrapper.TV
{
    public class Creator : ITmdbObject
    {
        public int Id { get; private set; }

        public string Name { get; private set; }

        public string ProfilePath { get; private set; }

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
            return Utilities.Extensions.MakeImageUri(size.ToString(), ProfilePath);
        }
        #endregion
    }
}
