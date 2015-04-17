using System;
using System.Collections.Generic;
using TmdbWrapper.Utilities;

namespace TmdbWrapper.Configuration
{
    internal class ImageConfiguration : ITmdbObject
    {
        internal Uri BaseUrl { get; private set; }

        internal Uri SecureBaseUrl { get; private set; }

        internal IReadOnlyList<string> BackdropSizes { get; private set; }

        internal IReadOnlyList<string> LogoSizes { get; private set; }

        internal IReadOnlyList<string> PosterSizes { get; private set; }

        internal IReadOnlyList<string> ProfileSizes { get; private set; }

        internal IReadOnlyList<string> StillSizes { get; private set; }

        public void ProcessJson(JSONObject jsonObject)
        {
            BaseUrl = jsonObject.GetSafeUri("base_url");
            SecureBaseUrl = jsonObject.GetSafeUri("secure_base_url");
            BackdropSizes = jsonObject.ProcessStringArray("backdrop_sizes");
            LogoSizes = jsonObject.ProcessStringArray("logo_sizes");
            PosterSizes = jsonObject.ProcessStringArray("poster_sizes");
            ProfileSizes = jsonObject.ProcessStringArray("profile_sizes");
            StillSizes = jsonObject.ProcessStringArray("still_sizes");
        }
    }
}
