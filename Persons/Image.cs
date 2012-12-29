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
    public class Profile : ITmdbObject
    {
        #region properties
        /// <summary>
        /// Path of the image
        /// </summary>
        public string FilePath { get; private set; }
        /// <summary>
        /// Width of the image.
        /// </summary>
        public int Width { get; private set; }
        /// <summary>
        /// Height of the image.
        /// </summary>
        public int Height { get; private set; }
        /// <summary>
        /// Code of the language this image is stated in.
        /// </summary>
        public string iso639_1 { get; private set; }
        /// <summary>
        /// Aspect ratio of the image.
        /// </summary>
        public double AspectRatio { get; private set; }
        #endregion

        #region interface implememtations
        void ITmdbObject.ProcessJson(JsonObject jsonObject)
        {
            FilePath = jsonObject.GetNamedValue("file_path").GetSafeString();
            Width = (int)jsonObject.GetNamedValue("width").GetSafeNumber();
            Height = (int)jsonObject.GetNamedValue("height").GetSafeNumber();
            iso639_1 = jsonObject.GetNamedValue("iso_639_1").GetSafeString();
            AspectRatio = jsonObject.GetNamedValue("aspect_ratio").GetSafeNumber();
        }
        #endregion
    }
}
