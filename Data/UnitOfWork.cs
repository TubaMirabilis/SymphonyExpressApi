namespace SymphonyExpressApi.Data;

using SymphonyExpressApi.Models;
public class UnitOfWork : IUnitOfWork
{
    private readonly ApplicationDbContext _context;
    private GenericRepository<Flight>? flightRepository;
    public UnitOfWork(ApplicationDbContext context)
    {
        _context = context;
    }
    public GenericRepository<Flight> FlightRepository
    {
        get
        {
            if (this.flightRepository == null)
            {
                this.flightRepository = new GenericRepository<Flight>(_context);
            }
            return flightRepository;
        }
    }
    // public CommandRepository<Employee> EmployeeCommandRepository
    // {
    //     get
    //     {
    //         if (this.employeeCommandRepository == null)
    //         {
    //             this.employeeCommandRepository = new CommandRepository<Employee>(_context);
    //         }
    //         return employeeCommandRepository;
    //     }
    // }
    public async Task<bool> SaveChangesAsync()
    {
        if (await _context.SaveChangesAsync() > 0)
        {
            return true;
        }
        return false;
    }
}