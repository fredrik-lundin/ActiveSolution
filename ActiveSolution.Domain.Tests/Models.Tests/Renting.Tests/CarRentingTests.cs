using System;
using ActiveSolution.Domain.Enums;
using ActiveSolution.Domain.Models.Cars;
using ActiveSolution.Domain.Models.Renting;
using NUnit.Framework;

namespace ActiveSolution.Domain.Tests.Models.Tests.Renting.Tests
{
    [TestFixture]
    public class CarRentingTests
    {
        private static SmallCar SmallCar => new SmallCar("abc123", CarType.Small, 2500);

        [Test]
        public void Ctor_WithValidParams_ShouldPopulateProperties()
        {
            var now = DateTime.Now;
            var carRenting = new CarRenting(1, SmallCar, "900504", now);

            Assert.AreEqual(carRenting.BookingNumber, 1);
            Assert.AreEqual(carRenting.Car.RegistrationNumber, "abc123");
            Assert.AreEqual(carRenting.RentingDate, now);
        }

        [Test]
        public void Ctor_NegativeBookingNumber_ShouldThrow()
        {
            Assert.Throws(typeof(ArgumentException), () => new CarRenting(-1, SmallCar, "900504", DateTime.Now));
        }

        [Test]
        public void Ctor_NullCar_ShouldThrow()
        {
            Assert.Throws(typeof(ArgumentNullException), () => new CarRenting(1, null, "900504", DateTime.Now));
        }

        [Test]
        public void Ctor_NullOrEmptyRenterId_ShouldThrow()
        {
            Assert.Throws(typeof(ArgumentNullException), () => new CarRenting(1, SmallCar, null, DateTime.Now));
            Assert.Throws(typeof(ArgumentNullException), () => new CarRenting(1, SmallCar, "", DateTime.Now));
            Assert.Throws(typeof(ArgumentNullException), () => new CarRenting(1, SmallCar, " ", DateTime.Now));
        }

        [Test]
        public void ReturnCar_WhenNoCarIsReturnedAndValidCarIsPassed_ShouldReturnCar()
        {
            var carRenting = new CarRenting(1, SmallCar, "900504", DateTime.Now);
            Assert.IsNull(carRenting.CarReturn);

            var carReturn = new CarReturn(1, DateTime.Now, 3000);
            carRenting.ReturnCar(carReturn);

            Assert.IsNotNull(carRenting.CarReturn);
            Assert.AreEqual(carReturn.BookingNumber, carRenting.CarReturn.BookingNumber);
            Assert.AreEqual(carReturn.ReturnedDate, carRenting.CarReturn.ReturnedDate);
            Assert.AreEqual(carReturn.NewKilometerDistance, carRenting.CarReturn.NewKilometerDistance);
        }

        [Test]
        public void ReturnCar_WhenACarIsAlreadyReturned_ShouldThrow()
        {
            var carRenting = new CarRenting(1, SmallCar, "900504", DateTime.Now);
            var carReturn = new CarReturn(1, DateTime.Now, 3000);
            carRenting.ReturnCar(carReturn);

            Assert.IsNotNull(carRenting.CarReturn);

            var carReturn2 = new CarReturn(1, DateTime.Now, 3200);
            Assert.Throws(typeof(InvalidOperationException), () => carRenting.ReturnCar(carReturn2));
        }

        [Test]
        public void ReturnCar_WhenNoCarIsReturnedButNullIsPassed_ShouldThrow()
        {
            var carRenting = new CarRenting(1, SmallCar, "900504", DateTime.Now);

            Assert.Throws(typeof(ArgumentNullException), () => carRenting.ReturnCar(null));
        }
    }
}
