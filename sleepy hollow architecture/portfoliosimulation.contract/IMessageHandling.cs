using System;
using portfoliosimulation.contract.data.messages.commands;
using portfoliosimulation.contract.data.messages.queries;

namespace portfoliosimulation.contract
{
    public interface IMessageHandling
    {
        CommandStatus Handle(UpdatePortfolioCommand command);
        PortfolioQueryResult Handle(PortfolioQuery query);
        CommandStatus Handle(BuyStockCommand command);
        CandidateStocksQueryResult Handle(CandidateStocksQuery query);
        CommandStatus Handle(SellStockCommand command);
        PortfolioStockQueryResult Handle(PortfolioStockQuery query);
    }
}