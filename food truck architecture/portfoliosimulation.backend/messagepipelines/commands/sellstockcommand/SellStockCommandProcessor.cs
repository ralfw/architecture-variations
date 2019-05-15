using eventstore;
using messagehandling;
using messagehandling.pipeline;
using messagehandling.pipeline.messagecontext;
using portfoliosimulation.backend.events;
using portfoliosimulation.contract.messages.commands;

namespace portfoliosimulation.backend.messagepipelines.commands.sellstockcommand
{
    public class SellStockCommandProcessor : IMessageProcessor {
        public Output Process(IMessage input, IMessageContext model){
            var cmd = input as SellStockCommand;
            var cmdModel = model as SellStockCommandContextModel;
            
            if (cmdModel.Values.Contains(cmd.StockSymbol))
                return new CommandOutput(
                    new Success(),
                    new Event[] {
                        new StockSold {
                            Symbol = cmd.StockSymbol
                        }, 
                    });
            return new CommandOutput(new Failure($"Stock '{cmd.StockSymbol}' not in portfolio. It cannot be sold!"));
        }
    }
}