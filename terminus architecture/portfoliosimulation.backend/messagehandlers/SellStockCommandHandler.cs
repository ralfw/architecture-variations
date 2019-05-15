using portfoliosimulation.contract;
using portfoliosimulation.contract.messages;
using portfoliosimulation.contract.messages.commands.sellstock;

namespace portfoliosimulation.backend.messagehandlers
{
    public class SellStockCommandHandler : ISellStockCommandHandling
    {
        private readonly IPortfolioRepository _repo;

        public SellStockCommandHandler(IPortfolioRepository repo) { _repo = repo; }
        
        
        public CommandStatus Handle(SellStockCommand command) {
            var portfolio = _repo.Load();

            portfolio.Remove(command.StockSymbol);
            
            _repo.Store(portfolio);
            return new Success();
        }
    }
}