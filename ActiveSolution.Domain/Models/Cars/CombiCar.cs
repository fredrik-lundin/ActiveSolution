using ActiveSolution.Domain.Enums;
using ActiveSolution.Domain.Models.Renting;

namespace ActiveSolution.Domain.Models.Cars
{
    public class CombiCar : Car
    {
        private const decimal COMBI_PRICE_CALCULATION_FACTOR = 1.3m;

        public CombiCar(string registrationNumber) : base(registrationNumber)
        {
        }

        public override CarType Type => CarType.Combi;

        public override decimal GetCalculatedRentingPrice(CarRenting carRenting, CarReturn carReturn,
            RentingBasePriceModel pricing)
        {
            var baseCalculatedPrice = base.GetCalculatedRentingPrice(carRenting, carReturn, pricing);
            var kilometersTraveled = carReturn.NewKilometerDistance - carRenting.StartKilometerDistance;
            var price = baseCalculatedPrice*COMBI_PRICE_CALCULATION_FACTOR + pricing.KilometerPrice*kilometersTraveled;

            return price;
        }
    }
}