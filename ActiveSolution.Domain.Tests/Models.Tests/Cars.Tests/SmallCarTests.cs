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
        public void GetCalculatedRentingPrice_WithValidParams_ShouldCalculateCorrectPrices()
        {
            const int baseDayPrice = 1000;

            var smallCar = new SmallCar("abc123", CarType.Small);
            var carReturn = new CarReturn(123, DateTime.Now, 300);

            var calculatedPrice = smallCar.GetCalculatedRentingPrice(carReturn, DateTime.Now.AddMinutes(-1), baseDayPrice);
            Assert.AreEqual(baseDayPrice, calculatedPrice, "Same day");

            carReturn = new CarReturn(123, DateTime.Now, 300);
            calculatedPrice = smallCar.GetCalculatedRentingPrice(carReturn, DateTime.Now.AddMinutes(-1), 2000);
            Assert.AreEqual(2000, calculatedPrice, "Same day, baseDayPrice = 2000");

            carReturn = new CarReturn(123, DateTime.Now, 300);
            calculatedPrice = smallCar.GetCalculatedRentingPrice(carReturn, DateTime.Now.AddDays(-1), baseDayPrice);
            Assert.AreEqual(baseDayPrice, calculatedPrice, "1 day");

            carReturn = new CarReturn(123, DateTime.Now, 300);
            calculatedPrice = smallCar.GetCalculatedRentingPrice(carReturn, DateTime.Now.AddDays(-2), baseDayPrice);
            Assert.AreEqual(2000, calculatedPrice, "2 days");

            carReturn = new CarReturn(123, DateTime.Now, 300);
            calculatedPrice = smallCar.GetCalculatedRentingPrice(carReturn, DateTime.Now.AddDays(-5), baseDayPrice);
            Assert.AreEqual(5000, calculatedPrice, "5 days");
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
