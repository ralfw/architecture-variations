using System.Linq;
using portfoliosimulation.contract;
using portfoliosimulation.contract.data.domain;
using portfoliosimulation.contract.messages.queries.portfoliostock;

namespace portfoliosimulation.backend.messagehandlers
{
    public class PortfolioStockQueryHandler : IPortfolioStockQuery {
        private readonly IPortfolioRepository _repo;

        public PortfolioStockQueryHandler(IPortfolioRepository repo) { _repo = repo; }
        
        
        public PortfolioStockQueryResult Handle(PortfolioStockQuery query)
        {
            var portfolio = _repo.Load();
            var matching = portfolio.Find(query.Pattern);
            return new PortfolioStockQueryResult {
                MatchingStocks = matching.Select(Map).ToArray()
            };


            (string Name, string Symbol) Map(Portfolio.Stock match) => (match.Name, match.Symbol);
        }
    }
}