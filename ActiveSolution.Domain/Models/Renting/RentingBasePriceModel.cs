using System;

namespace ActiveSolution.Domain.Models.Renting
{
    public class RentingBasePriceModel
    {
        public RentingBasePriceModel(int dayPrice = 0, int kilometerPrice = 0)
        {
            if (dayPrice < 0) throw new ArgumentException($"{nameof(dayPrice)} cannot be a negative value");
            if (kilometerPrice < 0) throw new ArgumentException($"{nameof(kilometerPrice)} cannot be a negative value");

            DayPrice = dayPrice;
            KilometerPrice = kilometerPrice;
        }

        public int DayPrice { get; }
        public int KilometerPrice { get; }
    }
}