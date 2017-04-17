using System;
using ActiveSolution.Domain.Models.Renting;
using NUnit.Framework;

namespace ActiveSolution.Domain.Tests.Models.Tests.Renting.Tests
{
    [TestFixture]
    public class RentingBasePriceModelTests
    {
        [Test]
        public void Ctor_WithoutNegativeValuesAsParams_ShouldThrow()
        {
            Assert.Throws(typeof(ArgumentException), () => new RentingBasePriceModel(-5, 10));
            Assert.Throws(typeof(ArgumentException), () => new RentingBasePriceModel(5, -10));
        }

        [Test]
        public void Ctor_WithoutParams_ShouldPopulatePropertiesWithDefaultValus()
        {
            var pricing = new RentingBasePriceModel();

            Assert.AreEqual(0, pricing.DayPrice);
            Assert.AreEqual(0, pricing.KilometerPrice);
        }

        [Test]
        public void Ctor_WithValidParams_ShouldPopulateProperties()
        {
            var pricing = new RentingBasePriceModel(1000, 5);

            Assert.AreEqual(1000, pricing.DayPrice);
            Assert.AreEqual(5, pricing.KilometerPrice);
        }
    }
}