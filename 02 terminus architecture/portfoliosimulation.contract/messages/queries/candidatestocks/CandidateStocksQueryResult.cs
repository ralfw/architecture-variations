namespace portfoliosimulation.contract.messages.queries.candidatestocks
{
    public class CandidateStocksQueryResult : QueryResult
    {
        public class CandidateStock
        {
            public string Name;
            public string Symbol;
            public string Currency;
            public decimal Price;
        }
        
        public CandidateStock[] Candidates;
    }
}