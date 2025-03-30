using System;

namespace Biluthyrning.Models;

public record Rental
{
    public required Guid Id { get; init; }
    public required string BookingNumber { get; init; }
    public required string Ssn { get; init; }
    public required Guid CarId { get; init; }
    public required DateTimeOffset StartDate { get; init; }
    public DateTimeOffset? ReturnedDate { get; init; }
    public long TotalKilometersBeforeRent { get; init; }
    public long? TotalKilometersAfterReturn { get; init; }
    public long TotalKilometersDriven => (TotalKilometersAfterReturn ?? TotalKilometersBeforeRent) - TotalKilometersBeforeRent;
    public decimal? Price { get; init; }
}
