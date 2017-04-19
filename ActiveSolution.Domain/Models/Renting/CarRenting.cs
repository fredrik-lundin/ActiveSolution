using System;

namespace ActiveSolution.Domain.Models.Renting
{
    public class CarRenting
    {
        /// <exception cref="ArgumentNullException">If any parameter is null</exception>
        /// <exception cref="ArgumentException">If the booking number is a negative value</exception>
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

        ///<exception cref="InvalidOperationException">If the car is already returned</exception>
        ///<exception cref="ArgumentException">If newKilometerDistance is less than StartKilometerDistance</exception>
        ///<exception cref="ArgumentException">If returnDate is before renting date</exception>
        public void ReturnCar(DateTime returnDate, int newKilometerDistance)
        {
            if(ReturnDate.HasValue)
                throw new InvalidOperationException("The car has already been returned");
            if (newKilometerDistance < StartKilometerDistance)
                throw new ArgumentException($"{nameof(newKilometerDistance)} cannot be less than {nameof(StartKilometerDistance)}");
            if (returnDate < RentingDate)
                throw new ArgumentException($"{nameof(returnDate)} cannot be before {nameof(RentingDate)}");

            ReturnDate = returnDate;
            ReturnKilometerDistance = newKilometerDistance;
        }
    }
}