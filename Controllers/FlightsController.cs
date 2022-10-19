namespace SymphonyExpressApi.Controllers;

using Microsoft.AspNetCore.Mvc;
using SymphonyExpressApi.Data;
using SymphonyExpressApi.Models;
[ApiController]
[Route("api/[controller]")]
public class FlightsController : ControllerBase
{
    private readonly IUnitOfWork _unitOfWork;
    public FlightsController(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }
    public async Task<IActionResult> GetAllFlights()
    {
        var flights = await _unitOfWork.FlightRepository.GetAll();
        return Ok(flights);
    }
}