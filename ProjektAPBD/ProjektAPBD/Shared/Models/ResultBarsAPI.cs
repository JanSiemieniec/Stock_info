using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace ProjektAPBD.Shared.Models
{
    public class ResultBarsAPI
    {
        public string Ticker { get; set; }
        public int QueryCount { get; set; }
        public bool Adjusted { get; set; }
        public List<ContentBars> Results { get; set; }
        public string Status { get; set; }
        public string Request_Id { get; set; }
        public int Count { get; set; }

        public class ContentBars
        {
            [JsonPropertyName("o")]
            public double Open { get; set; }
            [JsonPropertyName("l")]
            public double Low { get; set; }
            [JsonPropertyName("c")]
            public double Close { get; set; }
            [JsonPropertyName("h")]
            public double High { get; set; }
            [JsonPropertyName("v")]
            public double Volume { get; set; }
        }
    }
}
