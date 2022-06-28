using ProjektAPBD.Client.Services.Interfaces;
using ProjektAPBD.Shared.Models;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Json;
using System.Threading.Tasks;

namespace ProjektAPBD.Client.Services.Implementations
{
    public class StockService : IStockService
    {

        private readonly HttpClient _httpClient;

        public StockService(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }



        public async Task<List<StockInSearchDTO>> ShowStosckInSearchBarAsync(string Text)
        {
            var response = await _httpClient.GetAsync($"stock/search/{Text}");
            if (response.IsSuccessStatusCode)
            {
                return await response.Content.ReadFromJsonAsync<List<StockInSearchDTO>>();
            }
            var message = await response.Content.ReadAsStringAsync();
            throw new Exception(message);
        }

        public async Task<Stock> GetStockAsync(string Ticker)
        {
            var response = await _httpClient.GetAsync($"stock/{Ticker}");
            if (response.IsSuccessStatusCode)
            {
                return await response.Content.ReadFromJsonAsync<Stock>();
            }
            var message = await response.Content.ReadAsStringAsync();
            throw new Exception(message);
        }

        public async Task<List<Stock>> GetWatchedStocksAsync(string Username)
        {
            var response = await _httpClient.GetAsync($"stock/watchedBy/{Username}");
            if (response.IsSuccessStatusCode)
            {
                return await response.Content.ReadFromJsonAsync<List<Stock>>();
            }
            var message = await response.Content.ReadAsStringAsync();
            throw new Exception(message);
        }

        public async Task<ResultDTO> AddWatchStockAsync(string Username, string Ticker)
        {
            var res = await _httpClient.GetAsync($"stock/add/{Username}/{Ticker}");
            return new ResultDTO()
            {
                Code = res.StatusCode,
                Message = await res.Content.ReadAsStringAsync()

            };
        }

        public async Task<ResultDTO> DeleteStockFromWatchListAsync(string Username, string Ticker)
        {
            var res = await _httpClient.GetAsync($"stock/delete/{Username}/{Ticker}");
            return new ResultDTO()
            {
                Code = res.StatusCode,
                Message = await res.Content.ReadAsStringAsync()

            };
        }

        public async Task<List<StockDataDTO>> AgregateBarsAsync(string Ticker)
        {
            var response = await _httpClient.GetAsync($"stock/bar/{Ticker}");
            if (response.IsSuccessStatusCode)
            {
                return await response.Content.ReadFromJsonAsync<List<StockDataDTO>>();
            }
            var message = await response.Content.ReadAsStringAsync();
            throw new Exception(message);
        }

        public async Task<List<StockArticleDTO>> GetArticlesAsync(string Ticker)
        {
            var response = await _httpClient.GetAsync($"stock/article/{Ticker}");
            if (response.IsSuccessStatusCode)
            {
                return await response.Content.ReadFromJsonAsync<List<StockArticleDTO>>();
            }
            var message = await response.Content.ReadAsStringAsync();
            throw new Exception(message);
        }
    }
}
