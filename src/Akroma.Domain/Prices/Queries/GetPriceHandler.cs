using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using Akroma.Domain.Prices.Models;
using Brickweave.Cqrs;
using Newtonsoft.Json;

namespace Akroma.Domain.Prices.Queries
{
    public class GetPriceHandler : IQueryHandler<GetPrice, IEnumerable<Price>>
    {
        private static readonly HttpClient HttpClient = new HttpClient();

        public async Task<IEnumerable<Price>> HandleAsync(GetPrice query)
        {
            var response = await HttpClient.GetStringAsync(new Uri("https://stocks.exchange/api2/ticker"));
            var akroma = JsonConvert
                .DeserializeObject<List<StocksExchangePrice>>(response)
                .FirstOrDefault(x => x.MarketName == "AKA_BTC");

            return new List<Price>
            {
                new Price()
                {
                    Id = "akroma",
                    Name = "Akroma",
                    Symbol = "AKA",
                    Value = decimal.Parse(akroma.Ask)
                }
            };
        }
    }
}
