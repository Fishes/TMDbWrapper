using System.Collections.Generic;
using TmdbWrapper.Utilities;

namespace TmdbWrapper.Images
{
    /// <summary>
    /// Images of a movie
    /// </summary>
    public class Image : ITmdbObject
    {
        #region properties
        /// <summary>
        /// Id of the movie
        /// </summary>
        public int Id { get; private set; }
        /// <summary>
        /// List of backdrops
        /// </summary>
        public IReadOnlyList<Backdrop> Backdrops { get; private set; }
        /// <summary>
        /// List of posters
        /// </summary>
        public IReadOnlyList<Poster> Posters { get; private set; }
        #endregion

        #region interface implementations
        void ITmdbObject.ProcessJson(JSONObject jsonObject)
        {
            Id = (int)jsonObject.GetSafeNumber("id");
            Backdrops = jsonObject.ProcessObjectArray<Backdrop>("backdrops");
            Posters = jsonObject.ProcessObjectArray<Poster>("posters");
        }
        #endregion
    }
}
