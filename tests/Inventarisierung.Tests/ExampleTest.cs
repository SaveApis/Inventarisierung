namespace Inventarisierung.Tests;

public class ExampleTest
{
    [Test]
    public async Task Test1()
    {
        // Arrange
        var number = Random.Shared.Next(1, 100);
        var value = $"test-{number}";

        // Assert
        await Assert.That(value).IsEqualTo($"test-{number}");
    }
}
