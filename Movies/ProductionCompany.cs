using System.Threading.Tasks;
using TmdbWrapper.Companies;
using TmdbWrapper.Utilities;

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
        void ITmdbObject.ProcessJson(JSONObject jsonObject)
        {
            Id = (int)jsonObject.GetSafeNumber("id");
            Name = jsonObject.GetSafeString("name");
        }
        #endregion

        #region navigation properties
        /// <summary>
        /// Retrieves the associated company.
        /// </summary>
        public async Task<Company> CompanyAsync()
        {
            return await TheMovieDb.GetCompanyAsync(Id);
        }
        #endregion
    }
}
