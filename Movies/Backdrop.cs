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
    /// A movie image
    /// </summary>
    public class Backdrop : ITmdbObject
    {
        /// <summary>
        /// Path of this image.
        /// </summary>
        public string FilePath { get; set; }
        /// <summary>
        /// Width of this image.
        /// </summary>
        public int Width { get; set; }
        /// <summary>
        /// Height of this image.
        /// </summary>
        public int Height { get; set; }
        /// <summary>
        /// Code of this images language.
        /// </summary>
        public string Iso639_1 { get; set; }
        /// <summary>
        /// Aspect ratio
        /// </summary>
        public double AspectRatio { get; set; }
        /// <summary>
        /// Average of votes
        /// </summary>
        public double VoteAverage { get; set; }
        /// <summary>
        /// Number of votes.
        /// </summary>
        public int VoteCount { get; set; }
         
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

        /// <summary>
        /// Gives the Uri for the backdrop image
        /// </summary>
        /// <param name="size">The requested size of the image</param>
        /// <returns>Uri to the sized image</returns>
        public Uri Uri(BackdropSize size)
        {
            return Utilities.Extensions.MakeImageUri(size.ToString(), FilePath);
        }
    }
}
