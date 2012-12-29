using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TmdbWrapper.Utilities;
using Windows.Data.Json;

namespace TmdbWrapper.Persons
{
    /// <summary>
    /// Image
    /// </summary>
    public class Image : ITmdbObject
    {
        /// <summary>
        /// Path of the image
        /// </summary>
        public string FilePath { get; set; }
        /// <summary>
        /// Width of the image.
        /// </summary>
        public int Width { get; set; }
        /// <summary>
        /// Height of the image.
        /// </summary>
        public int Height { get; set; }
        /// <summary>
        /// Code of the language this image is stated in.
        /// </summary>
        public string iso639_1 { get; set; }
        /// <summary>
        /// Aspect ratio of the image.
        /// </summary>
        public double AspectRatio { get; set; }

        void ITmdbObject.ProcessJson(JsonObject jsonObject)
        {
            FilePath = jsonObject.GetNamedValue("file_path").GetSafeString();
            Width = (int)jsonObject.GetNamedValue("width").GetSafeNumber();
            Height = (int)jsonObject.GetNamedValue("height").GetSafeNumber();
            iso639_1 = jsonObject.GetNamedValue("iso_639_1").GetSafeString();
            AspectRatio = jsonObject.GetNamedValue("aspect_ratio").GetSafeNumber();
        }
    }
}
