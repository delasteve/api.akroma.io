using Newtonsoft.Json;

namespace Akroma.Domain.Prices.Models
{
    public class StocksExchangePrice
    {
        [JsonProperty(PropertyName = "min_order_number")]
        public string MinOrderAmount { get; set; }

        [JsonProperty(PropertyName = "ask")]
        public string Ask { get; set; }

        [JsonProperty(PropertyName = "bid")]
        public string Bid { get; set; }

        [JsonProperty(PropertyName = "last")]
        public string Last { get; set; }

        [JsonProperty(PropertyName = "lastDayAgo")]
        public string LastDayAgo { get; set; }

        [JsonProperty(PropertyName = "vol")]
        public string Volume { get; set; }

        [JsonProperty(PropertyName = "spread")]
        public string Spread { get; set; }

        [JsonProperty(PropertyName = "buy_fee_percent")]
        public string BuyFeePercent { get; set; }

        [JsonProperty(PropertyName = "sell_fee_percent")]
        public string SellFeePercent { get; set; }

        [JsonProperty(PropertyName = "market_name")]
        public string MarketName { get; set; }

        [JsonProperty(PropertyName = "updated_time")]
        public string UpdatedTime { get; set; }

        [JsonProperty(PropertyName = "server_time")]
        public string ServerTime { get; set; }
    }
}
