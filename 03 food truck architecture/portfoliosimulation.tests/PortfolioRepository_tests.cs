using System;
using System.IO;
using portfoliosimulation.backend.adapters;
using portfoliosimulation.backend.messagepipelines.queries.candidatestocksquery;
using portfoliosimulation.contract.data;
using Xunit;

namespace portfoliosimulation.tests
{
    public class PortfolioRepository_tests
    {
        [Fact]
        public void Load()
        {
            var sut = new PortfolioRepository("testportfolio.csv");
            var result = sut.Load();
            Assert.Equal(3, result.Entries.Count);
            Assert.Equal("AAPL", result.Entries[0].Symbol);
            Assert.Equal(7, result.Entries[1].Qty);
            Assert.Equal(new DateTime(2018,9,15), result.Entries[2].Bought);
        }
        
        [Fact]
        public void Load_with_no_header()
        {
            var sut = new PortfolioRepository("testportfolio_no_header.csv");
            var result = sut.Load();
            Assert.Equal(3, result.Entries.Count);
            Assert.Equal("AAPL", result.Entries[0].Symbol);
            Assert.Equal(7, result.Entries[1].Qty);
            Assert.Equal(new DateTime(2018,9,15), result.Entries[2].Bought);
        }

        
        [Fact]
        public void Store()
        {
            File.Copy("testportfolio_no_header.csv", "store.csv", true);
            var sut = new PortfolioRepository("store.csv");
            var p = sut.Load();

            p.Entries[0].Qty = 100;
            p.Entries[1].Symbol = "XXX";
            p.Entries.Add(new Portfolio.Stock {
                Name = "Test",
                Symbol = "TTT",
                Currency = "USD",
                Bought = new DateTime(2010,1,2),
                Qty = 99,
                BuyingPrice = 100.0m,
                CurrentPrice = 111.0m,
                LastUpdated = new DateTime(2019,5,2)
            });
            sut.Store(p);

            var lines = File.ReadAllLines("store.csv");
            Assert.Equal(4, lines.Length);
            Assert.StartsWith("Apple", lines[0]);
            Assert.StartsWith("Test", lines[3]);
            Assert.EndsWith("2019-05-02", lines[3]);

            p = sut.Load();
            Assert.Equal(100,p.Entries[0].Qty);
            Assert.Equal("XXX", p.Entries[1].Symbol);
            Assert.Equal(111.0m, p.Entries[3].CurrentPrice);
        }
    }
}