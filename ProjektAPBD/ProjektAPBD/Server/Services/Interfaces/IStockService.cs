using ProjektAPBD.Shared.Models;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;

namespace ProjektAPBD.Server.Services.Interfaces
{
    public interface IStockService
    {
        Task<ResultDTO> ShowStosckInSearchBarAsync(string Text);
        Task<ResultDTO> GetStockAsync(string Ticker);
        Task<ResultDTO> GetWatchedStocksAsync(string Username);
        Task<ResultDTO> AddWatchStockAsync(string Username, string Ticker);
        Task<ResultDTO> DeleteStockFromWatchListAsync(string Username, string Ticker);
        Task<ResultDTO> AgregateBarsAsync(string Ticker);
        Task<ResultDTO> GetArticlesAsync(string Ticker);
    }
}
