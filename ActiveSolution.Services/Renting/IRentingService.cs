using System;

namespace ActiveSolution.Services.Renting
{
    public interface IRentingService
    {
        void RentOutCar(int bookingNumber, string registrationNumber,
            string renterId, DateTime timeOfRenting,
            int kilometerDistance);

        decimal ReturnCar(int bookingNumber, DateTime timeOfReturn, int newKilometerDistance);
    }
}