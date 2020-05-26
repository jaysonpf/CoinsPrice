using System;
using System.Linq;
using System.Threading.Tasks;
using CoinsPriceServer.Core.Providers;
using CoinsPriceServer.Domain;

namespace CoinsPriceServer.Core.Service
{
    public class CoinPriceService : ICoinPriceService
    {
        private readonly ICoinMarketCapProvider _coinMarketCapProvider;

        public CoinPriceService(ICoinMarketCapProvider coinMarketCapProvider)
        {
            _coinMarketCapProvider = coinMarketCapProvider;
        }

        public async Task<Coin[]> GetPriceAsync()
        {
            var result = await _coinMarketCapProvider.GetCryptoCoinsPrices();

            return result.ToArray();

        }
    }
}
