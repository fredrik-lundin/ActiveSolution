using ActiveSolution.Domain.Enums;
using ActiveSolution.Domain.Models.Cars;

namespace ActiveSolution.Domain.Tests.Models.Tests.Cars.Tests
{
    public class MockCar : Car
    {
        public MockCar(string registrationNumber) : base(registrationNumber)
        {
        }

        public override CarType Type => CarType.Small;
    }
}