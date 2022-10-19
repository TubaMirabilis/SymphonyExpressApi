namespace SymphonyExpressApi.Data;

using SymphonyExpressApi.Models;
public interface IUnitOfWork
{
    GenericRepository<Flight> FlightRepository { get; }
    Task<bool> SaveChangesAsync();
}