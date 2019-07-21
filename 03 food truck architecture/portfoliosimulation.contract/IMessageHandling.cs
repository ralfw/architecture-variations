
using messagehandling;
using portfoliosimulation.contract.messages.commands;
using portfoliosimulation.contract.messages.queries.candidatestocks;
using portfoliosimulation.contract.messages.queries.portfolio;
using portfoliosimulation.contract.messages.queries.portfoliostock;

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