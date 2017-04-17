using System;
using ActiveSolution.Domain.Enums;

namespace ActiveSolution.Services.Renting
{
    internal interface IRentingService
    {
        void RentOutCar(int bookingNumber, string registrationNumber,
            string renterId, CarType carType, DateTime timeOfRenting,
            int kilometerDistance);
    }
}