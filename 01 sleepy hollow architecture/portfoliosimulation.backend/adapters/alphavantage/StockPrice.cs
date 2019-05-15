namespace onlinestocks
{
    public class StockPrice {
        public StockPrice(string id, double price) {
            Id = id;
            Price = price;
        }
        
        public string Id { get; }
        public double Price { get; }
    }
}