using System;
using ActiveSolution.DataAccess.Repositories;
using ActiveSolution.Domain.Models.Renting;

namespace ActiveSolution.Services.Tests.MockRepositoryImplementations
{
    public class MockPricingRepository : IPricingRepository
    {
        private RentingBasePriceModel _rentingBasePrice;
        public RentingBasePriceModel GetCurrentBasePricing() => _rentingBasePrice;

        public void SetCurrentBasePricing(RentingBasePriceModel pricing) =>
            _rentingBasePrice = pricing;
    }
}
