using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TmdbWrapper.Cache;
using TmdbWrapper.Persons;
using TmdbWrapper.Utilities;

namespace TmdbWrapper
{
    /// <summary>
    /// Enumeration of extras that should be prefilled on retrieving the person info
    /// </summary>
    [Flags]
    public enum PersonExtras
    {
        /// <summary>
        /// Retrieve the credits
        /// </summary>
        credits = 1
    }

    public static partial class TheMovieDb
    {
        /// <summary>
        /// Gets the information of the specified person.
        /// </summary>
        /// <param name="PersonID">The id of the person.</param>
        /// <param name="extras">Indicates which parts should be prefetched.</param>
        /// <returns>The person.</returns>
        public static async Task<Person> GetPersonAsync(int PersonID, PersonExtras extras = 0)
        {
            Person result = DatabaseCache.GetObject<Person>(PersonID);
            if (result == null)
            {
                Request<Person> request = new Request<Person>("person/" + PersonID.ToString());
                if (extras != 0)
                {
                    request.AddParameter("append_to_response", extras.ToString().Replace(" ", ""));
                }
                result = await request.ProcesRequestAsync();
                DatabaseCache.SetObject(PersonID, result);
            }
            return result;
        }

        /// <summary>
        /// Get the credits of a specific movie.
        /// </summary>
        /// <param name="PersonID">The id of the person.</param>
        /// <returns>The credits.</returns>
        public static async Task<Credit> GetCreditsAsync(int PersonID)
        {
            Credit credits = DatabaseCache.GetObject<Credit>(PersonID);
            if (credits == null)
            {
                Request<Credit> request = new Request<Credit>("person/" + PersonID.ToString() + "/credits");
                credits = await request.ProcesRequestAsync();
                DatabaseCache.SetObject(PersonID, credits);
            }
            return credits;
        }

        /// <summary>
        /// The images of a specific person.
        /// </summary>
        /// <param name="PersonID">The id of the specified person.</param>
        /// <returns>A list of images</returns>
        public static async Task<IReadOnlyList<Profile>> GetImageAsync(int PersonID)
        {
            IReadOnlyList<Profile> profile = DatabaseCache.GetObject<IReadOnlyList<Profile>>(PersonID);
            if (profile == null)
            {
                Request<Profile> request = new Request<Profile>("person/" + PersonID.ToString() + "/images");
                profile = await request.ProcesRequestListAsync("profiles");
                DatabaseCache.SetObject(PersonID, profile);
            }
            return profile;
        }
    }
}
