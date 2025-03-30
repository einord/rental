using Biluthyrning.Services;
using Microsoft.AspNetCore.Mvc;

namespace Biluthyrning.Endpoints;

public static class Rental
{
    public static RouteGroupBuilder MapEndpoints(RouteGroupBuilder routeGroupBuilder)
    {
        routeGroupBuilder.MapPost("/rent", RentCar).WithName("RentCar");
        routeGroupBuilder.MapPost("/return", ReturnCar).WithName("ReturnCar");

        return routeGroupBuilder;
    }

    /// <summary>
    /// Rent a car based on the provided request data.
    /// <para>If no car is available, null values will be returned.</para>
    /// </summary>
    private static RentResponse RentCar(IRentalService rentalService, [FromBody] RentRequest rentRequest)
    {
        // Validate the request data
        if (string.IsNullOrWhiteSpace(rentRequest.Ssn) || string.IsNullOrWhiteSpace(rentRequest.CarType.ToString()))
        {
            return new(null, null);
        }

        return rentalService.RentCar(rentRequest);
    }

    private static ReturnResponse ReturnCar(IRentalService rentalService, [FromBody] ReturnRequest returnRequest)
        => rentalService.ReturnCar(returnRequest);
}