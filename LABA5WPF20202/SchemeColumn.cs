using Newtonsoft.Json;

namespace LABA5WPF20202
{
    public class SchemeColumn
    {
        [JsonProperty("name")]
        public string Name { get; set; }

        [JsonProperty("type")]
        public string Type { get; set; }
    }
}