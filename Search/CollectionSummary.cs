using System;
using TmdbWrapper.Utilities;

namespace TmdbWrapper.Search
{
    /// <summary>
    /// Summary for a collection
    /// </summary>
    public class CollectionSummary : ITmdbObject
    {
        #region properties
        /// <summary>
        /// Id of the collection.
        /// </summary>
        public int Id { get; private set; }
        /// <summary>
        /// Path of the backdrop path.
        /// </summary>
        public string BackdropPath { get; private set; }
        /// <summary>
        /// Name of the collection
        /// </summary>
        public string Name { get; private set; }
        /// <summary>
        /// Path of the poster for this collection
        /// </summary>
        public string PosterPath { get; private set; }
        #endregion

        #region overrides
        /// <summary>
        /// Return this instances ToString
        /// </summary>
        /// <returns></returns>
        public override string ToString()
        {
            return Name;
        }
        #endregion

        #region interface implementations
        void ITmdbObject.ProcessJson(JSONObject jsonObject)
        {
            Id = (int)jsonObject.GetSafeNumber("id");
            BackdropPath = jsonObject.GetSafeString("backdrop_path");
            Name = jsonObject.GetSafeString("name");
            PosterPath = jsonObject.GetSafeString("poster_path");
        }
        #endregion

        #region image uri's
        /// <summary>
        /// Uri to the backdrop image.
        /// </summary>
        /// <param name="size">The size for the image as required</param>
        /// <returns>The uri to the sized image</returns>
        public Uri Uri(BackdropSize size)
        {
            return Extensions.MakeImageUri(size.ToString(), BackdropPath);
        }

        /// <summary>
        /// Uri to the poster image.
        /// </summary>
        /// <param name="size">The size for the image as required</param>
        /// <returns>The uri to the sized image</returns>
        public Uri Uri(PosterSize size)
        {
            return Extensions.MakeImageUri(size.ToString(), PosterPath);
        }
        #endregion
    }
}
