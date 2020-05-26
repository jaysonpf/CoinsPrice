using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CoinsPriceServer.Core.Service;
using CoinsPriceServer.Domain;
using Microsoft.AspNetCore.Components;

namespace CoinsPriceServer.Pages
{
    public class CoinsPriceBase : ComponentBase
    {
        [Inject]
        protected ICoinPriceService CoinPriceService { get; set; }

        public Coin[] CoinsPrice;


        protected override async Task OnInitializedAsync()
        {
            CoinsPrice = await CoinPriceService.GetPriceAsync();
        }
    }
}
