using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace ProjektAPBD.Shared.Models
{
    [DataContract]
    public class StockJson
    {
        [DataMember]
        [JsonPropertyName("ticker")]
        public string Ticker { get; set; }
        [DataMember]
        [JsonPropertyName("name")]
        public string Name { get; set; }
        [DataMember]
        [JsonPropertyName("description")]
        public string Description { get; set; }
        [DataMember]
        [JsonPropertyName("symbol")]
        public string Homepage { get; set; }
        [DataMember]
        [JsonPropertyName("homepage_url")]
        public string Industry { get; set; }
        [DataMember]
        [JsonPropertyName("locale")]
        public string Country { get; set; }



    }
}
