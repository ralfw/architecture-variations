using System;
using portfoliosimulation.backend;
using portfoliosimulation.backend.adapters;
using portfoliosimulation.backend.messagehandlers;
using portfoliosimulation.contract;
using portfoliosimulation.contract.messages.queries.portfolio;
using portfoliosimulation.contract.messages.queries.portfoliostock;
using portfoliosimulation.frontend;

namespace portfoliosimulation
{
    class Program
    {
        static void Main(string[] args) {
            var repo = new PortfolioRepository();
            var ex = new StockExchangeProvider();

            var pqh = new PortfolioQueryHandler(repo);
            var upc = new UpdatePortfolioCommandHandler(repo, ex);
            var csq = new CandidateStocksQueryHandler(ex);
            var bsc = new BuyStockCommandHandler(repo);
            var psq = new PortfolioStockQueryHandler(repo);
            var ssc = new SellStockCommandHandler(repo);
            
            var frontend = new UserInterface();
            
            frontend.OnPortfolioQuery += q => {
                var result = pqh.Handle(q);
                frontend.Display(result);
            };

            frontend.OnUpdatePortfolioCommand += c => {
                upc.Handle(c);
                var result = pqh.Handle(new PortfolioQuery());
                frontend.Display(result);
            };

            frontend.OnCandidateStocksQuery += q => {
                var result = csq.Handle(q);
                if (frontend.SelectStockToBuy(result, out var cmd)) {
                    bsc.Handle(cmd);
                    frontend.DisplayBuyConfirmation(cmd.StockSymbol, cmd.Qty, cmd.StockPrice);
                }
            };

            frontend.OnPortfolioStockQuery += q =>
            {
                var result = psq.Handle(q);
                if (frontend.SelectStockToSell(result, out var cmd)) {
                    ssc.Handle(cmd);
                    frontend.DisplaySellConfirmation(cmd.StockSymbol);
                }
            };
            

            frontend.Run();
        }
    }
}