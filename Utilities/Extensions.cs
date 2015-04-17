using System;
using System.IO;
using System.Text.RegularExpressions;

namespace TmdbWrapper.Utilities
{
    internal static class Extensions
    {
        #region private constants
        private static Uri _baseImageUri;
        #endregion

        internal static void Initialize(Uri baseUri)
        {
            _baseImageUri = baseUri;
        }

        #region string extensions
        internal static string EscapeString(this string s)
        {
            return Regex.Replace(s, "[" + Regex.Escape(new String(Path.GetInvalidFileNameChars())) + "]", "-");
        }
        #endregion

        #region image uri methods
        internal static Uri MakeImageUri(string size, string path)
        {
            return new Uri(string.Format("{0}{1}{2}", _baseImageUri, size, path));
        }
        #endregion
    }
}
