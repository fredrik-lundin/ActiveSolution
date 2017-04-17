using ActiveSolution.Domain.Enums;
using ActiveSolution.Domain.Models.Renting;

namespace ActiveSolution.Domain.Models.Cars
{
    public class Truck : Car
    {
        private readonly decimal TRUCK_PRICE_CALCULATION_FACTOR = 1.5m;

        public Truck(string registrationNumber) : base(registrationNumber)
        {
        }

        public override CarType Type => CarType.Truck;

        public override decimal GetCalculatedRentingPrice(CarRenting carRenting, CarReturn carReturn,
            RentingBasePriceModel pricing)
        {
            var baseCalculatedPrice = base.GetCalculatedRentingPrice(carRenting, carReturn, pricing);
            var kilometersTraveled = carReturn.NewKilometerDistance - carRenting.StartKilometerDistance;
            var price = baseCalculatedPrice*TRUCK_PRICE_CALCULATION_FACTOR +
                        pricing.KilometerPrice*kilometersTraveled*TRUCK_PRICE_CALCULATION_FACTOR;

            return price;
        }
    }
}