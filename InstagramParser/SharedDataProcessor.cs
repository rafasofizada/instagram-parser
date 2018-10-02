using Newtonsoft.Json.Linq;

namespace InstagramParser
{
    public class SharedDataProcessor
    {
        private readonly JObject _json;



        public SharedDataProcessor(string sharedData)
        {
            _json = JObject.Parse(sharedData);
        }



        public JToken UserJson => _json["entry_data"]["ProfilePage"][0]["graphql"]["user"];

        public string RhxGis => (string) _json["rhx_gis"];
    }
}