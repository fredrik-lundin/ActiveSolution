using System;

namespace ActiveSolution.Domain.Models.Renting
{
    public class CarReturn
    {
        public int BookingNumber { get; }
        public DateTime ReturnedDate { get; }
        public int NewKilometerDistance { get; }

        public CarReturn(int bookingNumber, DateTime returnedDate, int newKilometerDistance)
        {
            if(bookingNumber < 0) throw new ArgumentException($"{nameof(bookingNumber)} cannot be a negative value");
            if (newKilometerDistance < 0) throw new ArgumentException($"{nameof(newKilometerDistance)} cannot be a negative value");

            BookingNumber = bookingNumber;
            ReturnedDate = returnedDate;
            NewKilometerDistance = newKilometerDistance;
        }
    }
}