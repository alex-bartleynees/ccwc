using Application;
namespace Tests;

public class UnitTest1
{
    [Fact]
    public void ShouldOutputNumberOfBytesInFile_With_CommandLineOption_C()
    {
        // Arrange
        var args = new[] { "--c", "test.txt" };
        var expected = "Path: Tests/UnitTest1.cs\nSize: 1,000 bytes\n";

        // Act
        var actual = ProcessArgs.Process(args);

        // Assert
        Assert.Equal(expected, actual);
    }
}