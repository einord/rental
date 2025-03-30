using System;

namespace Biluthyrning.Models;

public record Car
{
    public required Guid Id { get; init; }
    public required string RegistrationNumber { get; init; }
    public required CarTypes CarType { get; init; }
    public required long TotalKilometers { get; init; }
}
