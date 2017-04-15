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
        public void Ctor_WithValidParams_ShouldPopulateProperties()
        {
            var combi = new CombiCar("abc123", CarType.Combi, 30000);

            Assert.AreEqual(combi.RegistrationNumber, "abc123");
            Assert.AreEqual(combi.Type, CarType.Combi);
            Assert.AreEqual(combi.KilometerDistance, 30000);
        }

        [Test]
        public void Ctor_WithNullOrEmptyRegistrationNumber_ShouldThrow()
        {
            Assert.Throws(typeof(ArgumentNullException), () => new CombiCar(null, CarType.Combi, 30000));
            Assert.Throws(typeof(ArgumentNullException), () => new CombiCar("", CarType.Combi, 30000));
            Assert.Throws(typeof(ArgumentNullException), () => new CombiCar(" ", CarType.Combi, 30000));
        }

        [Test]
        public void Ctor_WithNegativeKilometerDistance_ShouldThrow()
        {
            Assert.Throws(typeof(ArgumentException), () => new CombiCar("abc123", CarType.Combi, -30000));
        }

        [Test]
        public void GetCalculatedRentingPrice_WithValidParams_ShouldCalculateCorrectPrices()
        {
            var combiCar = new CombiCar("abc123", CarType.Combi);

            var carReturn = new CarReturn(123, DateTime.Now, 300);
            var calculatedPrice = combiCar.GetCalculatedRentingPrice(carReturn, DateTime.Now.AddMinutes(-1), 1000, 5);
            Assert.AreEqual(2800, calculatedPrice);

            carReturn = new CarReturn(123, DateTime.Now, 300);
            calculatedPrice = combiCar.GetCalculatedRentingPrice(carReturn, DateTime.Now.AddMinutes(-1), 2000, 5);
            Assert.AreEqual(4100, calculatedPrice);

            carReturn = new CarReturn(123, DateTime.Now, 300);
            calculatedPrice = combiCar.GetCalculatedRentingPrice(carReturn, DateTime.Now.AddDays(-1), 1000, 5);
            Assert.AreEqual(2800, calculatedPrice);

            carReturn = new CarReturn(123, DateTime.Now, 300);
            calculatedPrice = combiCar.GetCalculatedRentingPrice(carReturn, DateTime.Now.AddDays(-2), 1000, 5);
            Assert.AreEqual(4100, calculatedPrice);

            carReturn = new CarReturn(123, DateTime.Now, 300);
            calculatedPrice = combiCar.GetCalculatedRentingPrice(carReturn, DateTime.Now.AddDays(-5), 1000, 5);
            Assert.AreEqual(8000, calculatedPrice);

            carReturn = new CarReturn(123, DateTime.Now, 300);
            calculatedPrice = combiCar.GetCalculatedRentingPrice(carReturn, DateTime.Now.AddDays(-5), 1000, 7);
            Assert.AreEqual(8600, calculatedPrice);
        }

        [Test]
        public void GetCalculatedRentingPrice_WithNullCarReturn_ShouldThrow()
        {
            var combiCar = new CombiCar("abc123", CarType.Combi);

            Assert.Throws(typeof(ArgumentNullException),
                () => combiCar.GetCalculatedRentingPrice(null, DateTime.Now, 1000), "carRented");
        }

        [Test]
        public void GetCalculatedRentingPrice_WithNegativeBasePrice_ShouldThrow()
        {
            var combiCar = new CombiCar("abc123", CarType.Combi);
            var carReturn = new CarReturn(123, DateTime.Now, 3000);

            Assert.Throws(typeof(ArgumentException),
                () => combiCar.GetCalculatedRentingPrice(carReturn, DateTime.Now, -1000));
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
