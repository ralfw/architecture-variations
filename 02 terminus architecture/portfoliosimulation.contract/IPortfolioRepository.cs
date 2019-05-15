using portfoliosimulation.contract.data.domain;

namespace portfoliosimulation.contract
{
    public interface IPortfolioRepository
    {
        Portfolio Load();
        void Store(Portfolio portfolio);
    }
}