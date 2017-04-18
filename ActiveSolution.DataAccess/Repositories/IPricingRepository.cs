using ActiveSolution.Domain.Models.Renting;

namespace ActiveSolution.DataAccess.Repositories
{
    public interface IPricingRepository
    {
        RentingBasePriceModel GetCurrentBasePricing();
    }
}