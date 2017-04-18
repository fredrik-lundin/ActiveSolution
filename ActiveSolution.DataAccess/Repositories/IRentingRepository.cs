using ActiveSolution.Domain.Models.Renting;

namespace ActiveSolution.DataAccess.Repositories
{
    public interface IRentingRepository
    {
        void SaveOrUpdateCarRenting(CarRenting carRenting);
        CarRenting GetCarRenting(int bookingNumber);
        bool IsCarAvaiableForRenting(string registrationNumber);
    }
}