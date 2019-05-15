using System.Linq;
using portfoliosimulation.contract;
using portfoliosimulation.contract.messages.queries.candidatestocks;

namespace portfoliosimulation.backend.messagehandlers
{
    public class CandidateStocksQueryHandler : ICandidateStocksQueryHandling {
        private readonly IStockExchangeProvider _ex;

        public CandidateStocksQueryHandler(IStockExchangeProvider ex) { _ex = ex; }
        
        
        public CandidateStocksQueryResult Handle(CandidateStocksQuery query)
        {
            var candidates = _ex.FindCandidates(query.Pattern);
            return new CandidateStocksQueryResult {
                Candidates = candidates.Select(Map).ToArray()
            };

            
            CandidateStocksQueryResult.CandidateStock Map(CandidateStockInfo candidate)
                => new CandidateStocksQueryResult.CandidateStock {
                    Name = candidate.Name,
                    Symbol = candidate.Symbol,
                    Currency = candidate.Currency,
                    Price = candidate.Price
                };
        }
    }
}