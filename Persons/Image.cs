using TmdbWrapper.Utilities;

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
        void ITmdbObject.ProcessJson(JSONObject jsonObject)
        {
            FilePath = jsonObject.GetSafeString("file_path");
            Width = (int)jsonObject.GetSafeNumber("width");
            Height = (int)jsonObject.GetSafeNumber("height");
            iso639_1 = jsonObject.GetSafeString("iso_639_1");
            AspectRatio = jsonObject.GetSafeNumber("aspect_ratio");
        }
        #endregion
    }
}
