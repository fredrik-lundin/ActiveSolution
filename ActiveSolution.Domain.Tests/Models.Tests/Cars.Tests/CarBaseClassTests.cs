using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ActiveSolution.Domain.Enums;
using NUnit.Framework.Internal;
using NUnit.Framework;

namespace ActiveSolution.Domain.Tests.Models.Tests.Cars.Tests
{
    [TestFixture]
    public class CarBaseClassTests
    {
        [Test]
        public void Ctor_WithValidParams_ShouldPopulateProperties()
        {
            var car = new MockCar("abc123", CarType.Small, 30000);

            Assert.AreEqual(car.RegistrationNumber, "abc123");
            Assert.AreEqual(car.Type, CarType.Small);
            Assert.AreEqual(car.KilometerDistance, 30000);
        }

        [Test]
        public void Ctor_WithNullOrEmptyRegistrationNumber_ShouldThrow()
        {
            Assert.Throws(typeof(ArgumentNullException), () => new MockCar(null, CarType.Small, 30000));
            Assert.Throws(typeof(ArgumentNullException), () => new MockCar("", CarType.Small, 30000));
            Assert.Throws(typeof(ArgumentNullException), () => new MockCar(" ", CarType.Small, 30000));
        }

        [Test]
        public void Ctor_WithNegativeKilometerDistance_ShouldThrow()
        {
            Assert.Throws(typeof(ArgumentException), () => new MockCar("abc123", CarType.Small, -30000));
        }
    }
}
