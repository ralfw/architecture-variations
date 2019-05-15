using portfoliosimulation.backend.adapters;
using Xunit;

namespace portfoliosimulation.tests
{
    public class StockExchangeProvider_tests
    {
        [Fact]
        public void GetPrices()
        {
            var sut = new StockExchangeProvider();

            var prices = sut.GetPrice("MSFT", "AAPL");

            Assert.Equal(2, prices.Length);
            Assert.Equal("MSFT", prices[0].Symbol);
            Assert.Equal("AAPL", prices[1].Symbol);
            Assert.True(prices[0].Price > 0.0m);
            Assert.True(prices[1].Price > 0.0m);
        }
    }
}