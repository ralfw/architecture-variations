using portfoliosimulation.contract.data;

namespace portfoliosimulation.contract
{
    public interface IPortfolioRepository
    {
        Portfolio Load();
        void Store(Portfolio portfolio);
    }
}