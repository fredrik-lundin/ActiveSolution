using System;
using ActiveSolution.Domain.Models.Cars;
using ActiveSolution.Domain.Models.Renting;
using NUnit.Framework;

namespace ActiveSolution.Domain.Tests.Models.Tests.Cars.Tests
{
    [TestFixture]
    public class SmallCarTests
    {
        [Test]
        public void GetCalculatedRentingPrice_WithNullBasePricing_ShouldThrow()
        {
            var smallCar = new SmallCar("abc123");
            var carReturn = new CarReturn(123, DateTime.Now, 3000);

            Assert.Throws(typeof(ArgumentNullException),
                () => smallCar.GetCalculatedRentingPrice(null, carReturn, null));
        }

        [Test]
        public void GetCalculatedRentingPrice_WithNullCarReturn_ShouldThrow()
        {
            var smallCar = new SmallCar("abc123");
            var carRenting = new CarRenting(123, "abc123", "900504", DateTime.Now, 0);

            Assert.Throws(typeof(ArgumentNullException),
                () => smallCar.GetCalculatedRentingPrice(carRenting, null, new RentingBasePriceModel()), "carRented");
        }

        [Test]
        public void GetCalculatedRentingPrice_WithReturnDateBeforeRentedDate_ShouldThrow()
        {
            var smallCar = new SmallCar("abc123");
            var carRenting = new CarRenting(123, "abc123", "900504", DateTime.Now.AddMinutes(1), 0);
            var carReturn = new CarReturn(123, DateTime.Now, 3000);

            Assert.Throws(typeof(ArgumentException),
                () => smallCar.GetCalculatedRentingPrice(carRenting, carReturn, new RentingBasePriceModel()));
        }

        [Test]
        public void GetCalculatedRentingPrice_WithValidParams_ShouldCalculateCorrectPrices()
        {
            var smallCar = new SmallCar("abc123");

            var carRenting = new CarRenting(123, "abc123", "900504", DateTime.Now, 0);
            var carReturn = new CarReturn(123, DateTime.Now.AddMinutes(1), 300);
            var pricing = new RentingBasePriceModel(1000);
            var calculatedPrice = smallCar.GetCalculatedRentingPrice(carRenting, carReturn, pricing);
            Assert.AreEqual(1000, calculatedPrice, "Same day");

            carRenting = new CarRenting(123, "abc123", "900504", DateTime.Now, 0);
            carReturn = new CarReturn(123, DateTime.Now.AddMinutes(1), 300);
            pricing = new RentingBasePriceModel(2000);
            calculatedPrice = smallCar.GetCalculatedRentingPrice(carRenting, carReturn, pricing);
            Assert.AreEqual(2000, calculatedPrice, "Same day, baseDayPrice = 2000");

            carRenting = new CarRenting(123, "abc123", "900504", DateTime.Now, 0);
            carReturn = new CarReturn(123, DateTime.Now.AddDays(1), 300);
            pricing = new RentingBasePriceModel(1000);
            calculatedPrice = smallCar.GetCalculatedRentingPrice(carRenting, carReturn, pricing);
            Assert.AreEqual(1000, calculatedPrice, "1 day");

            carRenting = new CarRenting(123, "abc123", "900504", DateTime.Now, 0);
            carReturn = new CarReturn(123, DateTime.Now.AddDays(2), 300);
            pricing = new RentingBasePriceModel(1000);
            calculatedPrice = smallCar.GetCalculatedRentingPrice(carRenting, carReturn, pricing);
            Assert.AreEqual(2000, calculatedPrice, "2 days");

            carRenting = new CarRenting(123, "abc123", "900504", DateTime.Now, 0);
            carReturn = new CarReturn(123, DateTime.Now.AddDays(5), 300);
            pricing = new RentingBasePriceModel(1000);
            calculatedPrice = smallCar.GetCalculatedRentingPrice(carRenting, carReturn, pricing);
            Assert.AreEqual(5000, calculatedPrice, "5 days");
        }
    }
}