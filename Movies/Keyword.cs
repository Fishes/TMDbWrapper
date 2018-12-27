﻿using TmdbWrapper.Utilities;

namespace TmdbWrapper.Movies
{
    /// <summary>
    /// Keywords for this movie
    /// </summary>
    public class Keyword : ITmdbObject
    {
        #region properties

        /// <summary>
        /// Id of the keyword
        /// </summary>
        public int Id { get; private set; }

        /// <summary>
        /// Name of the keyword
        /// </summary>
        public string Name { get; private set; }

        #endregion properties

        #region interface implementations

        void ITmdbObject.ProcessJson(JsonObject jsonObject)
        {
            Id = (int)jsonObject.GetSafeNumber("id");
            Name = jsonObject.GetSafeString("name");
        }

        #endregion interface implementations

        #region overrides

        /// <summary>
        /// Returns this instances ToString
        /// </summary>
        public override string ToString()
        {
            return Name;
        }

        #endregion overrides
    }
}