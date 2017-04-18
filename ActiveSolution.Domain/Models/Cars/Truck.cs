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

        public override decimal GetCalculatedRentingPrice(CarRenting carRenting,
            RentingBasePriceModel pricing)
        {
            var baseCalculatedPrice = base.GetCalculatedRentingPrice(carRenting, pricing);
            var kilometersTraveled = carRenting.ReturnKilometerDistance.Value - carRenting.StartKilometerDistance;
            var price = baseCalculatedPrice*TRUCK_PRICE_CALCULATION_FACTOR +
                        pricing.KilometerPrice*kilometersTraveled*TRUCK_PRICE_CALCULATION_FACTOR;

            return price;
        }
    }
}
