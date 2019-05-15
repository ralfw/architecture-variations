using eventstore;
using messagehandling;
using messagehandling.pipeline;
using messagehandling.pipeline.messagecontext;
using portfoliosimulation.backend.events;
using portfoliosimulation.contract.messages.commands;

namespace portfoliosimulation.backend.messagepipelines.commands.buystockcommand
{
    public class BuyStockCommandProcessor : IMessageProcessor {
        public Output Process(IMessage input, IMessageContext _){
            var cmd = input as BuyStockCommand;
            return new CommandOutput(
                new Success(),
                new Event[] {
                    new StockBought {
                        Name = cmd.StockName,
                        Symbol = cmd.StockSymbol,
                        Currenncy = cmd.StockPriceCurrency,
                        Qty = cmd.Qty,
                        Price = cmd.StockPrice,
                        Timestamp = cmd.Bought
                    }, 
                    new StockPriceUpdated {
                        Symbol = cmd.StockSymbol,
                        Price = cmd.StockPrice
                    }, 
                }
            );
        }
    }
}