using System.Collections.Generic;
using System.Linq;
using ActiveSolution.DataAccess.Repositories;
using ActiveSolution.Domain.Models.Renting;

namespace ActiveSolution.Services.Tests.MockRepositoryImplementations
{
    public class MockRentingRepository : IRentingRepository
    {
        private IList<CarRenting> CarRentings { get; }

        public MockRentingRepository()
        {
            CarRentings = new List<CarRenting>();
        }

        public void SaveOrUpdateCarRenting(CarRenting carRenting)
        {
            var existingCarRenting = CarRentings.FirstOrDefault(r => r.BookingNumber == carRenting.BookingNumber);
            if (existingCarRenting == null)
            {
                CarRentings.Add(carRenting);
                return;
            }

            var index = CarRentings.IndexOf(existingCarRenting);
            CarRentings[index] = carRenting;
        }

        public CarRenting GetCarRenting(int bookingNumber) =>
            CarRentings.FirstOrDefault(r => r.BookingNumber == bookingNumber);

        public bool IsCarAvaiableForRenting(string registrationNumber) =>
            CarRentings.FirstOrDefault(r => r.RegistrationNumber == registrationNumber &&
                                            !r.ReturnKilometerDistance.HasValue) == null;
    }
}