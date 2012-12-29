using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TmdbWrapper.Utilities;
using Windows.Data.Json;

namespace TmdbWrapper.Movies
{
    /// <summary>
    /// A production company
    /// </summary>
    public class ProductionCompany : ITmdbObject
    {
        #region properties
        /// <summary>
        /// Name of the production company
        /// </summary>
        public string Name { get; private set; }
        /// <summary>
        /// Id of the production company
        /// </summary>
        public int Id { get; private set; }
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
        void ITmdbObject.ProcessJson(JsonObject jsonObject)
        {
            Id = (int)jsonObject.GetNamedValue("id").GetSafeNumber();
            Name = jsonObject.GetNamedValue("name").GetSafeString();
        }
        #endregion

        #region navigation properties
        /// <summary>
        /// Retrieves the associated company.
        /// </summary>
        public async Task<Companies.Company> Company()
        {
            return await TheMovieDb.GetCompany(Id);
        }
        #endregion
    }
}
