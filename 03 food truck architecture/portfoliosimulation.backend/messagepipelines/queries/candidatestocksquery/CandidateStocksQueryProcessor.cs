using System.Linq;
using messagehandling;
using messagehandling.pipeline;
using messagehandling.pipeline.messagecontext;
using portfoliosimulation.contract;
using portfoliosimulation.contract.messages.queries.candidatestocks;

namespace portfoliosimulation.backend.messagepipelines.queries.candidatestocksquery
{
    public class CandidateStocksQueryProcessor : IMessageProcessor {
        private readonly IStockExchangeProvider _ex;

        public CandidateStocksQueryProcessor(IStockExchangeProvider ex) { _ex = ex; }
        
        
        public Output Process(IMessage input, IMessageContext model)
        {
            var query = input as CandidateStocksQuery;
            
            var candidates = _ex.FindCandidates(query.Pattern);
            
            return new QueryOutput(
                new CandidateStocksQueryResult {
                    Candidates = candidates.Select(Map).ToArray()
                }
            );

            
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