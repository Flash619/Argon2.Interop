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

    [Theory]
    [InlineData("$argon2id$v=19$m=16,t=2,p=1$YXBwbGVzYXVjZQ$KP9kKPNlzH5kqC2fI4mnzA", Argon2Version.Nineteen)]
    [InlineData("$argon2id$v=16$m=16,t=2,p=1$YXBwbGVzYXVjZQ$KP9kKPNlzH5kqC2fI4mnzA", Argon2Version.Sixteen)]
    public void CanExtractVersion(string encoded, Argon2Version expectedVersion)
    {
        var argon = new Argon2Interop();
        var version = argon.GetVersion(encoded);
        
        Assert.Equal(expectedVersion, version);
    }

    [Fact]
    public void CannotExtractUnknownVersion()
    {
        var argon = new Argon2Interop();

        Assert.Throws<Argon2Exception>(() => argon.GetVersion("$argon2id$v=0$m=16,t=2,p=1$YXBwbGVzYXVjZQ$KP9kKPNlzH5kqC2fI4mnzA"));
    }

    [Theory]
    [InlineData("$argon2id$v=$m=16,t=2,p=1$YXBwbGVzYXVjZQ$KP9kKPNlzH5kqC2fI4mnzA")]
    [InlineData("$argon2id$$m=16,t=2,p=1$YXBwbGVzYXVjZQ$KP9kKPNlzH5kqC2fI4mnzA")]
    [InlineData("$argon2id$m=16,t=2,p=1$YXBwbGVzYXVjZQ$KP9kKPNlzH5kqC2fI4mnzA")]
    [InlineData("$argon2id$m=1.6,t=2,p=1$YXBwbGVzYXVjZQ$KP9kKPNlzH5kqC2fI4mnzA")]
    [InlineData("$argon2id$m=potato,t=2,p=1$YXBwbGVzYXVjZQ$KP9kKPNlzH5kqC2fI4mnzA")]
    [InlineData("")]
    public void CannotExtractMalformedEncodedString(string encoded)
    {
        var argon = new Argon2Interop();

        Assert.Throws<Argon2Exception>(() => argon.GetVersion(encoded));
    }
    
    [Fact]
    public void RecommendsReHashAppropriately()
    {
        var argon = new Argon2Interop(new Argon2Options()
        {
            Version = Argon2Version.Nineteen
        });
        var encoded = "$argon2id$v=16$m=16,t=2,p=1$YXBwbGVzYXVjZQ$KP9kKPNlzH5kqC2fI4mnzA";

        Assert.True(argon.NeedsReHash(encoded));
        
        encoded = "$argon2id$v=19$m=16,t=2,p=1$YXBwbGVzYXVjZQ$KP9kKPNlzH5kqC2fI4mnzA";
        
        Assert.False(argon.NeedsReHash(encoded));
    }
}