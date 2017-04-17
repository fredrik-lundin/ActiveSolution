using System;
using ActiveSolution.Domain.Enums;
using ActiveSolution.Domain.Models.Cars;
using ActiveSolution.Domain.Models.Renting;
using NUnit.Framework;

namespace ActiveSolution.Domain.Tests.Models.Tests.Cars.Tests
{
    [TestFixture]
    public class CombiCarTests
    {
        [Test]
        public void GetCalculatedRentingPrice_WithValidParams_ShouldCalculateCorrectPrices()
        {
            const int baseKilometerPrice = 5;
            const int baseDayPrice = 1000;

            var combiCar = new CombiCar("abc123", CarType.Combi);
            var carReturn = new CarReturn(123, DateTime.Now, 300);

            var calculatedPrice = combiCar.GetCalculatedRentingPrice(carReturn, DateTime.Now.AddMinutes(-1), baseDayPrice, baseKilometerPrice);
            Assert.AreEqual(2800, calculatedPrice, "Same day");

            carReturn = new CarReturn(123, DateTime.Now, 300);
            calculatedPrice = combiCar.GetCalculatedRentingPrice(carReturn, DateTime.Now.AddMinutes(-1), 2000, baseKilometerPrice);
            Assert.AreEqual(4100, calculatedPrice, "Same day, baseDayPrice = 2000");

            carReturn = new CarReturn(123, DateTime.Now, 300);
            calculatedPrice = combiCar.GetCalculatedRentingPrice(carReturn, DateTime.Now.AddDays(-1), baseDayPrice, baseKilometerPrice);
            Assert.AreEqual(2800, calculatedPrice, "1 day");

            carReturn = new CarReturn(123, DateTime.Now, 300);
            calculatedPrice = combiCar.GetCalculatedRentingPrice(carReturn, DateTime.Now.AddDays(-2), baseDayPrice, baseKilometerPrice);
            Assert.AreEqual(4100, calculatedPrice, "2 days");

            carReturn = new CarReturn(123, DateTime.Now, 300);
            calculatedPrice = combiCar.GetCalculatedRentingPrice(carReturn, DateTime.Now.AddDays(-5), baseDayPrice, baseKilometerPrice);
            Assert.AreEqual(8000, calculatedPrice, "5 days");

            carReturn = new CarReturn(123, DateTime.Now, 300);
            calculatedPrice = combiCar.GetCalculatedRentingPrice(carReturn, DateTime.Now.AddDays(-5), baseDayPrice, 7);
            Assert.AreEqual(8600, calculatedPrice, "5 days, baseKilometerPrice = 7");
        }

        [Test]
        public void GetCalculatedRentingPrice_WithNullCarReturn_ShouldThrow()
        {
            var combiCar = new CombiCar("abc123", CarType.Combi);

            Assert.Throws(typeof(ArgumentNullException),
                () => combiCar.GetCalculatedRentingPrice(null, DateTime.Now, 1000), "carRented");
        }

        [Test]
        public void GetCalculatedRentingPrice_WithNegativeBaseDayPrice_ShouldThrow()
        {
            var combiCar = new CombiCar("abc123", CarType.Combi);
            var carReturn = new CarReturn(123, DateTime.Now, 3000);

            Assert.Throws(typeof(ArgumentException),
                () => combiCar.GetCalculatedRentingPrice(carReturn, DateTime.Now, -1000, 1000));
        }

        [Test]
        public void GetCalculatedRentingPrice_WithNegativeBaseKilometerPrice_ShouldThrow()
        {
            var combiCar = new CombiCar("abc123", CarType.Combi);
            var carReturn = new CarReturn(123, DateTime.Now, 3000);

            Assert.Throws(typeof(ArgumentException),
                () => combiCar.GetCalculatedRentingPrice(carReturn, DateTime.Now, 1000, -1000));
        }

        [Test]
        public void GetCalculatedRentingPrice_WithReturnDateBeforeRentedDate_ShouldThrow()
        {
            var combiCar = new CombiCar("abc123", CarType.Combi);
            var carReturn = new CarReturn(123, DateTime.Now, 3000);

            Assert.Throws(typeof(ArgumentException),
                () => combiCar.GetCalculatedRentingPrice(carReturn, DateTime.Now.AddDays(1), 1000));
        }
    }
}
