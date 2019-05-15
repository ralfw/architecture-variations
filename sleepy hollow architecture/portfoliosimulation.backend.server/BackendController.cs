using System;
using portfoliosimulation.contract;
using portfoliosimulation.contract.data.messages.commands;
using portfoliosimulation.contract.data.messages.queries;
using servicehost.contract;

namespace portfoliosimulation.backend.server
{
    [Service]
    public class BackendController
    {
        public static IMessageHandling Handler;


        [EntryPoint(HttpMethods.Post, "/api/updateportfoliocommand")]
        public HttpCommandStatus Handle([Payload] UpdatePortfolioCommand command)
            => new HttpCommandStatus(Handler.Handle(command));

        [EntryPoint(HttpMethods.Post, "/api/portfolioquery")]
        public PortfolioQueryResult Handle([Payload] PortfolioQuery query)
            => Handler.Handle(query);

        [EntryPoint(HttpMethods.Post, "/api/buystockcommand")]
        public HttpCommandStatus Handle([Payload] BuyStockCommand command)
            => new HttpCommandStatus(Handler.Handle(command));

        [EntryPoint(HttpMethods.Post, "/api/candidatestocksquery")]
        public CandidateStocksQueryResult Handle([Payload] CandidateStocksQuery query)
            => Handler.Handle(query);

        [EntryPoint(HttpMethods.Post, "/api/sellstockcommand")]
        public HttpCommandStatus Handle([Payload] SellStockCommand command)
            => new HttpCommandStatus(Handler.Handle(command));

        [EntryPoint(HttpMethods.Post, "/api/portfoliostockquery")]
        public PortfolioStockQueryResult Handle([Payload] PortfolioStockQuery query)
            => Handler.Handle(query); 
        
        
        [EntryPoint(HttpMethods.Get, "/api/ping")]
        public string Ping(string msg)
        {
            return msg.ToUpper() + " / " + DateTime.Now;
        }
    }
}