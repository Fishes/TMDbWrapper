using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using Windows.Data.Json;

namespace TmdbWrapper.Utilities
{
    internal class JSONObject
    {
        private readonly JsonObject _jsonObject;

        internal JSONObject(JsonObject json)
        {
            _jsonObject = json;
        }

        internal JSONObject(string json)
        {
            _jsonObject = JsonObject.Parse(json);
        }

        #region jsonValue extensions

        internal IEnumerable<JSONObject> GetNamedArray(string name)
        {
            var jsonObjects = from obj in _jsonObject.GetNamedArray(name)
                              select new JSONObject(obj.GetObject());
            return jsonObjects.ToArray();
        }

        internal DateTime? GetSafeDateTime(string valueName)
        {
            try
            {
                if (_jsonObject.ContainsKey(valueName))
                {
                    var jsonValue = _jsonObject.GetNamedValue(valueName);
                    if ((jsonValue != null) && (jsonValue.ValueType != JsonValueType.Null))
                    {
                        DateTime dateTime;
                        if(DateTime.TryParse(jsonValue.GetString(), CultureInfo.InvariantCulture, DateTimeStyles.None, out dateTime )){
                            return dateTime;
                        }

                        return null;
                    }
                }
                return null;
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
                if (_jsonObject.ContainsKey(valueName))
                {
                    var jsonValue = _jsonObject.GetNamedValue(valueName);
                    if ((jsonValue != null) && (jsonValue.ValueType != JsonValueType.Null))
                    {
                        return jsonValue.GetString();
                    }
                }
                return "";
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
                if (_jsonObject.ContainsKey(valueName))
                {
                    var jsonValue = _jsonObject.GetNamedValue(valueName);
                    if ((jsonValue != null) && (jsonValue.ValueType != JsonValueType.Null))
                    {
                        var value = jsonValue.GetString();
                        if (!string.IsNullOrWhiteSpace(value))
                        {
                            return new Uri(value);
                        }
                    }
                }
                return null;
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
                if (_jsonObject.ContainsKey(valueName))
                {
                    var jsonValue = _jsonObject.GetNamedValue(valueName);
                    if ((jsonValue != null) && (jsonValue.ValueType != JsonValueType.Null))
                    {
                        return jsonValue.GetBoolean();
                    }
                }
                return defaultValue;
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
                if (_jsonObject.ContainsKey(valueName))
                {
                    var jsonValue = _jsonObject.GetNamedValue(valueName);
                    if ((jsonValue != null) && (jsonValue.ValueType != JsonValueType.Null))
                    {
                        return jsonValue.GetNumber();
                    }
                }
                return 0.0;
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
                if (_jsonObject.ContainsKey(valueName))
                {
                    var jsonValue = _jsonObject.GetNamedValue(valueName);
                    if ((jsonValue != null) && (jsonValue.ValueType == JsonValueType.Object))
                    {
                        return new JSONObject(jsonValue.GetObject());
                    }
                }
                return null;
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
                if (_jsonObject.ContainsKey(valueName))
                {
                    var jsonValue = _jsonObject.GetNamedValue(valueName);
                    if ((jsonValue != null) && (jsonValue.ValueType == JsonValueType.Object))
                    {
                        var newT = new T();
                        newT.ProcessJson(new JSONObject(jsonValue.GetObject()));
                        return newT;
                    }
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
            var jsonValue = _jsonObject.GetNamedValue(valueName);
            if ((jsonValue != null) && (jsonValue.ValueType == JsonValueType.Array))
            {
                foreach (var value in jsonValue.GetArray())
                {
                    var subObject = (JsonValue) value;
                    try
                    {
                        var newT = new T();
                        newT.ProcessJson(new JSONObject(subObject.GetObject()));
                        results.Add(newT);
                    }
                    catch
                    { }
                }
            }
            return results;
        }

        internal IReadOnlyList<string> ProcessStringArray(string valueName) 
        {
            var results = new List<string>();
            var jsonValue = _jsonObject.GetNamedValue(valueName);
            if ((jsonValue != null) && (jsonValue.ValueType == JsonValueType.Array))
            {
                foreach (var subObject in jsonValue.GetArray())
                {
                    try
                    {
                        results.Add(subObject.GetString());
                    }
                    catch
                    { }
                }
            }
            return results;
        }

        internal IReadOnlyList<int> ProcessIntArray(string valueName)
        {
            var results = new List<int>();
            var jsonValue = _jsonObject.GetNamedValue(valueName);
            if ((jsonValue != null) && (jsonValue.ValueType == JsonValueType.Array))
            {
                foreach (var subObject in jsonValue.GetArray())
                {
                    try
                    {
                        results.Add((int)subObject.GetNumber());
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
