using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace TmdbWrapper.Search
{
    /// <summary>
    /// A page of search results
    /// </summary>
    /// <typeparam name="T">The type that are searched.</typeparam>
    public class SearchResult<T>
    {
        #region properties
        /// <summary>
        /// Page number of this set.
        /// </summary>
        public int Page { get; internal set; }
        /// <summary>
        /// List with search results.
        /// </summary>
        public List<T> Results { get; internal set; }
        /// <summary>
        /// Total number of search pages.
        /// </summary>
        public int TotalPages { get; internal set; }
        /// <summary>
        /// Total number of results
        /// </summary>
        public int TotalResults { get; internal set; }
        #endregion
    }
}
