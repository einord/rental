using System;
using Biluthyrning.Models;

namespace Biluthyrning.Services;

public static class Database
{
    private const int NumberOfCarsPerType = 2;
    private static readonly char[] _availableLetters = ['A', 'B', 'C', 'D', 'E', 'F', 'G', 'H', 'I', 'J', 'K', 'L', 'M', 'N', 'O', 'P', 'Q', 'R', 'S', 'T', 'U', 'V', 'W', 'X', 'Y', 'Z'];
    private static readonly char[] _availableNumbers = ['0', '1', '2', '3', '4', '5', '6', '7', '8', '9'];

    /// <summary>
    /// All cars in the database.
    /// </summary>
    public static Dictionary<Guid, Car> Cars {get; } = [];

    /// <summary>
    /// All rentals in the database.
    /// </summary>
    public static Dictionary<Guid, Rental> Rentals {get; } = [];

    static Database()
    {
        // Generate random cars
        foreach (var carType in Enum.GetValues<CarTypes>())
        {
            for (int i = 0; i < NumberOfCarsPerType; i++)
            {
                var carId = Guid.CreateVersion7();
                Cars[carId] = new()
                {
                    Id = carId,
                    RegistrationNumber = GetRandomRegistrationNumber(),
                    CarType = (CarTypes)carType,
                    TotalKilometers = new Random().Next(100_000, 1_000_000)
                };
            }
        }
    }

    /// <summary>
    /// Returns a random car registration number.
    /// </summary>
    private static string GetRandomRegistrationNumber()
        => string.Join(string.Empty, [
            _availableLetters[new Random().Next(0, _availableLetters.Length)],
            _availableLetters[new Random().Next(0, _availableLetters.Length)],
            _availableLetters[new Random().Next(0, _availableLetters.Length)],
            ' ',
            _availableNumbers[new Random().Next(0, _availableNumbers.Length)],
            _availableNumbers[new Random().Next(0, _availableNumbers.Length)],
            _availableNumbers[new Random().Next(0, _availableNumbers.Length)]
        ]);
}
