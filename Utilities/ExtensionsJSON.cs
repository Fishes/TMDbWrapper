using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using TmdbWrapper;

namespace TmdbWrapper.Utilities
{
    internal class JSONObject
    {
        private JToken jsonObject;

        internal JSONObject(JToken json)
        {
            jsonObject = json;
        }

        internal JSONObject(string json) 
        {
            jsonObject = JObject.Parse(json);
        }

        #region jsonValue extensions

        internal IEnumerable<JSONObject> GetNamedArray(string name)
        {
            return from obj in (jsonObject[name] as JArray)
                   select new JSONObject(obj);
        }

        internal string GetSafeString(string valueName)
        {
            try
            {
                return (string)jsonObject[valueName];                
            }
            catch
            {
                return "";
            }
        }

        internal Uri GetSafeUri(string valueName)
        {
            try
            {
                return (Uri)jsonObject[valueName];  
            }
            catch
            {
                return null;
            }
        }

        internal bool GetSafeBoolean(string valueName, bool defaultValue = false)
        {
            try
            {
                return (bool)jsonObject[valueName];  
            }
            catch
            {
                return defaultValue;
            }
        }

        internal double GetSafeNumber(string valueName)
        {
            try
            {
                return (double)jsonObject[valueName];  
            }
            catch
            { 
                return 0.0;
            }
        }

        internal JSONObject GetSafeObject(string valueName)
        {
            try
            {                
                return new JSONObject((JObject)jsonObject[valueName]);                
            }
            catch
            {
                return null;
            }
        }

        internal T ProcessObject<T>(string valueName) where T : ITmdbObject, new()
        {
            try
            {
                JToken value = jsonObject[valueName];
                if (value != null && value.HasValues)
                {
                    T newT = new T();
                    newT.ProcessJson(new JSONObject((JObject)value));
                    return newT;
                }                        
                return default(T);
            }
            catch
            {
                return default(T);
            }
        }

        internal IReadOnlyList<T> ProcessArray<T>(string valueName) where T : ITmdbObject, new()
        {
            List<T> results = new List<T>();
            JToken value = jsonObject[valueName];
            if (value.HasValues)
            {
                if(value is JArray)
                foreach(JObject subObject in ((JArray)value))
                {
                    try
                    {
                        T newT = new T();
                        newT.ProcessJson(new JSONObject(subObject));
                        results.Add(newT);
                    }
                    catch
                    { }
                }
            }
            return results;
        }
        #endregion
    }
}
