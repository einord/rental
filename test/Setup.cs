using Biluthyrning;

namespace Test;

public static class Setup
{
    public static Settings GetDefaultSettings() => new()
    {
        BaseDayPrice = 500,
        BaseKilometerPrice = 10,
    };
}
