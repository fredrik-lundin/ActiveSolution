using System;
using ActiveSolution.Domain.Enums;
using ActiveSolution.Domain.Models.Renting;

namespace ActiveSolution.Domain.Models.Cars
{
    public abstract class Car
    {
        protected Car(string registrationNumber)
        {
            if (string.IsNullOrWhiteSpace(registrationNumber))
                throw new ArgumentNullException(nameof(registrationNumber));

            RegistrationNumber = registrationNumber;
        }

        public string RegistrationNumber { get; }
        public abstract CarType Type { get; }

        public virtual decimal GetCalculatedRentingPrice(CarRenting carRenting, RentingBasePriceModel pricing)
        {
            if (carRenting == null) throw new ArgumentNullException(nameof(carRenting));
            if (!carRenting.ReturnDate.HasValue) throw new InvalidOperationException("Cannot calulate price until the car is returned");
            if (!carRenting.ReturnKilometerDistance.HasValue) throw new InvalidOperationException("Cannot calulate price until the car is returned");
            if (pricing == null) throw new ArgumentNullException(nameof(pricing));

            var numberOfDays = (int) (carRenting.ReturnDate.Value - carRenting.RentingDate).TotalDays;
            numberOfDays = numberOfDays < 1 ? 1 : numberOfDays;

            return numberOfDays*pricing.DayPrice;
        }
    }
}