using System;
using System.Linq;
using eventstore;
using messagehandling;
using messagehandling.pipeline;
using messagehandling.pipeline.messagecontext;
using portfoliosimulation.backend.events;
using portfoliosimulation.contract;
using portfoliosimulation.contract.messages.commands;

namespace portfoliosimulation.backend.messagepipelines.commands.updateportfoliocommand
{
    public class UpdatePortfolioCommandProcessor : IMessageProcessor {
        private readonly IStockExchangeProvider _ex;

        public UpdatePortfolioCommandProcessor(IStockExchangeProvider ex) { _ex = ex; }
        
        public Output Process(IMessage _, IMessageContext context) {
            var cmdModel = context as UpdatePortfolioCommandContextModel;
            
            var currentPrices = _ex.GetPrice(cmdModel.Values.ToArray());
            var events = currentPrices.Select(Map).ToArray();

            return new CommandOutput(new Success(), events);


            Event Map((string symbol, decimal price) stockdata)
                => new StockPriceUpdated {
                    Symbol = stockdata.symbol,
                    Price = stockdata.price
                };
        }
    }
}