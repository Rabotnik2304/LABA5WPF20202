using Newtonsoft.Json;
using System.Collections.Generic;
using System.IO;

namespace LABA5WPF20202
{
    public class Scheme
    {
        [JsonProperty("name")]
        public string Name { get; set; }

        [JsonProperty("columns")]
        public List<SchemeColumn> Columns { get; set; }
        
        public static Scheme ReadJson(string path)
        {
            return JsonConvert.DeserializeObject<Scheme>(File.ReadAllText(path));
        }
    }  
        
}