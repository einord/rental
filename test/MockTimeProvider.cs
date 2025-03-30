using Biluthyrning;

namespace Test;

public class MockTimeProvider : ITimeProvider
{
    public DateTime Now { get; set; } = DateTime.Now;
}