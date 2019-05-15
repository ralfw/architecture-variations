using System;
using portfoliosimulation.contract;
using portfoliosimulation.contract.data.domain;
using portfoliosimulation.contract.messages;
using portfoliosimulation.contract.messages.commands.buystock;

namespace portfoliosimulation.backend.messagehandlers
{
    public class BuyStockCommandHandler : IBuyStockCommandHandling
    {
        private readonly IPortfolioRepository _repo;
        
        public BuyStockCommandHandler(IPortfolioRepository repo) { _repo = repo; }
        
        
        public CommandStatus Handle(BuyStockCommand command) {
            var portfolio = _repo.Load();
            
            var newStock = new Portfolio.Stock {
                Name = command.StockName,
                Symbol = command.StockSymbol,
                Currency = command.StockPriceCurrency,
                Qty = command.Qty,
                BuyingPrice = command.StockPrice,
                Bought = command.Bought,
                CurrentPrice = command.StockPrice,
                LastUpdated = DateTime.Now
            };
            portfolio.Entries.Add(newStock);
            
            _repo.Store(portfolio);
            return new Success();
        }
    }
}