using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;

namespace TmdbWrapper.Utilities
{
    internal class JsonObject
    {
        private readonly JToken jsonObject;

        internal JsonObject(JToken json)
        {
            jsonObject = json;
        }

        internal JsonObject(string json)
        {
            jsonObject = JObject.Parse(json);
        }

        #region jsonValue extensions

        internal IEnumerable<JsonObject> GetNamedArray(string name)
        {
            return from obj in (jsonObject[name] as JArray)
                   select new JsonObject(obj);
        }

        internal DateTime? GetSafeDateTime(string valueName)
        {
            try
            {
                return !DateTime.TryParse((string)jsonObject[valueName], CultureInfo.InvariantCulture
                                        , DateTimeStyles.None, out var dateTime)
                           ? (DateTime?)null
                           : dateTime;
            }
            catch
            {
                return null;
            }
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
                return jsonObject[valueName]?.Type != JTokenType.Null && !string.IsNullOrEmpty(jsonObject[valueName].Value<string>()) ? (Uri)jsonObject[valueName] : null;
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
                return jsonObject[valueName].Type != JTokenType.Null ? (double)jsonObject[valueName] : 0.0;
            }
            catch
            {
                return 0.0;
            }
        }

        internal int GetSafeInteger(string valueName)
        {
            try
            {
                var number = GetSafeNumber(valueName);
                var intNumber = (int)number;
                return intNumber;
            }
            catch
            {
                return 0;
            }
        }

        internal JsonObject GetSafeObject(string valueName)
        {
            try
            {
                return new JsonObject((JObject)jsonObject[valueName]);
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
                var value = jsonObject[valueName];
                if (value != null && value.HasValues)
                {
                    var newT = new T();
                    newT.ProcessJson(new JsonObject((JObject)value));
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
            var results = new List<T>();
            var value = jsonObject[valueName];
            if (value.HasValues)
            {
                if (value is JArray jArray)
                {
                    foreach (var jToken in jArray)
                    {
                        var subObject = (JObject)jToken;
                        try
                        {
                            var newT = new T();
                            newT.ProcessJson(new JsonObject(subObject));
                            results.Add(newT);
                        }
                        catch
                        {
                            //
                        }
                    }
                }
            }
            return results;
        }

        internal IReadOnlyList<int> ProcessIntArray(string valueName)
        {
            var results = new List<int>();
            var value = jsonObject[valueName];
            if (value.HasValues)
            {
                if (value is JArray jArray)
                {
                    foreach (var jToken in jArray)
                    {
                        var subObject = (JValue)jToken;
                        try
                        {
                            results.Add((int)subObject);
                        }
                        catch
                        { }
                    }
                }
            }
            return results;
        }

        internal IReadOnlyList<string> ProcessStringArray(string valueName)
        {
            var results = new List<string>();
            var value = jsonObject[valueName];
            if (value.HasValues)
            {
                if (value is JArray jArray)
                {
                    foreach (var jToken in jArray)
                    {
                        var subObject = (JValue)jToken;
                        try
                        {
                            results.Add((string)subObject);
                        }
                        catch
                        { }
                    }
                }
            }
            return results;
        }

        #endregion jsonValue extensions
    }
}