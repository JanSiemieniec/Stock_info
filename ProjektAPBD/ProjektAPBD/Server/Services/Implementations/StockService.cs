using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;
using ProjektAPBD.Server.Data;
using ProjektAPBD.Server.Models;
using ProjektAPBD.Server.Services.Interfaces;
using ProjektAPBD.Shared.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Http.Json;
using System.Text.Json;
using System.Threading.Tasks;

namespace ProjektAPBD.Server.Services.Implementations
{
    public class StockService : IStockService
    {
        private readonly ApplicationDbContext _context;
        private readonly IConfiguration _configuration;
        private readonly HttpClient _httpClient;


        public StockService(ApplicationDbContext context, IConfiguration configuration, HttpClient httpClient)
        {
            _context = context;
            _configuration = configuration;
            _httpClient = httpClient;
        }


        public async Task<ResultDTO> ShowStosckInSearchBarAsync(string Text)
        {
            var url =
                $"https://financialmodelingprep.com/api/v3/search?query={Text}&limit=10&exchange=NASDAQ,NSE,NYSE,FOREX&apikey={_configuration["FinancialModelingPrepApiKey"]}";
            var response = await _httpClient.GetAsync(url);
            if (response.IsSuccessStatusCode)
            {
                var stockSearchResult = await response.Content.ReadFromJsonAsync<List<StocksResultFromSearchJson>>();
                var stocksFoundToReturn = new List<StockInSearchDTO>();

                stockSearchResult.ForEach(stock => stocksFoundToReturn.Add(new StockInSearchDTO()
                {
                    Ticker = stock.Ticker,
                    Name = stock.Name,
                    Market = stock.Market
                }));

                return new ResultDTO()
                {
                    Code = HttpStatusCode.OK,
                    Stocks = stocksFoundToReturn
                };
            }

            var message = await response.Content.ReadAsStringAsync();

            return new ResultDTO()
            {
                Code = (HttpStatusCode)500,
                Message = message
            };
        }

        public async Task<ResultDTO> GetStockAsync(string Ticker)
        {
            Ticker = Ticker.ToUpper();
            StockTab stockFromDb = _context.Stock.SingleOrDefault(x => x.Ticker.Equals(Ticker));
            if (stockFromDb != null)
            {
                return new ResultDTO()
                {
                    Code = HttpStatusCode.OK,
                    Stock = new Stock()
                    {
                        Ticker = stockFromDb.Ticker,
                        Name = stockFromDb.Name,
                        Description = stockFromDb.Description,
                        Homepage = stockFromDb.Homepage,
                        Industry = stockFromDb.Industry,
                        Country = stockFromDb.Country,
                        Icon = stockFromDb.Logo
                    }
                };
            }
            var url =
                $"https://api.polygon.io/v3/reference/tickers/{Ticker}?date={DateTime.Now.ToString("yyyy-MM-dd")}&apiKey={_configuration["PolygonApiKey"]}";
            var response = await _httpClient.GetAsync(url);
            if (response.IsSuccessStatusCode)
            {
                var resultGet = await response.Content.ReadFromJsonAsync<ResultAPI>();
                StockTab stock = new StockTab()
                {
                    Ticker = resultGet.Results.Ticker,
                    Name = resultGet.Results.Name,
                    Description = resultGet.Results.Description,
                    Homepage = resultGet.Results.Homepage,
                    Industry = resultGet.Results.Industry,
                    Country = resultGet.Results.Country,
                    Logo = $"{resultGet.Results.Branding.Icon}?apiKey={_configuration["PolygonApiKey"]}"

                };
                await _context.Stock.AddAsync(stock);
                await _context.SaveChangesAsync();
                return new ResultDTO()
                {
                    Code = HttpStatusCode.OK,
                    Stock = new Stock()
                    {
                        Ticker = resultGet.Results.Ticker,
                        Name = resultGet.Results.Name,
                        Description = resultGet.Results.Description,
                        Homepage = resultGet.Results.Homepage,
                        Industry = resultGet.Results.Industry,
                        Country = resultGet.Results.Country,
                        Icon = $"{resultGet.Results.Branding.Icon}?apiKey={_configuration["PolygonApiKey"]}"

                    }
                };
            }

            var message = await response.Content.ReadAsStringAsync();

            return new ResultDTO()
            {
                Code = (HttpStatusCode)500,
                Message = message
            };
        }

