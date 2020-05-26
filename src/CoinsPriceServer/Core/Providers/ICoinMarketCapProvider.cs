using System.Collections.Generic;
using System.Threading.Tasks;
using CoinsPriceServer.Domain;

namespace CoinsPriceServer.Core.Providers
{
    public interface ICoinMarketCapProvider
    {
        Task<IEnumerable<Coin>> GetCryptoCoinsPrices();
    }
}