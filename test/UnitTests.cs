using Biluthyrning.Models;

namespace Test;

public class UnitTests
{
    [Fact]
    public void TestCompact()
    {
        int kmDriven = 100;
        int daysRented = 5;
        Biluthyrning.Settings settings = Setup.GetDefaultSettings();
        MockTimeProvider mockTime = new();

        Biluthyrning.Services.RentalService rentalService = new(settings, mockTime);

        /** Rent first car **/
        mockTime.Now = new DateTime(2025, 01, 01);
        var rentalResponse = rentalService.RentCar(new("123", CarTypes.Compact));
        Assert.NotNull(rentalResponse.AvailableCar);

        // Return first car
        mockTime.Now = new DateTime(2025, 01, daysRented);
        var returnResponse = rentalService.ReturnCar(new("123", rentalResponse.AvailableCar.TotalKilometers + kmDriven));
        Assert.True(returnResponse.Success);

        // Pris = basdygnshyra * antalDygn
        var expectedPrice = settings.BaseDayPrice * daysRented;
        Assert.Equal(expectedPrice, returnResponse.Rental?.Price);

        /** Rent two cars **/
        Assert.NotNull(rentalService.RentCar(new("456", CarTypes.Compact)).AvailableCar);
        Assert.NotNull(rentalService.RentCar(new("789", CarTypes.Compact)).AvailableCar);

        // No more cars should be available
        Assert.Null(rentalService.RentCar(new("951", CarTypes.Compact)).AvailableCar);
    }

    [Fact]
    public void TestSUV()
    {
        int kmDriven = 100;
        int daysRented = 5;
        Biluthyrning.Settings settings = Setup.GetDefaultSettings();
        MockTimeProvider mockTime = new();

        Biluthyrning.Services.RentalService rentalService = new(settings, mockTime);

        /** Rent first car **/
        mockTime.Now = new DateTime(2025, 01, 01);
        var rentalResponse = rentalService.RentCar(new("123", CarTypes.SUV));
        Assert.NotNull(rentalResponse.AvailableCar);

        // Return first car
        mockTime.Now = new DateTime(2025, 01, daysRented);
        var returnResponse = rentalService.ReturnCar(new("123", rentalResponse.AvailableCar.TotalKilometers + kmDriven));
        Assert.True(returnResponse.Success);

        // Pris = basdygnshyra * antalDygn * 1.3 + basKmPris * antalKm
        var expectedPrice = (settings.BaseDayPrice * daysRented * 1.3m) + (settings.BaseKilometerPrice * kmDriven);
        Assert.Equal(expectedPrice, returnResponse.Rental?.Price);

        /** Rent two cars **/
        Assert.NotNull(rentalService.RentCar(new("456", CarTypes.SUV)).AvailableCar);
        Assert.NotNull(rentalService.RentCar(new("789", CarTypes.SUV)).AvailableCar);

        // No more cars should be available
        Assert.Null(rentalService.RentCar(new("951", CarTypes.SUV)).AvailableCar);
    }

    [Fact]
    public void TestTruck()
    {
        int kmDriven = 100;
        int daysRented = 5;
        Biluthyrning.Settings settings = Setup.GetDefaultSettings();
        MockTimeProvider mockTime = new();

        Biluthyrning.Services.RentalService rentalService = new(settings, mockTime);

        /** Rent first car **/
        mockTime.Now = new DateTime(2025, 01, 01);
        var rentalResponse = rentalService.RentCar(new("123", CarTypes.Truck));
        Assert.NotNull(rentalResponse.AvailableCar);

        // Return first car
        mockTime.Now = new DateTime(2025, 01, daysRented);
        var returnResponse = rentalService.ReturnCar(new("123", rentalResponse.AvailableCar.TotalKilometers + kmDriven));
        Assert.True(returnResponse.Success);

        // Pris = basdygnshyra * antalDygn * 1.5 + basKmPris * antalKm * 1.5
        var expectedPrice = (settings.BaseDayPrice * daysRented * 1.5m) + (settings.BaseKilometerPrice * kmDriven * 1.5m);
        Assert.Equal(expectedPrice, returnResponse.Rental?.Price);

        /** Rent two cars **/
        Assert.NotNull(rentalService.RentCar(new("456", CarTypes.Truck)).AvailableCar);
        Assert.NotNull(rentalService.RentCar(new("789", CarTypes.Truck)).AvailableCar);

        // No more cars should be available
        Assert.Null(rentalService.RentCar(new("951", CarTypes.Truck)).AvailableCar);
    }
}
