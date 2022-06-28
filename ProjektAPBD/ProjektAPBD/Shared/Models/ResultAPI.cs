using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace ProjektAPBD.Shared.Models
{
    public class ResultAPI
    {
        public Content Results { get; set; }
        public string Status { get; set; }
        public string Error { get; set; }
    }

    public class Branding
    {
        [JsonPropertyName("logo_url")] 
        public string? Logo { get; set; }
        [JsonPropertyName("icon_url")] 
        public string? Icon { get; set; }
    }

    public class Content
    {
        [JsonPropertyName("ticker")]
        public string Ticker { get; set; }
        [JsonPropertyName("name")]
        public string Name { get; set; }
        [JsonPropertyName("description")]
        public string Description { get; set; }
        [JsonPropertyName("homepage_url")] 
        public string Homepage { get; set; }
        [JsonPropertyName("sic_description")]
        public string Industry { get; set; }
        [JsonPropertyName("locale")]
        public string Country { get; set; }
        public Branding? Branding { get; set; }
    }
}
