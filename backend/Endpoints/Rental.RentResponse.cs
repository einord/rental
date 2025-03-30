using Biluthyrning.Models;

namespace Biluthyrning.Endpoints;

public record class RentResponse(Car? AvailableCar, Models.Rental? Rental);

public record class ReturnResponse(bool Success, string? Message, Models.Rental? Rental, Car? Car);