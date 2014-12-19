using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using TmdbWrapper.Utilities;

namespace TmdbWrapper.TV
{
    public class Network : ITmdbObject
    {
        public int Id { get; private set; }

        public string Name { get; private set; }

        public override string ToString()
        {
            return Name;
        }

        void ITmdbObject.ProcessJson(JSONObject jsonObject)
        {
            Id = (int)jsonObject.GetSafeNumber("id");
            Name = jsonObject.GetSafeString("name");
        }
    }
}
