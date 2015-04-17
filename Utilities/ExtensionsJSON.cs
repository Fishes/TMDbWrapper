using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;


namespace TmdbWrapper.Utilities
{
    internal class JSONObject
    {
        private readonly JToken _jsonObject;

        internal JSONObject(JToken json)
        {
            _jsonObject = json;
        }

        internal JSONObject(string json) 
        {
            _jsonObject = JObject.Parse(json);
        }

        #region jsonValue extensions

        internal IEnumerable<JSONObject> GetNamedArray(string name)
        {
            return from obj in (_jsonObject[name] as JArray)
                   select new JSONObject(obj);
        }

        internal DateTime? GetSafeDateTime(string valueName)
        {
            try
            {
                DateTime dateTime;
                if(!DateTime.TryParse((string)_jsonObject[valueName], CultureInfo.InvariantCulture, DateTimeStyles.None, out dateTime )){
                    return null;
                }
                return dateTime;            
            }
            catch{
                return null;
            }
        }

        internal string GetSafeString(string valueName)
        {
            try
            {
                return (string)_jsonObject[valueName];                
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
                return (Uri)_jsonObject[valueName];  
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
                return (bool)_jsonObject[valueName];  
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
                return (double)_jsonObject[valueName];  
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
                return new JSONObject((JObject)_jsonObject[valueName]);                
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
                JToken value = _jsonObject[valueName];
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

        internal IReadOnlyList<T> ProcessObjectArray<T>(string valueName) where T : ITmdbObject, new()
        {
            List<T> results = new List<T>();
            JToken value = _jsonObject[valueName];
            if (value.HasValues)
            {
                JArray jArray = value as JArray;
                if(jArray != null)
                foreach(var jToken in jArray)
                {
                    var subObject = (JObject) jToken;
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

        internal IReadOnlyList<int> ProcessIntArray(string valueName) 
        {
            List<int> results = new List<int>();
            JToken value = _jsonObject[valueName];
            if (value.HasValues)
            {
                JArray jArray = value as JArray;
                if (jArray != null)
                    foreach (var jToken in jArray)
                    {
                        var subObject = (JValue) jToken;
                        try
                        {
                            results.Add((int)subObject);
                        }
                        catch
                        { }
                    }
            }
            return results;
        }

        internal IReadOnlyList<string> ProcessStringArray(string valueName)
        {
            List<string> results = new List<string>();
            JToken value = _jsonObject[valueName];
            if (value.HasValues)
            {
                JArray jArray = value as JArray;
                if (jArray != null)
                    foreach (var jToken in jArray)
                    {
                        var subObject = (JValue) jToken;
                        try
                        {
                            results.Add((string)subObject);
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
