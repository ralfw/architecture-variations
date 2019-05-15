using eventstore;
using messagehandling;
using messagehandling.pipeline;
using messagehandling.pipeline.messagecontext;
using portfoliosimulation.backend.adapters;
using portfoliosimulation.backend.messagepipelines.commands.buystockcommand;
using portfoliosimulation.backend.messagepipelines.commands.sellstockcommand;
using portfoliosimulation.backend.messagepipelines.commands.updateportfoliocommand;
using portfoliosimulation.backend.messagepipelines.queries.candidatestocksquery;
using portfoliosimulation.backend.messagepipelines.queries.portfolioquery;
using portfoliosimulation.backend.messagepipelines.queries.portfoliostockquery;
using portfoliosimulation.contract.messages.commands;
using portfoliosimulation.contract.messages.queries.candidatestocks;
using portfoliosimulation.contract.messages.queries.portfolio;
using portfoliosimulation.contract.messages.queries.portfoliostock;
using portfoliosimulation.frontend;

namespace portfoliosimulation
{
    class Program
    {
        static void Main(string[] args) {
            var ex = new StockExchangeProvider();
            
            using(var es = new EventStore("eventstream.db"))
            using (var msgpump = new MessagePump(es)) {
                IMessageContextManager mcm;
                IMessageProcessor mp;

                mcm = new BuyStockCommandContextManager();
                mp = new BuyStockCommandProcessor();
                msgpump.Register<BuyStockCommand>(mcm, mp);

                mcm = new SellStockCommandManager(es);
                mp = new SellStockCommandProcessor();
                msgpump.Register<SellStockCommand>(mcm, mp);

                mcm = new UpdatePortfolioCommandContextManager(es);
                mp = new UpdatePortfolioCommandProcessor(ex);
                msgpump.Register<UpdatePortfolioCommand>(mcm, mp);

                mcm = new CandidateStocksQueryContextManager();
                mp = new CandidateStocksQueryProcessor(ex);
                msgpump.Register<CandidateStocksQuery>(mcm, mp);

                mcm = new PortfolioQueryContextManager(es);
                mp = new PortfolioQueryProcessor();
                msgpump.Register<PortfolioQuery>(mcm, mp);

                mcm = new PortfolioStockQueryContextManager(es);
                mp = new PortfolioStockQueryProcessor();
                msgpump.Register<PortfolioStockQuery>(mcm, mp);

                var frontend = new UserInterface();


                frontend.OnPortfolioQuery += q => {
                    var result = msgpump.Handle(q) as PortfolioQueryResult;
                    frontend.Display(result);
                };

                frontend.OnUpdatePortfolioCommand += c => {
                    msgpump.Handle(c);
                    var result = msgpump.Handle(new PortfolioQuery()) as PortfolioQueryResult;
                    frontend.Display(result);
                };

                frontend.OnCandidateStocksQuery += q => {
                    var result = msgpump.Handle(q) as CandidateStocksQueryResult;
                    if (frontend.SelectStockToBuy(result, out var cmd)) {
                        msgpump.Handle(cmd);
                        frontend.DisplayBuyConfirmation(cmd.StockSymbol, cmd.Qty, cmd.StockPrice);
                    }
                };

                frontend.OnPortfolioStockQuery += q => {
                    var result = msgpump.Handle(q) as PortfolioStockQueryResult;
                    if (frontend.SelectStockToSell(result, out var cmd)) {
                        msgpump.Handle(cmd);
                        frontend.DisplaySellConfirmation(cmd.StockSymbol);
                    }
                };


                frontend.Run();
            }
        }
    }
}