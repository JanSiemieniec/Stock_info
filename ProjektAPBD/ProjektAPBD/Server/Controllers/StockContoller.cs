using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using ProjektAPBD.Server.Services.Interfaces;
using ProjektAPBD.Shared.Models;
using System.Collections.Generic;
using System.Net;
using System.Threading.Tasks;

namespace ProjektAPBD.Server.Controllers
{
    [Authorize]
    [Route("stock")]
    [ApiController]
    public class StockContoller : ControllerBase
    {
        private readonly IStockService _stockService;
        public StockContoller(IStockService stockService)
        {
            _stockService = stockService;
        }

        [HttpGet]
        [Route("search/{searchText}")]
        public async Task<IActionResult> GetStockSearchResult(string searchText)
        {
            var response = await _stockService.ShowStosckInSearchBarAsync(searchText);

            if (response.Code !=HttpStatusCode.OK)
            {
                return StatusCode((int)response.Code, response.Message);
            }

            return Ok(response.Stocks);
        }
        [HttpGet]
        [Route("{Ticker}")]
        public async Task<IActionResult> GetStock(string Ticker)
        {
            var response = await _stockService.GetStockAsync(Ticker);

            if (response.Code != HttpStatusCode.OK)
            {
                return StatusCode((int)response.Code, response.Message);
            }

            return Ok(response.Stock);
        }

        [HttpGet]
        [Route("watchedBy/{Username}")]
        public async Task<IActionResult> GetWatchedStocks(string Username)
        {
            var response = await _stockService.GetWatchedStocksAsync(Username);

            if (response.Code != HttpStatusCode.OK)
            {
                return StatusCode((int)response.Code, response.Message);
            }

            return Ok(response.StockInWatchList);
        }
        [HttpGet]
        [Route("add/{Username}/{Ticker}")]
        public async Task<IActionResult> AddWatchStock(string Username, string Ticker)
        {
            var response = await _stockService.AddWatchStockAsync(Username, Ticker);
                return StatusCode((int)response.Code, response.Message);
        }
        [HttpGet]
        [Route("delete/{Username}/{Ticker}")]
        public async Task<IActionResult> DeleteStockFromWatchList(string Username, string Ticker)
        {
            var response = await _stockService.DeleteStockFromWatchListAsync(Username, Ticker);
            return StatusCode((int)response.Code, response.Message);
        }

        [HttpGet]
        [Route("bar/{Ticker}")]
        public async Task<IActionResult> AgregateBars(string Ticker)
        {
            var response = await _stockService.AgregateBarsAsync(Ticker);

            if (response.Code != HttpStatusCode.OK)
            {
                return StatusCode((int)response.Code, response.Message);
            }

            return Ok(response.AggregatedBarrs);
        }
        [HttpGet]
        [Route("article/{Ticker}")]
        public async Task<IActionResult> GetArticles(string Ticker)
        {
            var response = await _stockService.GetArticlesAsync(Ticker);

            if (response.Code != HttpStatusCode.OK)
            {
                return StatusCode((int)response.Code, response.Message);
            }

            return Ok(response.Articles);
        }
    }
}