        public async Task<ResultDTO> GetWatchedStocksAsync(string Username)
        {
            List<int> IdList = _context.WatchList.Where(y => y.Id.Equals(Username)).Select(x => x.IdStock).ToList();

            List<Stock> stocks = new List<Stock>();
            foreach (int ele in IdList)
            {
                StockTab stab = _context.Stock.Where(x => x.IdStock == ele).FirstOrDefault();
                if (stab == null)
                {
                    return new ResultDTO
                    {
                        Code = (HttpStatusCode)500
                    };
                }
                stocks.Add(new Stock()
                {
                    Ticker = stab.Ticker,
                    Name = stab.Name,
                    Description = stab.Description,
                    Homepage = stab.Homepage,
                    Industry = stab.Industry,
                    Country = stab.Country,
                    Icon = stab.Logo

                });
            }
            return new ResultDTO()
            {
                Code = HttpStatusCode.OK,
                StockInWatchList = stocks
            };
        }

        public async Task<ResultDTO> AddWatchStockAsync(string Username, string Ticker)
        {
            int IdTicker = _context.Stock.Where(x => x.Ticker.Equals(Ticker)).FirstOrDefault().IdStock;
            if (_context.WatchList.Where(x => x.Id.Equals(Username) && x.IdStock == IdTicker).Count() > 0)
            {
                return new ResultDTO()
                {
                    Code = HttpStatusCode.InternalServerError,
                    Message = "This record exsists in db"
                };
            }
            WatchList StockToAdd = new WatchList()
            {
                Id = Username,
                IdStock = IdTicker
            };
            await _context.WatchList.AddAsync(StockToAdd);
            await _context.SaveChangesAsync();
            return new ResultDTO()
            {
                Code = HttpStatusCode.OK,
                Message = "Added to Watch list"
            };

        }

        public async Task<ResultDTO> DeleteStockFromWatchListAsync(string Username, string Ticker)
        {
            int IdTicker = _context.Stock.Where(x => x.Ticker.Equals(Ticker)).FirstOrDefault().IdStock;
            WatchList StockToDelete = _context.WatchList.Where(x => x.Id.Equals(Username) && x.IdStock == IdTicker).FirstOrDefault();
            if (StockToDelete == null)
            {
                return new ResultDTO()
                {
                    Code = HttpStatusCode.InternalServerError,
                    Message = "This record exsists in db"
                };
            }
            _context.Remove(StockToDelete);
            await _context.SaveChangesAsync();
            return new ResultDTO()
            {
                Code = HttpStatusCode.OK,
                Message = "Deleted from Watch list"
            };
        }

        public async Task<ResultDTO> AgregateBarsAsync(string Ticker)
        {
            string DateTimeNow = DateTime.Now.ToString("yyyy-MM-dd");
            string DateTimeAgo = DateTime.Now.AddDays(-360).ToString("yyyy-MM-dd");
            var Date = DateTime.Now.AddDays(-360);
            var url =
                $"https://api.polygon.io/v2/aggs/ticker/{Ticker}/range/1/day/{DateTimeAgo}/{DateTimeNow}?adjusted=true&sort=asc&limit=360&apiKey={_configuration["PolygonApiKey"]}";
            var response = await _httpClient.GetAsync(url);
            List<StockDataDTO> Bars = new List<StockDataDTO>();
            if (response.IsSuccessStatusCode)
            {
                var resultGet = await response.Content.ReadFromJsonAsync<ResultBarsAPI>();
                for (int i = 0; i < resultGet.Results.Count; i++)
                {
                    var res = resultGet.Results[i];
                    Bars.Add(new StockDataDTO
                    {
                        Date = Date,
                        Open = res.Open,
                        Low = res.Low,
                        Close = res.Close,
                        High = res.High,
                        Volume = res.Volume
                    });
                    Date = Date.AddDays(1);
                }
                return new ResultDTO()
                {
                    Code = HttpStatusCode.OK,
                    AggregatedBarrs = Bars
                };
            }

            var message = await response.Content.ReadAsStringAsync();

            return new ResultDTO()
            {
                Code = (HttpStatusCode)500,
                Message = message
            };
        }

        public async Task<ResultDTO> GetArticlesAsync(string Ticker)
        {
            var url =
                $"https://api.polygon.io/v2/reference/news?ticker={Ticker}&limit=5&apiKey={_configuration["PolygonApiKey"]}";
            var response = await _httpClient.GetAsync(url);
            List<StockArticleDTO> Articles = new List<StockArticleDTO>();
            if (response.IsSuccessStatusCode)
            {
                var resultGet = await response.Content.ReadFromJsonAsync<ResultArticlesAPI>();
                for (int i = 0; i < resultGet.Results.Count; i++)
                {
                    var res = resultGet.Results[i];
                    Articles.Add(new StockArticleDTO
                    {
                        Title = res.Title,
                        AmpUrl = res.AmpUrl,
                        Description = res.Description,
                        Author = res.Author
                    });
                }
                return new ResultDTO()
                {
                    Code = HttpStatusCode.OK,
                    Articles = Articles
                };
            }

            var message = await response.Content.ReadAsStringAsync();

            return new ResultDTO()
            {
                Code = (HttpStatusCode)500,
                Message = message
            };
        }
    }
}

