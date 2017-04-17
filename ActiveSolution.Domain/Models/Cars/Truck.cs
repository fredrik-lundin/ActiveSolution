using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ActiveSolution.Domain.Enums;
using ActiveSolution.Domain.Models.Renting;

namespace ActiveSolution.Domain.Models.Cars
{
    public class Truck : Car
    {
        private decimal TRUCK_PRICE_CALCULATION_FACTOR = 1.5m;

        public Truck(string registrationNumber, CarType type, int kilometerDistance = 0) : base(registrationNumber, type, kilometerDistance)
        {
        }

        public override decimal GetCalculatedRentingPrice(CarReturn carReturn, DateTime rentedDate, int baseDayPrice, int baseKilometerPrice = 0)
        {
            var baseCalculatedPrice = base.GetCalculatedRentingPrice(carReturn, rentedDate, baseDayPrice, baseKilometerPrice);
            var kilometersTraveled = carReturn.NewKilometerDistance - KilometerDistance;
            var price = baseCalculatedPrice * TRUCK_PRICE_CALCULATION_FACTOR + baseKilometerPrice * kilometersTraveled * TRUCK_PRICE_CALCULATION_FACTOR;

            return price;
        }
    }
}
