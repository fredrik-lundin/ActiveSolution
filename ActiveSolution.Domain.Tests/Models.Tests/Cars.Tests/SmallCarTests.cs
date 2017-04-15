using System;
using ActiveSolution.Domain.Enums;
using ActiveSolution.Domain.Models.Cars;
using ActiveSolution.Domain.Models.Renting;
using NUnit.Framework;

namespace ActiveSolution.Domain.Tests.Models.Tests.Cars.Tests
{
    [TestFixture]
    public class SmallCarTests
    {
        [Test]
        public void Ctor_WithValidParams_ShouldPopulateProperties()
        {
            var smallCar = new SmallCar("abc123", CarType.Small, 30000);

            Assert.AreEqual(smallCar.RegistrationNumber, "abc123");
            Assert.AreEqual(smallCar.Type, CarType.Small);
            Assert.AreEqual(smallCar.KilometerDistance, 30000);
        }

        [Test]
        public void Ctor_WithNullOrEmptyRegistrationNumber_ShouldThrow()
        {
            Assert.Throws(typeof(ArgumentNullException), () => new SmallCar(null, CarType.Small, 30000));
            Assert.Throws(typeof(ArgumentNullException), () => new SmallCar("", CarType.Small, 30000));
            Assert.Throws(typeof(ArgumentNullException), () => new SmallCar(" ", CarType.Small, 30000));
        }

        [Test]
        public void Ctor_WithNegativeKilometerDistance_ShouldThrow()
        {
            Assert.Throws(typeof(ArgumentException), () => new SmallCar("abc123", CarType.Small, -30000));
        }

        [Test]
        public void GetCalculatedRentingPrice_WithValidParams_ShouldCalculateCorrectPrices()
        {
            var smallCar = new SmallCar("abc123", CarType.Small);

            var carReturn = new CarReturn(123, DateTime.Now, 300);
            var calculatedPrice = smallCar.GetCalculatedRentingPrice(carReturn, DateTime.Now.AddMinutes(-1), 1000);
            Assert.AreEqual(1000, calculatedPrice);

            carReturn = new CarReturn(123, DateTime.Now, 300);
            calculatedPrice = smallCar.GetCalculatedRentingPrice(carReturn, DateTime.Now.AddMinutes(-1), 2000);
            Assert.AreEqual(2000, calculatedPrice);

            carReturn = new CarReturn(123, DateTime.Now, 300);
            calculatedPrice = smallCar.GetCalculatedRentingPrice(carReturn, DateTime.Now.AddDays(-1), 1000);
            Assert.AreEqual(1000, calculatedPrice);

            carReturn = new CarReturn(123, DateTime.Now, 300);
            calculatedPrice = smallCar.GetCalculatedRentingPrice(carReturn, DateTime.Now.AddDays(-2), 1000);
            Assert.AreEqual(2000, calculatedPrice);

            carReturn = new CarReturn(123, DateTime.Now, 300);
            calculatedPrice = smallCar.GetCalculatedRentingPrice(carReturn, DateTime.Now.AddDays(-5), 1000);
            Assert.AreEqual(5000, calculatedPrice);
        }

        [Test]
        public void GetCalculatedRentingPrice_WithNullCarReturn_ShouldThrow()
        {
            var smallCar = new SmallCar("abc123", CarType.Small);

            Assert.Throws(typeof(ArgumentNullException),
                () => smallCar.GetCalculatedRentingPrice(null, DateTime.Now, 1000), "carRented");
        }

        [Test]
        public void GetCalculatedRentingPrice_WithNegativeBasePrice_ShouldThrow()
        {
            var smallCar = new SmallCar("abc123", CarType.Small);
            var carReturn = new CarReturn(123, DateTime.Now, 3000);

            Assert.Throws(typeof(ArgumentException),
                () => smallCar.GetCalculatedRentingPrice(carReturn, DateTime.Now, -1000));
        }

        [Test]
        public void GetCalculatedRentingPrice_WithReturnDateBeforeRentedDate_ShouldThrow()
        {
            var smallCar = new SmallCar("abc123", CarType.Small);
            var carReturn = new CarReturn(123, DateTime.Now, 3000);

            Assert.Throws(typeof(ArgumentException),
                () => smallCar.GetCalculatedRentingPrice(carReturn, DateTime.Now.AddDays(1), 1000));
        }
    }
}
