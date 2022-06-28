using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace ProjektAPBD.Shared.Models
{
    public class ResultDTO
    {
        public HttpStatusCode Code { get; set; }
        public string Message { get; set; }
        public List<StockInSearchDTO> Stocks { get; set; }
        public List<Stock> StockInWatchList { get; set; }
        public Stock Stock { get; set; }
        public List<StockDataDTO> AggregatedBarrs { get; set; }
        public List<StockArticleDTO> Articles { get; set; }
    }
}
