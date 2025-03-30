using Biluthyrning.Endpoints;
using Biluthyrning.Models;

namespace Biluthyrning.Services;

public interface IRentalService
{
    RentResponse RentCar(RentRequest rentRequest);
    ReturnResponse ReturnCar(ReturnRequest returnRequest);
}

public class RentalService(Settings settings, ITimeProvider timeProvider) : IRentalService
{
    public RentResponse RentCar(RentRequest rentRequest)
    {
        // Find the next available car
        HashSet<Guid> unavailableCars = [.. Database.Rentals.Values.Where(x => x.ReturnedDate is null).Select(x => x.CarId)];
        Car? availableCar = Database.Cars.Values.FirstOrDefault(x => x.CarType == rentRequest.CarType && !unavailableCars.Contains(x.Id));

        // Create rental post if car was available
        Models.Rental? rental = availableCar is null ? null : new()
        {
            Id = Guid.CreateVersion7(),
            BookingNumber = Guid.CreateVersion7().ToString().Replace("-", string.Empty),
            Ssn = rentRequest.Ssn,
            CarId = availableCar.Id,
            StartDate = timeProvider.Now,
            TotalKilometersBeforeRent = availableCar.TotalKilometers
        };

        // Save rental post to the database (if car was available)
        if (rental is not null)
        {
            Database.Rentals.Add(rental.Id, rental);
        }

        // Return car and rental information (if available)
        return new(availableCar, rental);
    }

    public ReturnResponse ReturnCar(ReturnRequest returnRequest)
    {
        // Find by number
        Car? matchingCar = Database.Cars.Values.FirstOrDefault(x => x.RegistrationNumber == returnRequest.Number);
        Models.Rental? rental = Database.Rentals.Values.Where(x => x.ReturnedDate is null)
            .Where(x => x.Ssn == returnRequest.Number || x.BookingNumber == returnRequest.Number || matchingCar?.Id == x.CarId).FirstOrDefault();
        
        // If no car was found
        if (rental is null)
        {
            return new(false, "Du måste ange ett giltigt personnummer, bokningsnummer eller registreringsnummer", null, null);
        }

        // Make sure the car is correct
        matchingCar = Database.Cars.GetValueOrDefault(rental.CarId);
        if (matchingCar is null)
        {
            throw new Exception("Could not find matching car.");
        }

        // Validate the total kilometers
        if (returnRequest.TotalKilometers < rental.TotalKilometersBeforeRent)
        {
            return new(false, "Totala antalet kilometer kan inte vara lägre än när du lånade bilen.", null, null);
        }

        // If car was found, update the rental object and calculate pricing
        rental = rental with
        {
            ReturnedDate = timeProvider.Now,
            TotalKilometersAfterReturn = returnRequest.TotalKilometers
        };
        int numberOfDays = GetDaysBetweenDates(DateOnly.FromDateTime(rental.StartDate.Date), DateOnly.FromDateTime(rental.ReturnedDate.Value.Date));

        // Validate the number of days
        if (numberOfDays < 1)
        {
            throw new Exception("Total number of days are less than zero (0), unless time travel has been invented this is not possible.");
        }

        decimal price = CalculatePrice(matchingCar.CarType, numberOfDays, rental.TotalKilometersDriven);

        // Update price and save rental to the database
        rental = rental with
        {
            Price = price
        };
        Database.Rentals[rental.Id] = rental;

        return new(true, null, rental, matchingCar);
    }

    private int GetDaysBetweenDates(DateOnly startDate, DateOnly endDate)
        => (endDate.ToDateTime(TimeOnly.MinValue).AddDays(1) - startDate.ToDateTime(TimeOnly.MinValue)).Days;

    private decimal CalculatePrice(CarTypes carType, int numberOfDays, long numberOfKilometers)
        => carType switch
        {
            CarTypes.Compact => settings.BaseDayPrice * numberOfDays,
            CarTypes.SUV => (settings.BaseDayPrice * numberOfDays * 1.3m) + (settings.BaseKilometerPrice * numberOfKilometers),
            CarTypes.Truck => (settings.BaseDayPrice * numberOfDays * 1.5m) + (settings.BaseKilometerPrice * numberOfKilometers * 1.5m),
            _ => throw new NotImplementedException($"Car type {carType} has not been implemented yet.")
        };
}