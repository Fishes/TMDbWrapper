using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.Data.Json;

namespace TmdbWrapper.Utilities
{
    internal interface ITmdbObject
    {
        void ProcessJson(JsonObject jsonObject);
    }
}
