namespace Biluthyrning.Endpoints;

public class Rental
{
    public static RouteGroupBuilder MapEndpoints(RouteGroupBuilder routeGroupBuilder)
    {
        routeGroupBuilder.MapPost("/rent", RentCar).WithName("RentCar");
        routeGroupBuilder.MapPost("/return", ReturnCar).WithName("ReturnCar");

        return routeGroupBuilder;
    }

    private static string RentCar()
    {
        return "Rental endpoint is working!";
    }

    private static string ReturnCar()
    {
        return "Return endpoint is working!";
    }
}