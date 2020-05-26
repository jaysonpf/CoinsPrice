using System.Threading.Tasks;
using CoinsPriceServer.Domain;

namespace CoinsPriceServer.Core.Service
{
    public interface ICoinPriceService
    {
        Task<Coin[]> GetPriceAsync();
    }
}