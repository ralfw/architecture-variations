using System;
using portfoliosimulation.contract;
using portfoliosimulation.contract.data.messages.commands;
using portfoliosimulation.contract.data.messages.queries;
using RestSharp;

namespace portfoliosimulation.distributed
{
    class BackendProxy : IMessageHandling
    {
        private const string BACKEND_API_BASE_URL = "http://localhost:8080/api/";
        
        private readonly HttpJsonClient _client;
        
        public BackendProxy() { _client = new HttpJsonClient(BACKEND_API_BASE_URL); }


        public CommandStatus Handle(UpdatePortfolioCommand command)
            => _client.Execute<HttpCommandStatus>("updateportfoliocommand", command).CommandStatus;

        public PortfolioQueryResult Handle(PortfolioQuery query)
            => _client.Execute<PortfolioQueryResult>("portfolioquery", query);


        public CommandStatus Handle(BuyStockCommand command)
            => _client.Execute<HttpCommandStatus>("buystockcommand", command).CommandStatus;

        public CandidateStocksQueryResult Handle(CandidateStocksQuery query)
            => _client.Execute<CandidateStocksQueryResult>("candidatestocksquery", query);

        public CommandStatus Handle(SellStockCommand command)
            => _client.Execute<HttpCommandStatus>("sellstockcommand", command).CommandStatus;

        public PortfolioStockQueryResult Handle(PortfolioStockQuery query)
            => _client.Execute<PortfolioStockQueryResult>("portfoliostockquery", query);
    }
}