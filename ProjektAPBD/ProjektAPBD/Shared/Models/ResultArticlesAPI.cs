using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace ProjektAPBD.Shared.Models
{
    public class ResultArticlesAPI
    {
        public List<ContentArticle> Results { get; set; }
        public string Status { get; set; }
        public string Request_Id { get; set; }
        public int Count { get; set; }
        public string Next_Url { get; set; }

        public class ContentArticle
        {
            [JsonPropertyName("title")]
            public string Title { get; set; }
            [JsonPropertyName("article_url")]
            public string AmpUrl { get; set; }
            [JsonPropertyName("description")]
            public string Description { get; set; }
            [JsonPropertyName("author")]
            public string Author { get; set; }
        }

    }
}
