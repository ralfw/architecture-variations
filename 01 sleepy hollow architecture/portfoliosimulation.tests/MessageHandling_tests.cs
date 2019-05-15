using System;
using System.Diagnostics;
using System.IO;
using portfoliosimulation.backend;
using portfoliosimulation.backend.adapters;
using portfoliosimulation.contract;
using portfoliosimulation.contract.data.messages.commands;
using portfoliosimulation.contract.data.messages.queries;
using Xunit;
using Xunit.Abstractions;

namespace portfoliosimulation.tests
{
    public class MessageHandling_tests
    {
        private readonly ITestOutputHelper _output;

        public MessageHandling_tests(ITestOutputHelper output) {
            _output = output;
        }
        
        
        [Fact]
        public void PortfolioQuery()
        {
            var repo = new PortfolioRepository("smallportfolio.csv");
            var sut = new MessageHandling(repo, null);

            var result = sut.Handle(new PortfolioQuery());
            
            Assert.Equal(180.0m, result.PortfolioValue);
            Assert.Equal(0.5m, result.PortfolioRateOfReturn);
            Assert.Equal(20.0m, result.Stocks[0].CurrentValue);
            Assert.Equal(0.45m, result.Stocks[1].RateOfReturn, 2);
        }


        [Fact(Skip = "Needs online access")]
        public void UpdatePortfolio()
        {
            File.Copy("portfoliotoupdate.csv", "updateportfolio.csv", true);
            
            var repo = new PortfolioRepository("updateportfolio.csv");
            var before = repo.Load();
            
            var ex = new StockExchangeProvider();
            var sut = new MessageHandling(repo, ex);

            sut.Handle(new UpdatePortfolioCommand());

            var after = repo.Load();
            for (var i = 0; i < before.Entries.Count; i++) {
                Assert.True(after.Entries[i].CurrentPrice > 0.0m);
                Assert.True(before.Entries[i].CurrentPrice != after.Entries[i].CurrentPrice);
                Assert.True(before.Entries[i].LastUpdated < after.Entries[i].LastUpdated);
            }
        }


        [Fact(Skip = "Requires access to online service")]
        public void FindCandidates()
        {
            var ex = new StockExchangeProvider();
            var sut = new MessageHandling(null, ex);

            var result = sut.Handle(new CandidateStocksQuery {Pattern = "Apple"});
            
            Assert.True(result.Candidates.Length > 0);
            
            _output.WriteLine($"Candidates found: {result.Candidates.Length}");
            foreach(var r in result.Candidates)
                _output.WriteLine($"{r.Name} ({r.Symbol}): {r.Price} {r.Currency}");
        }
    }
}