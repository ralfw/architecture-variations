using System;
using System.Linq.Expressions;
using System.Xml.Serialization;
using portfoliosimulation.contract;
using portfoliosimulation.contract.data.messages.commands;
using portfoliosimulation.contract.data.messages.queries;

namespace portfoliosimulation.frontend
{
    public class UserInterface
    {
        private readonly IMessageHandling _mh;

        public UserInterface(IMessageHandling mh) {
            _mh = mh;
        }


        public void Run() {
            var portfolio = _mh.Handle(new PortfolioQuery());
            Display(portfolio);
            MenuLoop();
        }

        
        private void MenuLoop() {
            while (true) {
                Console.Write($">>> D(isplay, B(uy, S(ell, U(pdate, eX(it?: ");
                switch (Console.ReadLine().ToUpper()) {
                    case "X": return;
                    
                    
                    case "D":
                    {
                        var portfolio = _mh.Handle(new PortfolioQuery());
                        Display(portfolio);
                    }
                        break;

                    
                    case "U":
                    {
                        _mh.Handle(new UpdatePortfolioCommand());
                        var portfolio = _mh.Handle(new PortfolioQuery());
                        Display(portfolio);
                    }
                        break;

                    
                    case "B":
                    {
                        Console.Write("Identification?: ");
                        var input = Console.ReadLine();
                        if (input == "") break;
                        
                        var candidates = _mh.Handle(new CandidateStocksQuery(){Pattern = input});
                        DisplayBuyCandidates(candidates.Candidates);

                        Console.Write("Index of stock to buy?: ");
                        input = Console.ReadLine();
                        if (input == "") break;

                        var index = int.Parse(input) - 1;
                        var toBuy = candidates.Candidates[index];
                        DisplayChosenCandidate(toBuy);
                        
                        Console.Write("Buy qty?: ");
                        input = Console.ReadLine();
                        if (input == "") break;

                        var qty = int.Parse(input);

                        _mh.Handle(new BuyStockCommand {
                            StockName = toBuy.Name,
                            StockSymbol = toBuy.Symbol,
                            StockPriceCurrency = toBuy.Currency,
                            Qty = qty,
                            StockPrice = toBuy.Price,
                            Bought = DateTime.Now
                        });
                        
                        Console.WriteLine($"Total paid: {qty * toBuy.Price}");
                    }
                        break;

                    
                    case "S":
                    {
                        Console.Write("Identification?: ");
                        var input = Console.ReadLine();
                        if (input == "") break;

                        var candidates = _mh.Handle(new PortfolioStockQuery{Pattern = input});

                        DisplaySellCandidates(candidates.MatchingStocks);
                        
                        Console.Write("Index of stock to sell?: ");
                        input = Console.ReadLine();
                        if (input == "") break;
                        var index = int.Parse(input) - 1;

                        var toSell = candidates.MatchingStocks[index];

                        _mh.Handle(new SellStockCommand {StockSymbol = toSell.Symbol});
                        
                        Console.WriteLine($"Sold '{toSell.Name} ({toSell.Symbol})'!");

                    }
                        break;
                }
            }
        }


        private void DisplaySellCandidates((string Name, string Symbol)[] candidatesMatchingStocks)
        {
            for(var i=0; i<candidatesMatchingStocks.Length; i++)
                Console.WriteLine($"{i+1}. {candidatesMatchingStocks[i].Name} ({candidatesMatchingStocks[i].Symbol})");
        }


        private void DisplayChosenCandidate(CandidateStocksQueryResult.CandidateStock candidate) {
            Console.WriteLine($"{candidate.Name} ({candidate.Symbol})");
            Console.WriteLine($"{candidate.Price} {candidate.Currency}");
        }


        private static void Display(PortfolioQueryResult portfolio) {
            if (portfolio.Stocks.Length == 0) {
                Console.WriteLine("Empty portfolio!");
                return;
            }
            
            for (var i = 0; i < portfolio.Stocks.Length; i++) {
                var s = portfolio.Stocks[i];
                Console.WriteLine($"{i + 1}. {s.Name} ({s.Symbol}), bought: {s.Qty}x{s.BuyingPrice:F}={s.Qty*s.BuyingPrice:F}, curr.: {s.Qty}x{s.CurrentPrice:F}={s.Qty*s.CurrentPrice:F} -> {s.Return:F} / {s.RateOfReturn:P}");
            }
            Console.WriteLine($"Portfolio value: {portfolio.PortfolioValue:F} / {portfolio.PortfolioRateOfReturn:P}");
        }
        
        
        private static void DisplayBuyCandidates(CandidateStocksQueryResult.CandidateStock[] candidates)
        {
            for (var i = 0; i < candidates.Length; i++)
                Console.WriteLine($"{i+1}. {candidates[i].Name} ({candidates[i].Symbol}): {candidates[i].Price:F} {candidates[i].Currency}");
        }
    }
}