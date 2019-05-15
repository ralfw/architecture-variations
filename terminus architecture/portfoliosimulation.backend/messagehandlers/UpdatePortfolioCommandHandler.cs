using portfoliosimulation.contract;
using portfoliosimulation.contract.messages;
using portfoliosimulation.contract.messages.commands.updateportfolio;

namespace portfoliosimulation.backend.messagehandlers
{
    public class UpdatePortfolioCommandHandler : IUpdatePortfolioCommandHandling {
        private readonly IPortfolioRepository _repo;
        private readonly IStockExchangeProvider _ex;

        public UpdatePortfolioCommandHandler(IPortfolioRepository repo, IStockExchangeProvider ex) {
            _repo = repo;
            _ex = ex;
        }

        
        public CommandStatus Handle(UpdatePortfolioCommand request)
        {
            var portfolio = _repo.Load();
            var currentPrices = _ex.GetPrice(portfolio.StockSymbols);
            // Convert currency if needed; left out for simplicity
            portfolio.Update(currentPrices);
            _repo.Store(portfolio);
            return new Success();
        }
    }
}