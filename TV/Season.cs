using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using TmdbWrapper.Utilities;

namespace TmdbWrapper.TV
{
    public class Season : ITmdbObject
    {
        public string AirDate { get; private set; }

        public int Id { get; private set; }

        public string PosterPath { get; private set; }

        public int SeasonNumber { get; private set; }

        void ITmdbObject.ProcessJson(JSONObject jsonObject)
        {
            AirDate = jsonObject.GetSafeString("air_date");
            Id = (int)jsonObject.GetSafeNumber("id");
            PosterPath = jsonObject.GetSafeString("poster_path");
            SeasonNumber = (int)jsonObject.GetSafeNumber("season_number");
        }

        #region Image Uri's
        /// <summary>
        /// Uri to the poster image.
        /// </summary>
        /// <param name="size">The size for the image as required</param>
        /// <returns>The uri to the sized image</returns>
        public Uri Uri(PosterSize size)
        {
            return Utilities.Extensions.MakeImageUri(size.ToString(), PosterPath);
        }
        #endregion
    }
}
