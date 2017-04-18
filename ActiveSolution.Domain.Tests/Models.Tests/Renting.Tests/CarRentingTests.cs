using System;
using ActiveSolution.Domain.Models.Renting;
using NUnit.Framework;

namespace ActiveSolution.Domain.Tests.Models.Tests.Renting.Tests
{
    [TestFixture]
    public class CarRentingTests
    {
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

        [Test]
        public void ReturnCar_WithValidParams_ShouldPopulateProperties()
        {
            var carRenting = new CarRenting(123, "abc123", "900504", DateTime.Now, 0);

            Assert.AreEqual(null, carRenting.ReturnDate);
            Assert.AreEqual(null, carRenting.ReturnKilometerDistance);

            var returnDate = DateTime.Now.AddDays(1);
            carRenting.ReturnCar(returnDate, 3000);

            Assert.AreEqual(returnDate, carRenting.ReturnDate);
            Assert.AreEqual(3000, carRenting.ReturnKilometerDistance);
        }

        [Test]
        public void ReturnCar_NegativeNewKilometerDistance_ShouldThrow()
        {
            var carRenting = new CarRenting(123, "abc123", "900504", DateTime.Now, 0);
            Assert.Throws(typeof(ArgumentException), () => carRenting.ReturnCar(DateTime.Now, -3000));
        }

        [Test]
        public void ReturnCar_WithReturnDateBeforeRentingDate_ShouldThrow()
        {
            var carRenting = new CarRenting(123, "abc123", "900504", DateTime.Now, 0);
            Assert.Throws(typeof(ArgumentException), () => carRenting.ReturnCar(DateTime.Now.AddDays(-1), 3000));
        }
    }
}