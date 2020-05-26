using System;

namespace CoinsPriceServer.Domain
{
    public class Coin
    {
        public DateTime Date { get; set; }

        public decimal Price { get; set; }       

        public string CoinName { get; set; }

        public string Symbol { get; set; }

    }
}
