using System.Collections.Generic;
using System.Linq;
using ActiveSolution.DataAccess.Repositories;
using ActiveSolution.Domain.Models.Cars;

namespace ActiveSolution.Services.Tests.MockRepositoryImplementations
{
    public class MockCarRepository : ICarRepository
    {
        private IList<Car> Cars { get; }

        public MockCarRepository()
        {
            Cars = new List<Car>();
        }

        public Car GetCar(string registrationNumber) =>
            Cars.SingleOrDefault(c => c.RegistrationNumber == registrationNumber);

        public bool CarExists(string registrationNumber) =>
            GetCar(registrationNumber) != null;

        public void AddCar(Car car) => Cars.Add(car);
    }
}