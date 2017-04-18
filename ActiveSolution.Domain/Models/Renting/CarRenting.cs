﻿using System;

namespace ActiveSolution.Domain.Models.Renting
{
    public class CarRenting
    {
        public CarRenting(int bookingNumber, string registrationNumber, string renterId, DateTime rentingDate,
            int startKilometerDistance)
        {
            if (bookingNumber < 0) throw new ArgumentException($"{nameof(bookingNumber)} cannot be a negative value");
            if (startKilometerDistance < 0)
                throw new ArgumentException($"{nameof(startKilometerDistance)} cannot be a negative value");
            if (string.IsNullOrWhiteSpace(registrationNumber))
                throw new ArgumentNullException(nameof(registrationNumber));
            if (string.IsNullOrWhiteSpace(renterId)) throw new ArgumentNullException(nameof(renterId));

            BookingNumber = bookingNumber;
            RegistrationNumber = registrationNumber;
            RenterId = renterId;
            RentingDate = rentingDate;
            StartKilometerDistance = startKilometerDistance;
        }

        public int BookingNumber { get; }
        public string RegistrationNumber { get; }
        public string RenterId { get; }
        public DateTime RentingDate { get; }
        public int StartKilometerDistance { get; }
        public DateTime? ReturnDate { get; private set; }
        public int? ReturnKilometerDistance { get; private set; }

        public void ReturnCar(DateTime returnDate, int newKilometerDistance)
        {
            if (newKilometerDistance < 0)
                throw new ArgumentException($"{nameof(newKilometerDistance)} cannot be a negative value");
            if (returnDate < RentingDate)
                throw new ArgumentException($"{nameof(returnDate)} cannot be before {nameof(RentingDate)}");

            ReturnDate = returnDate;
            ReturnKilometerDistance = newKilometerDistance;
        }
    }
}