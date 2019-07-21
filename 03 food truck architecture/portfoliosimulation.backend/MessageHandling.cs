using System;
using System.Linq;
using messagehandling;
using portfoliosimulation.backend.adapters;
using portfoliosimulation.backend.domain;
using portfoliosimulation.backend.messagepipelines.queries.candidatestocksquery;
using portfoliosimulation.contract;
using portfoliosimulation.contract.data;
using portfoliosimulation.contract.messages.commands;
using portfoliosimulation.contract.messages.queries.candidatestocks;
using portfoliosimulation.contract.messages.queries.portfolio;
using portfoliosimulation.contract.messages.queries.portfoliostock;

namespace portfoliosimulation.backend
{
    public class MessageHandling : IMessageHandling
    {
        private readonly IPortfolioRepository _repo;
        private readonly IStockExchangeProvider _ex;

        public MessageHandling() : this(new PortfolioRepository(), new StockExchangeProvider()) {}
        internal MessageHandling(IPortfolioRepository repo, IStockExchangeProvider ex) {
            _repo = repo;
            _ex = ex;
        }
        
        
        public CommandStatus Handle(UpdatePortfolioCommand command)
        {
            var portfolio = _repo.Load();
            var currentPrices = _ex.GetPrice(portfolio.StockSymbols);
            // Convert currency if needed; left out for simplicity
            portfolio.Update(currentPrices);
            _repo.Store(portfolio);
            return new Success();
        }

        
        public PortfolioQueryResult Handle(PortfolioQuery query)  {
            var portfolio = _repo.Load();
            var portfolioReturns = PortfolioManager.CalculateReturns(portfolio);
            return Result();

            
            PortfolioQueryResult Result() {
                return new PortfolioQueryResult {
                    PortfolioValue = portfolio.Entries.Sum(e => e.Qty * e.CurrentPrice),
                    PortfolioRateOfReturn = portfolioReturns.TotalRateOfReturn,
                    Stocks = portfolio.Entries.Select(Map).ToArray()
                };


                PortfolioQueryResult.StockInfo Map(Portfolio.Stock e)
                    => new PortfolioQueryResult.StockInfo {
                        Name = e.Name,
                        Symbol = e.Symbol,
                        Currency = e.Currency,
                        Qty = e.Qty,
                        BuyingPrice = e.BuyingPrice,
                        CurrentPrice = e.CurrentPrice,
                        
                        BuyingValue = e.Qty * e.BuyingPrice,
                        CurrentValue = e.Qty * e.CurrentPrice,
                        
                        Return = portfolioReturns.Returns[e.Symbol].Return,
                        RateOfReturn = portfolioReturns.Returns[e.Symbol].RateOfReturn
                    };
            }
        }

        
        public CommandStatus Handle(BuyStockCommand command)
        {
            var portfolio = _repo.Load();
            
            var newStock = new Portfolio.Stock {
                Name = command.StockName,
                Symbol = command.StockSymbol,
                Currency = command.StockPriceCurrency,
                Qty = command.Qty,
                BuyingPrice = command.StockPrice,
                Bought = command.Bought,
                CurrentPrice = command.StockPrice,
                LastUpdated = DateTime.Now
            };
            portfolio.Entries.Add(newStock);
            
            _repo.Store(portfolio);
            return new Success();
        }

        
        public CandidateStocksQueryResult Handle(CandidateStocksQuery query) {
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

        
        public CommandStatus Handle(SellStockCommand command) {
            var portfolio = _repo.Load();

            portfolio.Remove(command.StockSymbol);
            
            _repo.Store(portfolio);
            return new Success();
        }

        
        public PortfolioStockQueryResult Handle(PortfolioStockQuery query) {
            var portfolio = _repo.Load();
            var matching = portfolio.Find(query.Pattern);
            return new PortfolioStockQueryResult {
                MatchingStocks = matching.Select(Map).ToArray()
            };


            (string Name, string Symbol) Map(Portfolio.Stock match)
                => (match.Name, match.Symbol);
        }
    }
}