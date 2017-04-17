using System;
using ActiveSolution.Domain.Enums;
using ActiveSolution.Domain.Models.Renting;

namespace ActiveSolution.Domain.Models.Cars
{
    public abstract class Car
    {
        public string RegistrationNumber { get; }
        public CarType Type { get; }
        public int KilometerDistance { get; set; }

        protected Car(string registrationNumber, CarType type, int kilometerDistance = 0)
        {
            if(string.IsNullOrWhiteSpace(registrationNumber)) throw new ArgumentNullException(nameof(registrationNumber));
            if(kilometerDistance < 0) throw new ArgumentException($"{nameof(kilometerDistance)} cannot be a negative value");

            RegistrationNumber = registrationNumber;
            Type = type;
            KilometerDistance = kilometerDistance;
        }

        public virtual decimal GetCalculatedRentingPrice(CarReturn carReturn, DateTime rentedDate, int baseDayPrice, int baseKilometerPrice = 0)
        {
            if(carReturn == null) throw new ArgumentNullException(nameof(carReturn));
            if (rentedDate > carReturn.ReturnedDate) throw new ArgumentException("Return date cannot be before rented date");
            if (baseDayPrice < 0) throw new ArgumentException($"{nameof(baseDayPrice)} cannot be a negative value");
            if (baseKilometerPrice < 0) throw new ArgumentException($"{nameof(baseKilometerPrice)} cannot be a negative value");

            var numberOfDays = (int)(carReturn.ReturnedDate - rentedDate).TotalDays;
            numberOfDays = numberOfDays < 1 ? 1 : numberOfDays;

            return numberOfDays * baseDayPrice;
        }
    }
}
