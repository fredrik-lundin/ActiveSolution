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

        public virtual decimal GetCalculatedRentingPrice(CarRenting carRenting, CarReturn carReturn,
            RentingBasePriceModel pricing)
        {
            if (carReturn == null) throw new ArgumentNullException(nameof(carReturn));
            if (carRenting == null) throw new ArgumentNullException(nameof(carRenting));
            if (pricing == null) throw new ArgumentNullException(nameof(pricing));
            if (carRenting.RentingDate > carReturn.ReturnedDate)
                throw new ArgumentException($"{nameof(carReturn)} return date cannot be before renting date");

            var numberOfDays = (int) (carReturn.ReturnedDate - carRenting.RentingDate).TotalDays;
            numberOfDays = numberOfDays < 1 ? 1 : numberOfDays;

            return numberOfDays*pricing.DayPrice;
        }
    }
}