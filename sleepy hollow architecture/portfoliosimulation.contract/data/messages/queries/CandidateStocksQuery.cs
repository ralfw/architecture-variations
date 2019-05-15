namespace portfoliosimulation.contract.data.messages.queries
{
    public class CandidateStocksQuery  {
        public string Pattern;
    }
    
    public class CandidateStocksQueryResult
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