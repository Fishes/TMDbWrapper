using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TmdbWrapper.Persons;
using TmdbWrapper.Utilities;

namespace TmdbWrapper
{
    public static partial class TheMovieDb
    {
        /// <summary>
        /// Gets the information of the specified person.
        /// </summary>
        /// <param name="PersonID">The id of the person.</param>
        /// <returns>The person.</returns>
        public static async Task<Person> GetPerson(int PersonID)
        {
            Request<Person> request = new Request<Person>("person/" + PersonID.ToString());
            return await request.ProcesRequestAsync();
        }

        /// <summary>
        /// Get the credits of a specific movie.
        /// </summary>
        /// <param name="PersonID">The id of the person.</param>
        /// <returns>The credits.</returns>
        public static async Task<Credit> GetCredits(int PersonID)
        {
            Request<Credit> request = new Request<Credit>("person/" + PersonID.ToString() + "/credits");
            return await request.ProcesRequestAsync();
        }

        /// <summary>
        /// The images of a specific person.
        /// </summary>
        /// <param name="PersonID">The id of the specified person.</param>
        /// <returns>A list of images</returns>
        public static async Task<IList<Image>> GetImage(int PersonID)
        {
            Request<Image> request = new Request<Image>("person/" + PersonID.ToString() +"/images");
            return await request.ProcesRequestListAsync("profiles");
        }
    }
}
