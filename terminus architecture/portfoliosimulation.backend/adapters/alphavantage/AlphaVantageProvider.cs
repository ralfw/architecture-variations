using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Reflection;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace portfoliosimulation.backend.adapters.alphavantage
{
    public class AlphaVantageProvider : IAlphaVantageProvider
    {
        class Secrets  {
#pragma warning disable 649
            public string APIKey;
#pragma warning restore 649
        }
        
        private readonly string _baseUrl;
        
        public AlphaVantageProvider()
        {
            var secretsFilename = Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location) + "/adapters/alphavantage/alphavantage.secrets";
            var secrets = JsonConvert.DeserializeObject<Secrets>(File.ReadAllText(secretsFilename));
            _baseUrl = $"https://www.alphavantage.co/query?apikey={secrets.APIKey}";
        }

        
        public StockPrice GetQuote(string symbol) {
            var endpointUrl = _baseUrl + $"&function=GLOBAL_QUOTE&symbol={symbol}";
            var quoteText = new WebClient().DownloadString(endpointUrl);
            var quote = JsonConvert.DeserializeObject<Dictionary<string, object>>(quoteText);
            var quoteObj = quote["Global Quote"] as JObject;
            return new StockPrice(
                quoteObj["01. symbol"].ToString(),
                quoteObj["05. price"].Value<double>());
        }

        
        public ExchangeRateToEuro GetConversionRateToEuro(string fromCurrencyAbbreviation) {
            var endpointUrl = _baseUrl + $"&function=CURRENCY_EXCHANGE_RATE&from_currency={fromCurrencyAbbreviation}&to_currency=EUR";
            var exchangeRateText = new WebClient().DownloadString(endpointUrl);
            var exchangeRate = JsonConvert.DeserializeObject<Dictionary<string, object>>(exchangeRateText);
            var exchangeRateObj = exchangeRate["Realtime Currency Exchange Rate"] as JObject;
            return new ExchangeRateToEuro(
                fromCurrencyAbbreviation,
                exchangeRateObj["5. Exchange Rate"].Value<double>());
        }


        public StockInfo[] FindMatchingStocks(string pattern)  {
            var endpointUrl = _baseUrl + $"&function=SYMBOL_SEARCH&keywords={pattern}";
            var queryResultText = new WebClient().DownloadString(endpointUrl);
            var queryResult = JsonConvert.DeserializeObject<Dictionary<string, object>>(queryResultText);
            var queryResultObj = queryResult["bestMatches"] as JArray;

            var stockInfos = new List<StockInfo>();
            foreach (var item in queryResultObj.Children()) {
                var itemProperties = item.Children<JProperty>();
                var info = new StockInfo(
                    itemProperties.First(x => x.Name == "1. symbol").Value.Value<string>(),
                    itemProperties.First(x => x.Name == "2. name").Value.Value<string>(),
                    itemProperties.First(x => x.Name == "4. region").Value.Value<string>(),
                    itemProperties.First(x => x.Name == "8. currency").Value.Value<string>()
                );
                stockInfos.Add(info);
            }
            return stockInfos.ToArray();
        }
    }
}