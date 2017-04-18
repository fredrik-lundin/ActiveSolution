using ActiveSolution.Domain.Models.Cars;

namespace ActiveSolution.DataAccess.Repositories
{
    public interface ICarRepository
    {
        Car GetCar(string registrationNumber);
        bool CarExists(string registrationNumber);
    }
}