using Biluthyrning.Models;

namespace Biluthyrning.Endpoints;

public record class RentRequest(string Ssn, CarTypes CarType);

public record class ReturnRequest(string Number, long TotalKilometers);