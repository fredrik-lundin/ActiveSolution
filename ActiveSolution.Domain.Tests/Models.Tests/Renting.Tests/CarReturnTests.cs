using System;
using ActiveSolution.Domain.Models.Renting;
using NUnit.Framework;

namespace ActiveSolution.Domain.Tests.Models.Tests.Renting.Tests
{
    [TestFixture]
    public class CarReturnTests
    {
        [Test]
        public void Ctor_WithValidParams_ShouldPopulateProperties()
        {
            var now = DateTime.Now;
            var carReturn = new CarReturn(123, now, 3000);

            Assert.AreEqual(carReturn.BookingNumber, 123);
            Assert.AreEqual(carReturn.ReturnedDate, now);
            Assert.AreEqual(carReturn.NewKilometerDistance, 3000);
        }

        [Test]
        public void Ctor_NegativeBookingNumber_ShouldThrow()
        {
            Assert.Throws(typeof(ArgumentException), () => new CarReturn(-123, DateTime.Now, 3000));
        }

        [Test]
        public void Ctor_NegativeNewKilometerDistance_ShouldThrow()
        {
            Assert.Throws(typeof(ArgumentException), () => new CarReturn(123, DateTime.Now, -3000));
        }
    }
}
