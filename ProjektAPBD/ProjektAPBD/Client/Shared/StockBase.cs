using Microsoft.AspNetCore.Components;
using ProjektAPBD.Client.Services.Interfaces;
using ProjektAPBD.Shared.Models;
using System;
using System.Threading.Tasks;

namespace ProjektAPBD.Client.Shared
{
    public class StockBase : ComponentBase
    {
        [Parameter]
        public string Ticker { get; set; }
        [Inject]
        public IStockService _stockService { get; set; }
        protected Stock Stock;
        protected override async Task OnInitializedAsync()
        {
            try
            {
                Stock = await _stockService.GetStockAsync(Ticker);
            }
            catch (Exception e)
            {
            }
        }
    }
}
