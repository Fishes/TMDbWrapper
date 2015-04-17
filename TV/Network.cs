using TmdbWrapper.Utilities;

namespace TmdbWrapper.TV
{
    /// <summary>
    /// A broadcasting network the broadcasted the series
    /// </summary>
    public class Network : ITmdbObject
    {
        /// <summary>
        /// The id of the network
        /// </summary>
        public int Id { get; private set; }
        /// <summary>
        /// The name of the network
        /// </summary>
        public string Name { get; private set; }
        /// <summary>
        /// Returns the string representation
        /// </summary>
        /// <returns>Name of the Network</returns>
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
