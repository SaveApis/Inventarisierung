namespace Inventarisierung.Tests;

public class ExampleTest
{
    [Test]
    public async Task Test1()
    {
        // Arrange
        var value = "test";

        // Assert
        await Assert.That(value).IsEqualTo("test");
    }
}
