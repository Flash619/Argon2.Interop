using Xunit;

namespace Argon2.Interop.Tests;

public class Argon2Tests
{
    [Fact]
    public void CanHash()
    {
        var argon = new Argon2();

        var hash = argon.Hash("ilikecheese");
        
        Console.WriteLine(hash);
    }
}