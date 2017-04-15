using System;
using ActiveSolution.Domain.Models.Cars;

namespace ActiveSolution.Domain.Models.Renting
{
    public class CarRenting
    {
        public int BookingNumber { get; }
        public Car Car { get; }
        public string RenterId { get; }
        public DateTime RentingDate { get; }
        public CarReturn CarReturn { get; private set; }

        public CarRenting(int bookingNumber, Car car, string renterId, DateTime rentingDate)
        {
            if(bookingNumber < 0) throw new ArgumentException($"{nameof(bookingNumber)} cannot be a negative value");
            if(car == null) throw new ArgumentNullException(nameof(car));
            if(string.IsNullOrWhiteSpace(renterId)) throw new ArgumentNullException(nameof(renterId));

            BookingNumber = bookingNumber;
            Car = car;
            RenterId = renterId;
            RentingDate = rentingDate;
        }

        public void ReturnCar(CarReturn carReturn)
        {
            if(carReturn == null) throw new ArgumentNullException(nameof(carReturn));
            if(CarReturn != null) throw new InvalidOperationException("The car for this renting is already returned");
            CarReturn = carReturn;
        }
    }
}
