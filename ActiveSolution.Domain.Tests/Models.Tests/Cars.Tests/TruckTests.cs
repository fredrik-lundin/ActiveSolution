using System;
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
        public void GetCalculatedRentingPrice_WithNullBasePricing_ShouldThrow()
        {
            var truck = new Truck("abc123");
            var carRenting = new CarRenting(123, "abc123", "900504", DateTime.Now, 0);
            var carReturn = new CarReturn(123, DateTime.Now, 3000);

            Assert.Throws(typeof(ArgumentNullException),
                () => truck.GetCalculatedRentingPrice(carRenting, carReturn, null));
        }

        [Test]
        public void GetCalculatedRentingPrice_WithNullCarRenting_ShouldThrow()
        {
            var truck = new Truck("abc123");
            var carReturn = new CarReturn(123, DateTime.Now, 3000);

            Assert.Throws(typeof(ArgumentNullException),
                () => truck.GetCalculatedRentingPrice(null, carReturn, new RentingBasePriceModel()), "carRented");
        }

        [Test]
        public void GetCalculatedRentingPrice_WithNullCarReturn_ShouldThrow()
        {
            var truck = new Truck("abc123");
            var carRenting = new CarRenting(123, "abc123", "900504", DateTime.Now, 0);

            Assert.Throws(typeof(ArgumentNullException),
                () => truck.GetCalculatedRentingPrice(carRenting, null, new RentingBasePriceModel()), "carRented");
        }

        [Test]
        public void GetCalculatedRentingPrice_WithReturnDateBeforeRentedDate_ShouldThrow()
        {
            var truck = new Truck("abc123");
            var carRenting = new CarRenting(123, "abc123", "900504", DateTime.Now.AddMinutes(1), 0);
            var carReturn = new CarReturn(123, DateTime.Now, 3000);

            Assert.Throws(typeof(ArgumentException),
                () => truck.GetCalculatedRentingPrice(carRenting, carReturn, new RentingBasePriceModel()));
        }

        [Test]
        public void GetCalculatedRentingPrice_WithValidParams_ShouldCalculateCorrectPrices()
        {
            var truck = new Truck("abc123");
            var carRenting = new CarRenting(123, "abc123", "900504", DateTime.Now, 0);
            var carReturn = new CarReturn(123, DateTime.Now.AddMinutes(1), 300);
            var pricing = new RentingBasePriceModel(1000, 5);
            var calculatedPrice = truck.GetCalculatedRentingPrice(carRenting, carReturn, pricing);
            Assert.AreEqual(3750, calculatedPrice, "Same day");

            carRenting = new CarRenting(123, "abc123", "900504", DateTime.Now, 0);
            carReturn = new CarReturn(123, DateTime.Now.AddMinutes(1), 300);
            pricing = new RentingBasePriceModel(2000, 5);
            calculatedPrice = truck.GetCalculatedRentingPrice(carRenting, carReturn, pricing);
            Assert.AreEqual(5250, calculatedPrice, "Same day, baseDayPrice = 2000");

            carRenting = new CarRenting(123, "abc123", "900504", DateTime.Now, 0);
            carReturn = new CarReturn(123, DateTime.Now.AddDays(1), 300);
            pricing = new RentingBasePriceModel(1000, 5);
            calculatedPrice = truck.GetCalculatedRentingPrice(carRenting, carReturn, pricing);
            Assert.AreEqual(3750, calculatedPrice, "1 day");

            carRenting = new CarRenting(123, "abc123", "900504", DateTime.Now, 0);
            carReturn = new CarReturn(123, DateTime.Now.AddDays(2), 300);
            pricing = new RentingBasePriceModel(1000, 5);
            calculatedPrice = truck.GetCalculatedRentingPrice(carRenting, carReturn, pricing);
            Assert.AreEqual(5250, calculatedPrice, "2 days");

            carRenting = new CarRenting(123, "abc123", "900504", DateTime.Now, 0);
            carReturn = new CarReturn(123, DateTime.Now.AddDays(5), 300);
            pricing = new RentingBasePriceModel(1000, 5);
            calculatedPrice = truck.GetCalculatedRentingPrice(carRenting, carReturn, pricing);
            Assert.AreEqual(9750, calculatedPrice, "5 days");

            carRenting = new CarRenting(123, "abc123", "900504", DateTime.Now, 0);
            carReturn = new CarReturn(123, DateTime.Now.AddDays(5), 300);
            pricing = new RentingBasePriceModel(1000, 7);
            calculatedPrice = truck.GetCalculatedRentingPrice(carRenting, carReturn, pricing);
            Assert.AreEqual(10650, calculatedPrice, "5 days, baseKilometerPrice = 7");
        }

        [Test]
        public void Truck_ShouldAlwayReturnCarTypeOfTruck()
        {
            var combiCar = new Truck("abc123");
            Assert.AreEqual(CarType.Truck, combiCar.Type);
        }
    }
}