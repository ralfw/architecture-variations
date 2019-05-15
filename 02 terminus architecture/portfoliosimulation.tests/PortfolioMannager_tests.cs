using portfoliosimulation.backend;
using portfoliosimulation.backend.domain;
using portfoliosimulation.contract.data.domain;
using Xunit;

namespace portfoliosimulation.tests
{
    public class PortfolioMannager_tests
    {
        [Fact]
        public void CalculateReturns()
        {
            var portfolio = new Portfolio();
            portfolio.Entries.Add(new Portfolio.Stock
            {
                Symbol = "S1",
                Qty = 5,
                BuyingPrice = 2.0m,
                CurrentPrice = 4.0m
            });
            portfolio.Entries.Add(new Portfolio.Stock
            {
                Symbol = "S2",
                Qty = 10,
                BuyingPrice = 11.0m,
                CurrentPrice = 16.0m
            });

            var result = PortfolioManager.CalculateReturns(portfolio);
            
            Assert.Equal(10.0m, result.Returns["S1"].Return);
            Assert.Equal(1.0m, result.Returns["S1"].RateOfReturn);
            
            Assert.Equal(50.0m, result.Returns["S2"].Return);
            Assert.Equal(0.45m, result.Returns["S2"].RateOfReturn, 2);
                        
            Assert.Equal(60.0m, result.TotalReturn);
            Assert.Equal(0.5m, result.TotalRateOfReturn);

        }
    }
}