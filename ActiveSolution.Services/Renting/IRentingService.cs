using System;

namespace ActiveSolution.Services.Renting
{
    public interface IRentingService
    {
        /// <summary>
        /// Registers a car renting
        /// </summary>
        /// <param name="bookingNumber"></param>
        /// <param name="registrationNumber"></param>
        /// <param name="renterId"></param>
        /// <param name="timeOfRenting"></param>
        /// <param name="kilometerDistance"></param>
        /// <exception cref="InvalidOperationException">If a car with the provided registration number doesn't exist</exception>
        /// <exception cref="InvalidOperationException">If the car with the provided registration number is already rented out</exception>
        /// <exception cref="InvalidOperationException">If the car is already returned</exception>
        /// <exception cref="ArgumentNullException">If any parameter is null</exception>
        /// <exception cref="ArgumentException">If the booking number is a negative value</exception>
        void RentOutCar(int bookingNumber, string registrationNumber, 
            string renterId, DateTime timeOfRenting, int kilometerDistance);

        /// <summary>
        /// Registers a return of a car for a associated booking number
        /// </summary>
        /// <param name="bookingNumber">Needs to be a booking number of an existing car renting</param>
        /// <param name="timeOfReturn"></param>
        /// <param name="newKilometerDistance"></param>
        /// <returns>The calculated price for the renting</returns>
        /// <exception cref="InvalidOperationException">If no car renting exist for the booking number</exception>
        /// <exception cref="InvalidOperationException">If no base pricing exist</exception>
        /// <exception cref="InvalidOperationException">If the car for the booking doesn't exist</exception>
        /// <exception cref="InvalidOperationException">If the car is already returned</exception>
        /// <exception cref="ArgumentException">If newKilometerDistance is less than StartKilometerDistance</exception>
        /// <exception cref="ArgumentException">If returnDate is before renting date</exception>
        decimal ReturnCar(int bookingNumber, DateTime timeOfReturn, int newKilometerDistance);
    }
}