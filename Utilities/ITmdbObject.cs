using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TmdbWrapper.Utilities
{
    internal interface ITmdbObject
    {
        void ProcessJson(JSONObject jsonObject);
    }
}
