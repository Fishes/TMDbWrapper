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
        private const string baseImageUri = @"http://cf2.imgobject.com/t/p/";

        internal static string EscapeString(this string s)
        {
            return Regex.Replace(s, "[" + Regex.Escape(new String(Path.GetInvalidFileNameChars())) + "]", "-");
        }

        internal static string GetSafeString(this JsonValue jsonValue)
        {
            if (jsonValue.ValueType != JsonValueType.Null)
            {
                return jsonValue.GetString();
            }
            return "";
        }

        internal static bool GetSafeBoolean(this JsonValue jsonValue, bool defaultValue = false)
        {
            if (jsonValue.ValueType != JsonValueType.Null)
            {
                return jsonValue.GetBoolean();
            }
            return defaultValue;
        }

        internal static double GetSafeNumber(this JsonValue jsonValue)
        {
            if (jsonValue.ValueType != JsonValueType.Null)
            {
                return jsonValue.GetNumber();
            }
            return 0.0;
        }

        internal static JsonObject GetSafeObject(this JsonValue jsonValue)
        {
            if (jsonValue.ValueType != JsonValueType.Null)
            {
                return jsonValue.GetObject();
            }
            return null;
        }

        internal static T ProcessObject<T>(this JsonValue jsonValue) where T : ITmdbObject, new()
        {
            if (jsonValue.ValueType == JsonValueType.Object)
            {
                T newT = new T();
                newT.ProcessJson(jsonValue.GetObject());
                return newT;
            }
            return default(T);
        }

        internal static IList<T> ProcessArray<T>(this JsonValue jsonValue) where T : ITmdbObject, new()
        {
            IList<T> results = new List<T>();
            if (jsonValue.ValueType == JsonValueType.Array)
            {
                foreach(JsonObject subObject in jsonValue.GetArray())
                {
                    T newT = new T();
                    newT.ProcessJson(subObject);
                    results.Add(newT);
                }
            }
            return results;
        }

        internal static Uri MakeImageUri(string size, string path)
        {
            return new Uri(string.Format("{0}{1}{2}", baseImageUri, size, path));
        }
    }
}
