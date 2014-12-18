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
    internal static partial class Extensions
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

        #region image uri methods
        internal static Uri MakeImageUri(string size, string path)
        {
            return new Uri(string.Format("{0}{1}{2}", baseImageUri, size, path));
        }
        #endregion
    }
}
