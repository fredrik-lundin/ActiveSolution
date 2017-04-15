using ActiveSolution.Domain.Enums;

namespace ActiveSolution.Domain.Models.Cars
{
    public class SmallCar: Car
    {
        public SmallCar(string registrationNumber, CarType type, int kilometerDistance = 0) : base(registrationNumber, type, kilometerDistance)
        {
        }
    }
}
