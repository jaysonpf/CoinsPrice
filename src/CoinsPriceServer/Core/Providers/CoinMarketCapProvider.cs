using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web;
using CoinsPriceServer.Domain;
using Newtonsoft.Json.Linq;

namespace CoinsPriceServer.Core.Providers
{
    public class CoinMarketCapProvider : ICoinMarketCapProvider
    {
        private static readonly string ApiKey = "ad7d9bca-8c29-4cd4-8432-1c0a4ddcb154";

        private static readonly string ApiUrl = "https://pro-api.coinmarketcap.com/v1/cryptocurrency/quotes/latest";
        private readonly HttpClient _httpClient;


        public CoinMarketCapProvider(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task<IEnumerable<Coin>> GetCryptoCoinsPrices()
        {
            var coinsIds = new[] { "1", "2", "1027", "512",};

            var url = new UriBuilder(ApiUrl);

            var queryString = HttpUtility.ParseQueryString(string.Empty);
            queryString["id"] = string.Join(',', coinsIds);

            url.Query = queryString.ToString();

            _httpClient.DefaultRequestHeaders.Add("X-CMC_PRO_API_KEY", ApiKey);
            _httpClient.DefaultRequestHeaders.Add("Accepts", "application/json");

            string responseJson;
            using (var response = await _httpClient.GetAsync(url.Uri))
            {
                responseJson = await response.Content.ReadAsStringAsync();
            }

            return ToSequenceOfCoins(responseJson, coinsIds);
        }

        private IEnumerable<Coin> ToSequenceOfCoins(string jsonString, string[] coinsIds)
        {
            return coinsIds.Select(id => ToSymbolPrice(jsonString, id))
                .Select(coin => coin.ToCoin())
                .ToList();
        }
        private  (string Name, string Symbol, decimal Price) ToSymbolPrice(string jsonString, string coinNumber)
        {
            var jObject = JObject.Parse(jsonString);
            var status = jObject["status"];

            var requestStatus = status["error_code"].Value<string>();
            if (requestStatus != "0") return (null, null, 0);

            var data = jObject["data"];

            var coin = data[coinNumber];

            var name = coin["name"].Value<string>();
            var symbol = coin["symbol"].Value<string>();

            var quote = coin["quote"]["USD"];
            var usdPrice = quote["price"].Value<decimal>();

            return (name, symbol, usdPrice);
        }
    }

    internal static class TupleExtensions
    {
        public static Coin ToCoin(this (string Name, string Symbol, decimal Price) tuple)
        {
            return new Coin
            {
                CoinName = tuple.Name,
                Symbol = tuple.Symbol,
                Price = tuple.Price,
                Date = DateTime.Now
            };
        }
    }
}
