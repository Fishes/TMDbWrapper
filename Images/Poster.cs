using System;
using TmdbWrapper.Utilities;

namespace TmdbWrapper.Images
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
        void ITmdbObject.ProcessJson(JSONObject jsonObject)
        {
            FilePath = jsonObject.GetSafeString("file_path");
            Width = (int)jsonObject.GetSafeNumber("width");
            Height = (int)jsonObject.GetSafeNumber("height");
            Iso639_1 = jsonObject.GetSafeString("iso_639_1");
            AspectRatio = jsonObject.GetSafeNumber("aspect_ratio");
            VoteAverage = jsonObject.GetSafeNumber("vote_average");
            VoteCount = (int)jsonObject.GetSafeNumber("vote_count");
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
            return Extensions.MakeImageUri(size.ToString(), FilePath);
        }
        #endregion
    }
}
