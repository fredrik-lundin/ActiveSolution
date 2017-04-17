using ActiveSolution.Domain.Enums;

namespace ActiveSolution.Domain.Models.Cars
{
    public class SmallCar : Car
    {
        public SmallCar(string registrationNumber) : base(registrationNumber)
        {
        }

        public override CarType Type => CarType.Small;
    }
}