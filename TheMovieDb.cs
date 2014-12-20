using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using TmdbWrapper;
using TmdbWrapper.Configuration;
using TmdbWrapper.Search;
using TmdbWrapper.Utilities;

namespace TmdbWrapper
{
    /// <summary>
    /// The static that wraps The movie database service.
    /// It should be initilised with your API_KEY.
    /// You can apply for an API_KEY at www.TheMovieDb.org
    /// </summary>
    public static partial class TheMovieDb
    {
        /// <summary>
        /// The apikey that is used in all requests.
        /// </summary>
        internal static string ApiKey { get; private set; }
        /// <summary>
        /// Language all the request uses if entered.
        /// </summary>
        internal static string Language { get; private set; }

        internal static bool UseSecureConnections { get; set; }
        internal static Configuration.Configuration Configuration { get; private set; }

        /// <summary>
        /// Initialises the wrapper.
        /// </summary>
        /// <param name="apiKey">The apikey the requests will use.</param>  
        /// <param name="useSecureConnections">Inidicates if a secure connection should be used.</param>
        public static void Initialise(string apiKey, bool useSecureConnections = true) 
        {
            ApiKey = apiKey;
            UseSecureConnections = useSecureConnections;
            Configuration.Configuration config = GetConfig();
            Extensions.Initialize(useSecureConnections ? config.ImageConfiguration.SecureBaseUrl : config.ImageConfiguration.BaseUrl);
        }

        /// <summary>
        /// Initialises the wrapper.
        /// </summary>
        /// <param name="apiKey">The apikey the request will use.</param>       
        /// <param name="language">The language the requests will use.</param>
        /// <param name="useSecureConnections">Inidicates if a secure connection should be used.</param>
        public static void Initialise(string apiKey, string language, bool useSecureConnections = true) 
        {
            Initialise(apiKey, useSecureConnections);
            Language = language;
        }

        private static Configuration.Configuration GetConfig()
        {
            Request<Configuration.Configuration>.Initialize(UseSecureConnections);
            Request<Configuration.Configuration> request = new Request<Configuration.Configuration>("configuration");
            Configuration.Configuration config = request.ProcesRequestAsync().Result;
            return config;
        }
    }
}
