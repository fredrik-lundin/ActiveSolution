using System;
using ActiveSolution.Domain.Models.Cars;
using ActiveSolution.Domain.Models.Renting;
using ActiveSolution.Services.Renting;
using ActiveSolution.Services.Tests.MockRepositoryImplementations;
using NUnit.Framework;
using NUnit.Framework.Internal;

namespace ActiveSolution.Services.Tests.Renting.Tests
{
    [TestFixture]
    public class RentingServiceTests
    {
        private MockRentingRepository _rentingRepository;
        private MockCarRepository _carRepository;
        private MockPricingRepository _pricingRepository;

        private RentingService _rentingService;

        [SetUp]
        public void InitDependencies()
        {
            _rentingRepository = new MockRentingRepository();
            _carRepository = new MockCarRepository();
            _pricingRepository = new MockPricingRepository();
            _rentingService = new RentingService(_rentingRepository, _carRepository, _pricingRepository);
        }

        [Test]
        public void Ctor_WithAnyParameterAsNull_ShouldThrow()
        {
            Assert.Throws(typeof(ArgumentNullException),
                () => new RentingService(null, _carRepository, _pricingRepository));
            Assert.Throws(typeof(ArgumentNullException),
                () => new RentingService(_rentingRepository, null, _pricingRepository));
            Assert.Throws(typeof(ArgumentNullException),
                () => new RentingService(_rentingRepository, _carRepository, null));
        }

        [Test]
        public void Ctor_WithValidParamters_ShouldNotThrow()
        {
            Assert.DoesNotThrow(() => new RentingService(_rentingRepository, _carRepository, _pricingRepository));
        }


        [Test]
        public void RentOutCar_ForACarThatDoesntExist_ShouldThrow()
        {
            Assert.Throws(typeof(InvalidOperationException), () =>
                    _rentingService.RentOutCar(1, "abc123", "900504", DateTime.Now, 0));
        }

        [Test]
        public void RentOutCar_ForACarThatIsAlreadyRentedOut_ShouldThrow()
        {
            _carRepository.AddCar(new SmallCar("abc123"));
            _rentingRepository.SaveOrUpdateCarRenting(new CarRenting(1, "abc123", "900504", DateTime.Now, 0));

            Assert.Throws(typeof(InvalidOperationException), () =>
                    _rentingService.RentOutCar(1, "abc123", "900504", DateTime.Now, 0));
        }

        [Test]
        public void RentOutCar_WithValidParameters_ShouldAddACarRenting()
        {
            _carRepository.AddCar(new SmallCar("abc123"));

            Assert.IsNull(_rentingRepository.GetCarRenting(1));
            _rentingService.RentOutCar(1, "abc123", "900504", DateTime.Now, 0);
            Assert.IsNotNull(_rentingRepository.GetCarRenting(1));
        }

        [Test]
        public void RenturnCar_ForABookingNumberThatDoesntHaveABooking_ShouldThrow()
        {
            _pricingRepository.SetCurrentBasePricing(new RentingBasePriceModel());

            Assert.Throws(typeof(InvalidOperationException), () =>
                    _rentingService.ReturnCar(1, DateTime.Now.AddDays(1), 10));
        }

        [Test]
        public void RenturnCar_WhenNoBasePricingIsAvailable_ShouldThrow()
        {
            _carRepository.AddCar(new SmallCar("abc123"));
            _rentingService.RentOutCar(1, "abc123", "900504", DateTime.Now, 0);

            Assert.Throws(typeof(InvalidOperationException), () =>
                    _rentingService.ReturnCar(1, DateTime.Now.AddDays(1), 10));
        }

        [Test]
        public void RenturnCar_ForACarThatDoesntExistShouldThrow()
        {
            _pricingRepository.SetCurrentBasePricing(new RentingBasePriceModel());
            _rentingRepository.SaveOrUpdateCarRenting(new CarRenting(1, "abc123", "900504", DateTime.Now, 0));

            Assert.Throws(typeof(InvalidOperationException), () =>
                    _rentingService.ReturnCar(1, DateTime.Now.AddDays(1), 10));
        }

        [Test]
        public void RenturnCar_WithValidParameters_ShouldUpdateCarRentingCorrectly()
        {
            _carRepository.AddCar(new SmallCar("abc123"));
            _pricingRepository.SetCurrentBasePricing(new RentingBasePriceModel());
            _rentingService.RentOutCar(1, "abc123", "900504", DateTime.Now, 0);

            var carRentingNotReturned = _rentingRepository.GetCarRenting(1);
            Assert.IsNull(carRentingNotReturned.ReturnKilometerDistance);
            Assert.IsNull(carRentingNotReturned.ReturnDate);

            _rentingService.ReturnCar(1, DateTime.Now, 10);
            var carRentingReturned = _rentingRepository.GetCarRenting(1);
            Assert.IsNotNull(carRentingReturned.ReturnKilometerDistance);
            Assert.IsNotNull(carRentingReturned.ReturnDate);
        }
    }
}
