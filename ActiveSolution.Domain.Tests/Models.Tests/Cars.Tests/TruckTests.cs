using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ActiveSolution.Domain.Enums;
using ActiveSolution.Domain.Models.Cars;
using ActiveSolution.Domain.Models.Renting;
using NUnit.Framework;

namespace ActiveSolution.Domain.Tests.Models.Tests.Cars.Tests
{
    [TestFixture]
    public class TruckTests
    {
        [Test]
        public void GetCalculatedRentingPrice_WithValidParams_ShouldCalculateCorrectPrices()
        {
            const int baseDayPrice = 1000;
            const int baseKilometerPrice = 5;

            var truck = new Truck("abc123", CarType.Truck);
            var carReturn = new CarReturn(123, DateTime.Now, 300);
            
            var calculatedPrice = truck.GetCalculatedRentingPrice(carReturn, DateTime.Now.AddMinutes(-1), baseDayPrice, baseKilometerPrice);
            Assert.AreEqual(3750, calculatedPrice, "Same day");

            carReturn = new CarReturn(123, DateTime.Now, 300);
            calculatedPrice = truck.GetCalculatedRentingPrice(carReturn, DateTime.Now.AddMinutes(-1), 2000, baseKilometerPrice);
            Assert.AreEqual(5250, calculatedPrice, "Same day, baseDayPrice = 2000");

            carReturn = new CarReturn(123, DateTime.Now, 300);
            calculatedPrice = truck.GetCalculatedRentingPrice(carReturn, DateTime.Now.AddDays(-1), baseDayPrice, baseKilometerPrice);
            Assert.AreEqual(3750, calculatedPrice, "1 day");

            carReturn = new CarReturn(123, DateTime.Now, 300);
            calculatedPrice = truck.GetCalculatedRentingPrice(carReturn, DateTime.Now.AddDays(-2), baseDayPrice, baseKilometerPrice);
            Assert.AreEqual(5250, calculatedPrice, "2 days");

            carReturn = new CarReturn(123, DateTime.Now, 300);
            calculatedPrice = truck.GetCalculatedRentingPrice(carReturn, DateTime.Now.AddDays(-5), baseDayPrice, baseKilometerPrice);
            Assert.AreEqual(9750, calculatedPrice, "5 days");

            carReturn = new CarReturn(123, DateTime.Now, 300);
            calculatedPrice = truck.GetCalculatedRentingPrice(carReturn, DateTime.Now.AddDays(-5), baseDayPrice, 7);
            Assert.AreEqual(10650, calculatedPrice, "5 days, baseKilometerPrice = 7");
        }

        [Test]
        public void GetCalculatedRentingPrice_WithNullCarReturn_ShouldThrow()
        {
            var truck = new Truck("abc123", CarType.Truck);

            Assert.Throws(typeof(ArgumentNullException),
                () => truck.GetCalculatedRentingPrice(null, DateTime.Now, 1000));
        }

        [Test]
        public void GetCalculatedRentingPrice_WithNegativeBaseDayPrice_ShouldThrow()
        {
            var truck = new Truck("abc123", CarType.Truck);
            var carReturn = new CarReturn(123, DateTime.Now, 3000);

            Assert.Throws(typeof(ArgumentException),
                () => truck.GetCalculatedRentingPrice(carReturn, DateTime.Now, -1000, 1000));
        }

        [Test]
        public void GetCalculatedRentingPrice_WithNegativeBaseKilometerPrice_ShouldThrow()
        {
            var truck = new Truck("abc123", CarType.Truck);
            var carReturn = new CarReturn(123, DateTime.Now, 3000);

            Assert.Throws(typeof(ArgumentException),
                () => truck.GetCalculatedRentingPrice(carReturn, DateTime.Now, 1000, -1000));
        }

        [Test]
        public void GetCalculatedRentingPrice_WithReturnDateBeforeRentedDate_ShouldThrow()
        {
            var truck = new Truck("abc123", CarType.Truck);
            var carReturn = new CarReturn(123, DateTime.Now, 3000);

            Assert.Throws(typeof(ArgumentException),
                () => truck.GetCalculatedRentingPrice(carReturn, DateTime.Now.AddDays(1), 1000));
        }
    }
}
