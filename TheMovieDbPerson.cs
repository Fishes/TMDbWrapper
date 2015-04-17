using System;
using System.Collections.Generic;
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
        Credits = 1
    }

    public static partial class TheMovieDb
    {
        /// <summary>
        /// Gets the information of the specified person.
        /// </summary>
        /// <param name="personId">The id of the person.</param>
        /// <param name="extras">Indicates which parts should be prefetched.</param>
        /// <returns>The person.</returns>
        public static async Task<Person> GetPersonAsync(int personId, PersonExtras extras = 0)
        {
            var result = DatabaseCache.GetObject<Person>(personId);
            if (result == null)
            {
                var request = new Request<Person>("person/" + personId);
                if (extras != 0)
                {
                    request.AddParameter("append_to_response", extras.ToString().Replace(" ", ""));
                }
                result = await request.ProcesRequestAsync();
                DatabaseCache.SetObject(personId, result);
            }
            return result;
        }

        /// <summary>
        /// Get the credits of a specific movie.
        /// </summary>
        /// <param name="personId">The id of the person.</param>
        /// <returns>The credits.</returns>
        public static async Task<Credit> GetCreditsAsync(int personId)
        {
            var credits = DatabaseCache.GetObject<Credit>(personId);
            if (credits == null)
            {
                var request = new Request<Credit>("person/" + personId + "/credits");
                credits = await request.ProcesRequestAsync();
                DatabaseCache.SetObject(personId, credits);
            }
            return credits;
        }

        /// <summary>
        /// The images of a specific person.
        /// </summary>
        /// <param name="personId">The id of the specified person.</param>
        /// <returns>A list of images</returns>
        public static async Task<IReadOnlyList<Profile>> GetImageAsync(int personId)
        {
            var profile = DatabaseCache.GetObject<IReadOnlyList<Profile>>(personId);
            if (profile == null)
            {
                var request = new Request<Profile>("person/" + personId + "/images");
                profile = await request.ProcesRequestListAsync("profiles");
                DatabaseCache.SetObject(personId, profile);
            }
            return profile;
        }
    }
}
