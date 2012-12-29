using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using TmdbWrapper;
using TmdbWrapper.Search;
using TmdbWrapper.Utilities;
using Windows.Data.Json;

namespace TmdbWrapper
{
    /// <summary>
    /// The static that wraps The movie database service.
    /// </summary>
    public static partial class TheMovieDatabase
    {
        /// <summary>
        /// The apikey that is used in all requests.
        /// </summary>
        public static string ApiKey { get; set; }
        /// <summary>
        /// Language all the request uses if entered.
        /// </summary>
        public static string Language { get; set; }

        /// <summary>
        /// Initialises the wrapper.
        /// </summary>
        /// <param name="apiKey">The apikey the requests will use.</param>       
        public static void InitialiseTmdb(string apiKey) 
        {
            ApiKey = apiKey;
        }

        /// <summary>
        /// Initialises the wrapper.
        /// </summary>
        /// <param name="apiKey">The apikey the request will use.</param>       
        /// <param name="language">The language the requests will use.</param>
        public static void InitialiseTmdb(string apiKey, string language)
        {
            ApiKey = apiKey;
            Language = language;
        }        

        
    }
}
