using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace ProjektAPBD.Shared.Models
{
    [DataContract]
    public class StocksResultFromSearchJson
    {
        [DataMember]
        [JsonPropertyName("symbol")] 
        public string Ticker { get; set; }
        [DataMember]
        [JsonPropertyName("name")]
        public string Name { get; set; }
        [DataMember]
        [JsonPropertyName("exchangeShortName")]
        public string Market { get; set; }
    }
}
