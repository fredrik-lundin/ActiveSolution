using System;
using ActiveSolution.Domain.Enums;
using NUnit.Framework;

namespace ActiveSolution.Domain.Tests.Models.Tests.Cars.Tests
{
    [TestFixture]
    public class CarBaseClassTests
    {
        [Test]
        public void Ctor_WithNullOrEmptyRegistrationNumber_ShouldThrow()
        {
            Assert.Throws(typeof(ArgumentNullException), () => new MockCar(null));
            Assert.Throws(typeof(ArgumentNullException), () => new MockCar(""));
            Assert.Throws(typeof(ArgumentNullException), () => new MockCar(" "));
        }

        [Test]
        public void Ctor_WithValidParams_ShouldPopulateProperties()
        {
            var car = new MockCar("abc123");

            Assert.AreEqual(car.RegistrationNumber, "abc123");
            Assert.AreEqual(car.Type, CarType.Small);
        }
    }
}