using System;
using ActiveSolution.Domain.Enums;
using ActiveSolution.Domain.Models.Renting;

namespace ActiveSolution.Domain.Models.Cars
{
    public class CombiCar : Car
    {
        private const decimal COMBI_PRICE_CALCULATION_FACTOR = 1.3m;

        public CombiCar(string registrationNumber, CarType type, int kilometerDistance = 0) : base(registrationNumber, type, kilometerDistance)
        {
        }

        public override decimal GetCalculatedRentingPrice(CarReturn carReturn, DateTime rentedDate, int baseDayPrice, int baseKilometerPrice = 0)
        {
            var baseCalculatedPrice = base.GetCalculatedRentingPrice(carReturn, rentedDate, baseDayPrice);
            var kilometersTraveled = carReturn.NewKilometerDistance - KilometerDistance;
            var price = baseCalculatedPrice * COMBI_PRICE_CALCULATION_FACTOR + baseKilometerPrice * kilometersTraveled;

            return price;
        }
    }
}
