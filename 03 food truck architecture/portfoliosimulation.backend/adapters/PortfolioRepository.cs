using System;
using System.Globalization;
using System.IO;
using System.Linq;
using portfoliosimulation.backend.messagepipelines.queries.candidatestocksquery;
using portfoliosimulation.contract;
using portfoliosimulation.contract.data;

namespace portfoliosimulation.backend.adapters
{
    public class PortfolioRepository : IPortfolioRepository
    {
        private readonly string _filepath;

        public PortfolioRepository() : this("portfolio.csv") {}
        public PortfolioRepository(string filepath) { _filepath = filepath; }


        public Portfolio Load() {
            if (File.Exists(_filepath) is false) return new Portfolio();
            
            var lines = File.ReadAllLines(_filepath);
            if (lines.Length == 0) return new Portfolio();

            var skipHeader = lines[0].StartsWith("Name") ? 1 : 0;
            var records = lines.Skip(skipHeader).Select(l => l.Split(';'));
            return new Portfolio{
                Entries = records.Select(MapToEntry).ToList()
            };
        }

        public void Store(Portfolio portfolio) {
            var lines = portfolio.Entries.Select(MapToLine);
            File.WriteAllLines(_filepath, lines);
        }


        static Portfolio.Stock MapToEntry(string[] record) {
            return new Portfolio.Stock {
                Name = record[0],
                Symbol = record[1],
                Currency = record[2],
                Bought = DateTime.Parse(record[3]),
                Qty = int.Parse(record[4]),
                BuyingPrice = decimal.Parse(record[5], NumberStyles.Any, CultureInfo.InvariantCulture),
                CurrentPrice = decimal.Parse(record[6], NumberStyles.Any, CultureInfo.InvariantCulture),
                LastUpdated = DateTime.Parse(record[7])
            };
        }

        static string MapToLine(Portfolio.Stock e)
            => $"{e.Name};{e.Symbol};{e.Currency};{e.Bought:yyyy-MM-dd};{e.Qty};{e.BuyingPrice.ToString(CultureInfo.InvariantCulture)};{e.CurrentPrice.ToString(CultureInfo.InvariantCulture)};{e.LastUpdated:yyyy-MM-dd}";
    }
}