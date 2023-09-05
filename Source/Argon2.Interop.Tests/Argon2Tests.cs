using Xunit;

namespace Argon2.Interop.Tests;

public class Argon2Tests
{
    [Fact]
    public void CanHash()
    {
        var argon = new Argon2Interop();

        var hash = argon.Hash("ilikecheese");
        
        Assert.NotNull(hash);
    }

    [Fact]
    public void HashesCanBeVerified()
    {
        var argon = new Argon2Interop();

        var hash = argon.Hash("ilikecheese");
        
        Assert.True(argon.Verify(hash, "ilikecheese"));
    }

    [Fact]
    public void CanNotVerifyIncorrectPassword()
    {
        var argon = new Argon2Interop();

        var hash = argon.Hash("ilikecheese");
        
        Assert.False(argon.Verify(hash, "idontlikecheese"));
    }
}