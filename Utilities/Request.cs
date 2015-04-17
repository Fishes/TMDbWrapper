using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using TmdbWrapper.Search;

namespace TmdbWrapper.Utilities
{
    internal class Request<T> where T : ITmdbObject, new()
    {
        private static string _baseUrl = @"http://api.themoviedb.org/3/";
        private const string BaseNonsecureUrl = @"http://api.themoviedb.org/3/";
        private const string BaseSecureUrl = @"https://api.themoviedb.org/3/";   

        public string ApiName { get; private set; }
        private readonly IDictionary<string, string> _parameters = new Dictionary<string, string>();

        internal static void Initialize(bool useSecureConnection)
        {
            _baseUrl = useSecureConnection ? BaseSecureUrl : BaseNonsecureUrl;
        }

        public Request(string apiName)
        {
            if (string.IsNullOrWhiteSpace(TheMovieDb.ApiKey))
            {
                throw new ArgumentNullException("apiName", "The library was not initialized correctly. Please specify an API_KEY.");
            }
            ApiName = apiName;
            AddParameter("api_key", TheMovieDb.ApiKey);
        }

        public void AddParameter(string key, string value)
        {
            _parameters.Add(key, value);
        }

        public void AddParameter(string key, object value)
        {
            AddParameter(key, value.ToString());
        }

        public string RequestUrl
        {
            get 
            {
                var sb = new StringBuilder();
                sb.Append(ApiName);
                sb.Append("?");
                var args = from n in _parameters
                           select n.Key + "=" + n.Value;
                sb.Append(string.Join("&", args));
                return sb.ToString();
            }
        }

        public async Task<T> ProcesRequestAsync() 
        {
            var client = new HttpClient();
            client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

            var response = await client.GetStringAsync(_baseUrl + RequestUrl);
            var jsonObject = new JSONObject(response);

            var result = new T();
            result.ProcessJson(jsonObject);

            return result;
        }

        public async Task<SearchResult<T>> ProcessSearchRequestAsync()
        {
            if (_parameters["page"] == "0")
            {
                _parameters["page"] = "1";
                var result = await GetSearchResponseAsync();
                for (var i = 2; i <= result.TotalPages; i++)
                {
                    _parameters["page"] = i.ToString();
                    var subResult = await GetSearchResponseAsync();
                    result.Results.AddRange(subResult.Results);
                }
                result.TotalPages = 1;
                return result;
            }
            else
            {
                return await GetSearchResponseAsync();
            }
        
        }

        private async Task<SearchResult<T>> GetSearchResponseAsync()
        {
            var client = new HttpClient();
            client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

            var response = await client.GetStringAsync(_baseUrl + RequestUrl);
            var jsonObject = new JSONObject(response);

            var result = new SearchResult<T>
            {
                Page = (int) jsonObject.GetSafeNumber("page"),
                TotalPages = (int) jsonObject.GetSafeNumber("total_pages"),
                TotalResults = (int) jsonObject.GetSafeNumber("total_results"),
                Results = new List<T>()
            };

            foreach (var jsonObj in jsonObject.GetNamedArray("results"))
            {
                var newT = new T();
                newT.ProcessJson(jsonObj);
                result.Results.Add(newT);
            }
            return result;
        }

        public async Task<IReadOnlyList<T>> ProcesRequestListAsync(string valueName)
        {
            var client = new HttpClient();
            client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

            var response = await client.GetStringAsync(_baseUrl + RequestUrl);
            var jsonObject = new JSONObject(response);
            var result = jsonObject.ProcessObjectArray<T>(valueName);
            return result;
        }
    }
}
