using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
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
            Request<Person> request = new Request<Person>("person/" + PersonID.ToString());
            return await request.ProcesRequestAsync();
                Request<Person> request = new Request<Person>("person/" + PersonID.ToString());
                if (extras != 0)
                {
                    request.AddParameter("append_to_response", extras.ToString().Replace(" ", ""));
                }
                result = await request.ProcesRequestAsync();
            return result;
        }

        /// <summary>
        /// Get the credits of a specific movie.
        /// </summary>
        /// <param name="PersonID">The id of the person.</param>
        /// <returns>The credits.</returns>
        public static async Task<Credit> GetCreditsAsync(int PersonID)
        {
            Request<Credit> request = new Request<Credit>("person/" + PersonID.ToString() + "/credits");
            return await request.ProcesRequestAsync();
        }

        /// <summary>
        /// The images of a specific person.
        /// </summary>
        /// <param name="PersonID">The id of the specified person.</param>
        /// <returns>A list of images</returns>
        public static async Task<IReadOnlyList<Profile>> GetImageAsync(int PersonID)
        {
            Request<Profile> request = new Request<Profile>("person/" + PersonID.ToString() +"/images");
            return await request.ProcesRequestListAsync("profiles");
        }
    }
}
