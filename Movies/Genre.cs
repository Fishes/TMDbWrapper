using TmdbWrapper.Utilities;

namespace TmdbWrapper.Movies
{
    /// <summary>
    /// Genre.
    /// </summary>
    public class Genre : ITmdbObject
    {
        #region properties
        /// <summary>
        /// Id of this genre.
        /// </summary>
        public int Id { get; private set; }
        /// <summary>
        /// Name of this genre.
        /// </summary>
        public string Name { get; private set; }
        #endregion

        #region overrides
        /// <summary>
        /// Returns this instance ToString
        /// </summary>        
        public override string ToString()
        {
            return Name;
        }
        #endregion

        #region interface implementations
        void ITmdbObject.ProcessJson(JSONObject jsonObject)
        {
            Id = (int)jsonObject.GetSafeNumber("id");
            Name = jsonObject.GetSafeString("name");
        }
        #endregion
    }
}
