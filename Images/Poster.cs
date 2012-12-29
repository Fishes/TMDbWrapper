using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.Data.Json;
using TmdbWrapper.Utilities;

namespace TmdbWrapper.Image
{
    /// <summary>
    /// A movie image
    /// </summary>
    public class Poster : ITmdbObject
    {
        #region properties
        /// <summary>
        /// Path of this image.
        /// </summary>
        public string FilePath { get; private set; }
        /// <summary>
        /// Width of this image.
        /// </summary>
        public int Width { get; private set; }
        /// <summary>
        /// Height of this image.
        /// </summary>
        public int Height { get; private set; }
        /// <summary>
        /// Code of this images language.
        /// </summary>
        public string Iso639_1 { get; private set; }
        /// <summary>
        /// Aspect ratio
        /// </summary>
        public double AspectRatio { get; private set; }
        /// <summary>
        /// Average of votes
        /// </summary>
        public double VoteAverage { get; private set; }
        /// <summary>
        /// Number of votes.
        /// </summary>
        public int VoteCount { get; private set; }
        #endregion

        #region interface implementations
        void ITmdbObject.ProcessJson(JsonObject jsonObject)
        {
            FilePath = jsonObject.GetNamedValue("file_path").GetSafeString();
            Width = (int)jsonObject.GetNamedValue("width").GetSafeNumber();
            Height = (int)jsonObject.GetNamedValue("height").GetSafeNumber();
            Iso639_1 = jsonObject.GetNamedValue("iso_639_1").GetSafeString();
            AspectRatio = jsonObject.GetNamedValue("aspect_ratio").GetSafeNumber();
            VoteAverage = jsonObject.GetNamedValue("vote_average").GetSafeNumber();
            VoteCount = (int)jsonObject.GetNamedValue("vote_count").GetSafeNumber();
        }
        #endregion

        #region image uri's
        /// <summary>
        /// Uri to the profile image.
        /// </summary>
        /// <param name="size">The size for the image as required</param>
        /// <returns>The uri to the sized image</returns>
        public Uri Uri(PosterSize size)
        {
            return Utilities.Extensions.MakeImageUri(size.ToString(), FilePath);
        }
        #endregion
    }
}
