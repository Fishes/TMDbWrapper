using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using TmdbWrapper;
using Windows.Data.Json;

namespace TmdbWrapper.Utilities
{
    internal partial class JSONObject
    {
        private readonly JsonObject jsonObject;

        internal JSONObject(JsonObject json)
        {
            jsonObject = json;
        }

        internal JSONObject(string json)
        {
            jsonObject = JsonObject.Parse(json);
        }

        #region jsonValue extensions

        internal IEnumerable<JSONObject> GetNamedArray(string name)
        {
            var jsonObjects = from obj in jsonObject.GetNamedArray(name)
                              select new JSONObject(obj.GetObject());
            return jsonObjects.ToArray();
        }


        internal string GetSafeString(string valueName)
        {
            try
            {
                if (jsonObject.ContainsKey(valueName))
                {
                    JsonValue jsonValue = jsonObject.GetNamedValue(valueName);
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
                if (jsonObject.ContainsKey(valueName))
                {
                    JsonValue jsonValue = jsonObject.GetNamedValue(valueName);
                    if ((jsonValue != null) && (jsonValue.ValueType != JsonValueType.Null))
                    {
                        string value = jsonValue.GetString();
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
                if (jsonObject.ContainsKey(valueName))
                {
                    JsonValue jsonValue = jsonObject.GetNamedValue(valueName);
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
                if (jsonObject.ContainsKey(valueName))
                {
                    JsonValue jsonValue = jsonObject.GetNamedValue(valueName);
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
                if (jsonObject.ContainsKey(valueName))
                {
                    JsonValue jsonValue = jsonObject.GetNamedValue(valueName);
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
                if (jsonObject.ContainsKey(valueName))
                {
                    JsonValue jsonValue = jsonObject.GetNamedValue(valueName);
                    if ((jsonValue != null) && (jsonValue.ValueType == JsonValueType.Object))
                    {
                        T newT = new T();
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

        internal IReadOnlyList<T> ProcessArray<T>(string valueName) where T : ITmdbObject, new()
        {
            List<T> results = new List<T>();
            JsonValue jsonValue = jsonObject.GetNamedValue(valueName);
            if ((jsonValue != null) && (jsonValue.ValueType == JsonValueType.Array))
            {
                foreach (JsonValue subObject in jsonValue.GetArray())
                {
                    try
                    {
                        T newT = new T();
                        newT.ProcessJson(new JSONObject(subObject.GetObject()));
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
