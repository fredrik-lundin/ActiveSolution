using System;
using ActiveSolution.DataAccess.Repositories;
using ActiveSolution.Domain.Models.Renting;

namespace ActiveSolution.Services.Renting
{
    public class RentingService : IRentingService
    {
        private readonly IRentingRepository _rentingRepository;
        private readonly ICarRepository _carRepository;
        private readonly IPricingRepository _pricingRepository;

        public RentingService(IRentingRepository rentingRepository, ICarRepository carRepository, IPricingRepository pricingRepository)
        {
            if(rentingRepository == null) throw new ArgumentNullException(nameof(rentingRepository));
            if(carRepository == null) throw new ArgumentNullException(nameof(carRepository));
            if(pricingRepository == null) throw new ArgumentNullException(nameof(pricingRepository));

            _rentingRepository = rentingRepository;
            _carRepository = carRepository;
            _pricingRepository = pricingRepository;
        }

        public void RentOutCar(int bookingNumber, string registrationNumber, string renterId, DateTime timeOfRenting,
            int kilometerDistance)
        {
            if (!_carRepository.CarExists(registrationNumber))
                throw new InvalidOperationException($"Car ({registrationNumber}) doesn't exist");
            if (!_rentingRepository.IsCarAvaiableForRenting(registrationNumber))
                throw new InvalidOperationException($"Car ({registrationNumber}) is already rented out");

            var carRenting = new CarRenting(bookingNumber, registrationNumber, renterId, timeOfRenting, kilometerDistance);
            _rentingRepository.SaveOrUpdateCarRenting(carRenting);
        }

        public decimal ReturnCar(int bookingNumber, DateTime timeOfReturn, int newKilometerDistance)
        {
            var carRenting = _rentingRepository.GetCarRenting(bookingNumber);
            if(carRenting == null) throw new InvalidOperationException($"No booking with booking number \"{bookingNumber}\" exists");

            carRenting.ReturnCar(timeOfReturn, newKilometerDistance);
            _rentingRepository.SaveOrUpdateCarRenting(carRenting);

            var basePriceModel = _pricingRepository.GetCurrentBasePricing();
            if (basePriceModel == null) throw new InvalidOperationException("No base prices exists");

            var car = _carRepository.GetCar(carRenting.RegistrationNumber);
            if (car == null) throw new InvalidOperationException($"Car ({carRenting.RegistrationNumber}) associated with the booking doesn't exist");

            return car.GetCalculatedRentingPrice(carRenting, basePriceModel);
        }
    }
}