namespace SymphonyExpressApi.Models;
public class Flight : Entity
{
    public int FlightNumber { get; set; }
    public string ATCCallsign { get => "SMX" + this.FlightNumber; }
    public string Origin { get; set; }
    public string Destination { get; set; }
    public decimal Price { get; set; }
    public DateOnly Date { get; set; }
    public TimeOnly Time { get; set; }
    /*
    public ICollection<Passenger> PassengerList { get; set; }
    public Flight()
    {
        this.PassengerList = new List<Passenger>();
    }
    */
}