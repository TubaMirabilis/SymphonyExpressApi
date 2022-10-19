namespace SymphonyExpressApi.Data;

using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using SymphonyExpressApi.Models;
public class ApplicationDbContext : IdentityDbContext
{
    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
        : base(options)
    {
    }
    public DbSet<Flight> Flights => Set<Flight>();
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);
        modelBuilder.Entity<Flight>().HasData(GenerateFlights());
    }
    List<Flight> GenerateFlights()
    {
        var flights = new List<Flight>();
        var random = new Random();
        DateOnly date1 = DateOnly.FromDateTime(DateTime.Today);
        DateOnly date2 = date1;
        while(date2.CompareTo(date1.AddMonths(18)) <= 0)
        {
            var time = new TimeOnly(6, 45);
            flights.Add(new Flight
            {
                Id = Guid.NewGuid(),
                FlightNumber = 101,
                Origin = "East Midlands",
                Destination = "Glasgow",
                Price = (decimal)random.Next(55, 110),
                Date = date2,
                Time = time
            });
            flights.Add(new Flight
            {
                Id = Guid.NewGuid(),
                FlightNumber = 102,
                Origin = "Glasgow",
                Destination = "East Midlands",
                Price = (decimal)random.Next(55, 110),
                Date = date2,
                Time = time.AddMinutes(105)
            });
            flights.Add(new Flight
            {
                Id = Guid.NewGuid(),
                FlightNumber = 103,
                Origin = "East Midlands",
                Destination = "Edinburgh",
                Price = (decimal)random.Next(55, 110),
                Date = date2,
                Time = time.AddMinutes(210)
            });
            flights.Add(new Flight
            {
                Id = Guid.NewGuid(),
                FlightNumber = 104,
                Origin = "Edinburgh",
                Destination = "East Midlands",
                Price = (decimal)random.Next(55, 110),
                Date = date2,
                Time = time.AddMinutes(315)
            });
            flights.Add(new Flight
            {
                Id = Guid.NewGuid(),
                FlightNumber = 105,
                Origin = "East Midlands",
                Destination = "Glasgow",
                Price = (decimal)random.Next(55, 110),
                Date = date2,
                Time = time.AddMinutes(420)
            });
            flights.Add(new Flight
            {
                Id = Guid.NewGuid(),
                FlightNumber = 106,
                Origin = "Glasgow",
                Destination = "East Midlands",
                Price = (decimal)random.Next(55, 110),
                Date = date2,
                Time = time.AddMinutes(525)
            });
            flights.Add(new Flight
            {
                Id = Guid.NewGuid(),
                FlightNumber = 107,
                Origin = "East Midlands",
                Destination = "Edinburgh",
                Price = (decimal)random.Next(55, 110),
                Date = date2,
                Time = time.AddMinutes(630)
            });
            flights.Add(new Flight
            {
                Id = Guid.NewGuid(),
                FlightNumber = 108,
                Origin = "Edinburgh",
                Destination = "East Midlands",
                Price = (decimal)random.Next(55, 110),
                Date = date2,
                Time = time.AddMinutes(735)
            });
            flights.Add(new Flight
            {
                Id = Guid.NewGuid(),
                FlightNumber = 109,
                Origin = "East Midlands",
                Destination = "Glasgow",
                Price = (decimal)random.Next(55, 110),
                Date = date2,
                Time = time.AddMinutes(840)
            });
            flights.Add(new Flight
            {
                Id = Guid.NewGuid(),
                FlightNumber = 110,
                Origin = "Glasgow",
                Destination = "East Midlands",
                Price = (decimal)random.Next(55, 110),
                Date = date2,
                Time = time.AddMinutes(945)
            });
            date2 = date2.AddDays(1);
        }
        return flights;
    }
}