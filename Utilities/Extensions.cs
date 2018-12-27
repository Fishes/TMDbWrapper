using System;
using System.IO;
using System.Text.RegularExpressions;

namespace TmdbWrapper.Utilities
{
    internal static class Extensions
    {
        #region private constants

        private static Uri _baseImageUri;

        #endregion private constants

        internal static void Initialize(Uri baseUri)
        {
            _baseImageUri = baseUri;
        }

        #region string extensions

        internal static string EscapeString(this string s)
        {
            return Regex.Replace(s, "[" + Regex.Escape(new string(Path.GetInvalidFileNameChars())) + "]", "-");
        }

        #endregion string extensions

        #region image uri methods

        internal static Uri MakeImageUri(string size, string path)
        {
            return new Uri($"{_baseImageUri}{size}{path}");
        }

        #endregion image uri methods
    }
}