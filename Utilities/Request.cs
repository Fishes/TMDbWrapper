using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using TmdbWrapper.Search;
using Windows.Data.Json;

namespace TmdbWrapper.Utilities
{
    internal class Request<T> where T : ITmdbObject, new()
    {
        private const string BASE_URL = @"http://api.themoviedb.org/3/";   

        public string ApiName { get; private set; }
        private IDictionary<string, string> Parameters = new Dictionary<string, string>();

        public Request(string apiName)
        {
            ApiName = apiName;
            AddParameter("api_key", TheMovieDb.ApiKey);
        }

        public void AddParameter(string key, string value)
        {
            Parameters.Add(key, value);
        }

        public void AddParameter(string key, object value)
        {
            AddParameter(key, value.ToString());
        }

        public string RequestUrl
        {
            get 
            {
                StringBuilder sb = new StringBuilder();
                sb.Append(ApiName);
                sb.Append("?");
                var args = from n in Parameters
                           select n.Key + "=" + n.Value;
                sb.Append(string.Join("&", args));
                return sb.ToString();
            }
        }

        public async Task<T> ProcesRequestAsync() 
        {
            HttpClient client = new HttpClient();
            client.DefaultRequestHeaders.Accept.Add(new System.Net.Http.Headers.MediaTypeWithQualityHeaderValue("application/json"));

            string response = await client.GetStringAsync(BASE_URL + RequestUrl);
            JsonObject jsonObject = JsonObject.Parse(response);

            T result = new T();
            result.ProcessJson(jsonObject);

            return result;
        }

        public async Task<SearchResult<T>> ProcessSearchRequestAsync()
        {
            if (Parameters["page"] == "0")
            {
                Parameters["page"] = "1";
                SearchResult<T> result = await GetSearchResponseAsync();
                for (int i = 2; i <= result.TotalPages; i++)
                {
                    Parameters["page"] = i.ToString();
                    SearchResult<T> subResult = await GetSearchResponseAsync();
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
            HttpClient client = new HttpClient();
            client.DefaultRequestHeaders.Accept.Add(new System.Net.Http.Headers.MediaTypeWithQualityHeaderValue("application/json"));

            string response = await client.GetStringAsync(BASE_URL + RequestUrl);
            JsonObject jsonObject = JsonObject.Parse(response);

            SearchResult<T> result = new SearchResult<T>();
            result.Page = (int)jsonObject.GetNamedNumber("page");
            result.TotalPages = (int)jsonObject.GetNamedNumber("total_pages");
            result.TotalResults = (int)jsonObject.GetNamedNumber("total_results");

            var jsonObjects = from obj in jsonObject.GetNamedArray("results")
                              select obj;
            result.Results = new List<T>();
            foreach (var jsonObj in jsonObjects)
            {
                T newT = new T();
                newT.ProcessJson(jsonObj.GetObject());
                result.Results.Add(newT);
            }
            return result;
        }

        public async Task<IReadOnlyList<T>> ProcesRequestListAsync(string valueName)
        {
            HttpClient client = new HttpClient();
            client.DefaultRequestHeaders.Accept.Add(new System.Net.Http.Headers.MediaTypeWithQualityHeaderValue("application/json"));

            string response = await client.GetStringAsync(BASE_URL + RequestUrl);
            JsonObject jsonObject = JsonObject.Parse(response);
            IReadOnlyList<T> result = jsonObject.GetNamedValue("valueName").ProcessArray<T>();
            return result;
        }
    }
}
