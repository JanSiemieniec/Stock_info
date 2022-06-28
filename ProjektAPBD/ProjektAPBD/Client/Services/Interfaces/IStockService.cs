using ProjektAPBD.Shared.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ProjektAPBD.Client.Services.Interfaces
{
    public interface IStockService
    {
        Task<List<StockInSearchDTO>> ShowStosckInSearchBarAsync(string Text);
        Task<Stock> GetStockAsync(string Ticker);
        Task<List<Stock>> GetWatchedStocksAsync(string Username);
        Task<ResultDTO> AddWatchStockAsync(string Username, string Ticker);
        Task<ResultDTO> DeleteStockFromWatchListAsync(string Username, string Ticker);
        Task<List<StockDataDTO>> AgregateBarsAsync(string Ticker);
        Task<List<StockArticleDTO>> GetArticlesAsync(string Ticker);
    }
}
