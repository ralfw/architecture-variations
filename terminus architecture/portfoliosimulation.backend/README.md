# Backend Development
## Increments
1. √Query current portfolio (PortfolioQuery)
2. √Update portfolio (UpdatePortfolioCommand)
3. √Query candidates (CandidateStocksQuery)
4. √Buy stock (BuyStockCommand)
5. √Sell stock (√SellStockCommand, PortfolioStockQuery)

### Query current portfolio
The portfolio is loaded as is and some additional data is calculated.

#### Database
```
Name;Symbol;Currency;Bought;Qty;BuyingPrice;CurrentPrice;LastUpdated
Apple Inc;AAPL;USD;2019-04-11;5;200.00;210.52;2019-05-02
Facebook;FB;USD;2019-02-04;7;168.00;193.03;2019-05-02
Tesla;TSLA;USD;2018-09-15;8;292.00;234.01;2019-05-02
```

