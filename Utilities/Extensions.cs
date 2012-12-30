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
    internal static class Extensions
    {
        #region private constants
        private const string baseImageUri = @"http://cf2.imgobject.com/t/p/";
        #endregion

        #region string extensions
        internal static string EscapeString(this string s)
        {
            return Regex.Replace(s, "[" + Regex.Escape(new String(Path.GetInvalidFileNameChars())) + "]", "-");
        }
        #endregion

        #region jsonValue extensions
        internal static string GetSafeString(this JsonObject jsonObject, string valueName)
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

        internal static Uri GetSafeUri(this JsonObject jsonObject, string valueName)
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

        internal static bool GetSafeBoolean(this JsonObject jsonObject, string valueName, bool defaultValue = false)
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

        internal static double GetSafeNumber(this JsonObject jsonObject, string valueName)
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

        internal static JsonObject GetSafeObject(this JsonObject jsonObject, string valueName)
        {
            try
            {
                if (jsonObject.ContainsKey(valueName))
                {
                    JsonValue jsonValue = jsonObject.GetNamedValue(valueName);
                    if ((jsonValue != null) && (jsonValue.ValueType == JsonValueType.Object))
                    {
                        return jsonValue.GetObject();
                    }
                }
                return null;
            }
            catch
            {
                return null;
            }
        }

        internal static T ProcessObject<T>(this JsonObject jsonObject, string valueName) where T : ITmdbObject, new()
        {
            try
            {
                if (jsonObject.ContainsKey(valueName))
                {
                    JsonValue jsonValue = jsonObject.GetNamedValue(valueName);
                    if ((jsonValue != null) && (jsonValue.ValueType == JsonValueType.Object))
                    {
                        T newT = new T();
                        newT.ProcessJson(jsonValue.GetObject());
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

        internal static IReadOnlyList<T> ProcessArray<T>(this JsonObject jsonObject, string valueName) where T : ITmdbObject, new()
        {
            List<T> results = new List<T>();
            JsonValue jsonValue = jsonObject.GetNamedValue(valueName);
            if ((jsonValue != null) && (jsonValue.ValueType == JsonValueType.Array))
            {
                foreach(JsonValue subObject in jsonValue.GetArray())
                {
                    try
                    {
                        T newT = new T();
                        newT.ProcessJson(subObject.GetObject());
                        results.Add(newT);
                    }
                    catch
                    { }
                }
            }
            return results;
        }
        #endregion

        #region image uri methods
        internal static Uri MakeImageUri(string size, string path)
        {
            return new Uri(string.Format("{0}{1}{2}", baseImageUri, size, path));
        }
        #endregion
    }
}
