using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ActiveSolution.Domain.Enums;
using ActiveSolution.Domain.Models.Cars;

namespace ActiveSolution.Domain.Tests.Models.Tests.Cars.Tests
{
    public class MockCar: Car
    {
        public MockCar(string registrationNumber, CarType type, int kilometerDistance = 0) : base(registrationNumber, type, kilometerDistance)
        {
        }
    }
}
