using System.Collections.Generic;
using TmdbWrapper.Utilities;

namespace TmdbWrapper.Configuration
{
    internal class Configuration : ITmdbObject
    {
        internal ImageConfiguration ImageConfiguration { get; set; }

        internal IReadOnlyList<string> ChangeKeys { get; set; }

        public void ProcessJson(JSONObject jsonObject)
        {
            ImageConfiguration = jsonObject.ProcessObject<ImageConfiguration>("images");
            ChangeKeys = jsonObject.ProcessStringArray("change_keys");
        }
    }
}
