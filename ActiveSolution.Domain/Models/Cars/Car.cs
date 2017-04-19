using System;
using ActiveSolution.Domain.Enums;
using ActiveSolution.Domain.Models.Renting;

namespace ActiveSolution.Domain.Models.Cars
{
    public abstract class Car
    {
        /// <exception cref="ArgumentNullException">If registration number is null</exception>
        protected Car(string registrationNumber)
        {
            if (string.IsNullOrWhiteSpace(registrationNumber))
                throw new ArgumentNullException(nameof(registrationNumber));

            RegistrationNumber = registrationNumber;
        }

        public string RegistrationNumber { get; }
        public abstract CarType Type { get; }


        /// <exception cref="ArgumentNullException">If any argument is null</exception>
        /// <exception cref="InvalidOperationException">If car is not returned before this method is invoked</exception>
        public virtual decimal GetCalculatedRentingPrice(CarRenting carRenting, RentingBasePriceModel pricing)
        {
            if (carRenting == null) throw new ArgumentNullException(nameof(carRenting));
            if (!carRenting.ReturnDate.HasValue || !carRenting.ReturnKilometerDistance.HasValue)
                throw new InvalidOperationException("Cannot calculate price until the car is returned");
            if (pricing == null) throw new ArgumentNullException(nameof(pricing));

            var numberOfDays = (int) (carRenting.ReturnDate.Value - carRenting.RentingDate).TotalDays;
            numberOfDays = numberOfDays < 1 ? 1 : numberOfDays;

            return numberOfDays*pricing.DayPrice;
        }
    }
}