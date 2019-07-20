using System;
using portfoliosimulation.contract.messages.commands;
using portfoliosimulation.contract.messages.queries.candidatestocks;
using portfoliosimulation.contract.messages.queries.portfolio;
using portfoliosimulation.contract.messages.queries.portfoliostock;

namespace portfoliosimulation.frontend
{
    public class UserInterface
    {
        public event Action<PortfolioQuery> OnPortfolioQuery;
        
        public event Action<UpdatePortfolioCommand> OnUpdatePortfolioCommand;
        
        public event Action<CandidateStocksQuery> OnCandidateStocksQuery;

        public event Action<PortfolioStockQuery> OnPortfolioStockQuery;
        
        
        public void Run() {
            OnPortfolioQuery(new PortfolioQuery());
            MenuLoop();
        }

        
        private void MenuLoop() {
            while (true) {
                Console.Write(">>> D(isplay, B(uy, S(ell, U(pdate, eX(it?: ");
                switch (Console.ReadLine().ToUpper()) {
                    case "X": return;
                    
                    case "D":
                        OnPortfolioQuery(new PortfolioQuery());
                        break;
                    
                    case "U":
                        Console.WriteLine("Ûpdating...");
                        OnUpdatePortfolioCommand(new UpdatePortfolioCommand());
                        break;
                    
                    case "B":
                        Ask_user_for_stock_identification(
                            id => OnCandidateStocksQuery(new CandidateStocksQuery{Pattern = id}));
                        break;
                    
                    case "S":
                        Ask_user_for_stock_identification(
                            id => OnPortfolioStockQuery(new PortfolioStockQuery{Pattern = id}));
                        break;
                }
            }


            void Ask_user_for_stock_identification(Action<string> onId) {
                Console.Write("Identification?: ");
                var input = Console.ReadLine();
                if (input == "") return;

                Console.WriteLine("Loading candidates...");
                onId(input);
            }
        }

        
        public bool SelectStockToBuy(CandidateStocksQueryResult candidates, out BuyStockCommand command)
        {
            BuyStockCommand buyCmd = null;
            
            DisplayBuyCandidates(candidates.Candidates);
            Let_user_select_by_index("Enter index of stock to buy: ", index => {
                var chosenStock = candidates.Candidates[index];
                DisplayChosenCandidate(chosenStock);
                Ask_user_for_qty_to_buy(qty => {
                    buyCmd = new BuyStockCommand {
                        StockName = chosenStock.Name,
                        StockSymbol = chosenStock.Symbol,
                        StockPriceCurrency = chosenStock.Currency,
                        Qty = qty,
                        StockPrice = chosenStock.Price,
                        Bought = DateTime.Now
                    };
                });
            });

            command = buyCmd;
            return command != null;


            void Ask_user_for_qty_to_buy(Action<int> onQty) {
                Console.Write("Buy qty?: ");
                var input = Console.ReadLine();
                if (input == "") return;
                
                var qty = int.Parse(input);
                onQty(qty);
            }
        }
        
        
        public bool SelectStockToSell(PortfolioStockQueryResult candidates, out SellStockCommand command)
        {
            SellStockCommand sellCmd = null;
            
            DisplaySellCandidates(candidates.MatchingStocks);
            Let_user_select_by_index("Enter index of stock to sell: ", index => {
                var toSell = candidates.MatchingStocks[index];
                sellCmd = new SellStockCommand {StockSymbol = toSell.Symbol};
            });

            command = sellCmd;
            return command != null;
        }
        
        
        private void Let_user_select_by_index(string prompt, Action<int> onIndex) {
            Console.Write(prompt);
            var input = Console.ReadLine();
            if (input == "") return;

            var index = int.Parse(input) - 1;
            onIndex(index);
        }

        
        private static void DisplayBuyCandidates(CandidateStocksQueryResult.CandidateStock[] candidates) {
            for (var i = 0; i < candidates.Length; i++)
                Console.WriteLine($"{i+1}. {candidates[i].Name} ({candidates[i].Symbol}): {candidates[i].Price:F} {candidates[i].Currency}");
        }
        
        private void DisplayChosenCandidate(CandidateStocksQueryResult.CandidateStock candidate) {
            Console.WriteLine($"{candidate.Name} ({candidate.Symbol})");
            Console.WriteLine($"{candidate.Price} {candidate.Currency}");
        }
        
        private void DisplaySellCandidates((string Name, string Symbol)[] candidatesMatchingStocks) {
            for(var i=0; i<candidatesMatchingStocks.Length; i++)
                Console.WriteLine($"{i+1}. {candidatesMatchingStocks[i].Name} ({candidatesMatchingStocks[i].Symbol})");
        }


        public void Display(PortfolioQueryResult portfolio) {
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


        public void DisplayBuyConfirmation(string stockSymbol, int qty, decimal price) {
            Console.WriteLine($"Bought {qty} x {stockSymbol} at {price} = {qty * price}");
        }


        public void DisplaySellConfirmation(string stockSymbol) {
            Console.WriteLine($"Sold all '{stockSymbol}'!");
        }
    }
}