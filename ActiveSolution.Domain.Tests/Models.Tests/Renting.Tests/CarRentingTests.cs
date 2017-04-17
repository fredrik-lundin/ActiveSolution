using System;
using ActiveSolution.Domain.Models.Cars;
using ActiveSolution.Domain.Models.Renting;
using NUnit.Framework;

namespace ActiveSolution.Domain.Tests.Models.Tests.Renting.Tests
{
    [TestFixture]
    public class CarRentingTests
    {
        private static SmallCar SmallCar => new SmallCar("abc123");

        [Test]
        public void Ctor_NegativeBookingNumber_ShouldThrow()
        {
            Assert.Throws(typeof(ArgumentException), () => new CarRenting(-1, "abc123", "900504", DateTime.Now, 10));
        }

        [Test]
        public void Ctor_WithNegativeKilometerDistance_ShouldThrow()
        {
            Assert.Throws(typeof(ArgumentException), () => new CarRenting(1, "abc123", "900504", DateTime.Now, -10));
        }

        [Test]
        public void Ctor_WithNullOrEmptyRegistrationNumber_ShouldThrow()
        {
            Assert.Throws(typeof(ArgumentNullException), () => new CarRenting(1, null, "900504", DateTime.Now, 10));
            Assert.Throws(typeof(ArgumentNullException), () => new CarRenting(1, "", "900504", DateTime.Now, 10));
            Assert.Throws(typeof(ArgumentNullException), () => new CarRenting(1, "   ", "900504", DateTime.Now, 10));
        }

        [Test]
        public void Ctor_WithNullOrEmptyRenterId_ShouldThrow()
        {
            Assert.Throws(typeof(ArgumentNullException), () => new CarRenting(1, "abc123", null, DateTime.Now, 10));
            Assert.Throws(typeof(ArgumentNullException), () => new CarRenting(1, "abc123", "", DateTime.Now, 10));
            Assert.Throws(typeof(ArgumentNullException), () => new CarRenting(1, "abc123", "   ", DateTime.Now, 10));
        }

        [Test]
        public void Ctor_WithValidParams_ShouldPopulateProperties()
        {
            var now = DateTime.Now;
            var carRenting = new CarRenting(1, "abc123", "900504", now, 10);

            Assert.AreEqual(1, carRenting.BookingNumber);
            Assert.AreEqual("abc123", carRenting.RegistrationNumber);
            Assert.AreEqual(carRenting.RenterId, "900504");
            Assert.AreEqual(now, carRenting.RentingDate);
            Assert.AreEqual(10, carRenting.StartKilometerDistance);
        }
    }
}