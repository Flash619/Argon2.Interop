using System.Runtime.InteropServices;
using System.Security.Cryptography;
using System.Text;

namespace Argon2.Interop;

public class Argon2
{
    private readonly Argon2Options _options;

    public Argon2()
    {
        _options = new Argon2Options();
    }
    
    public Argon2(Argon2Options options)
    {
        _options = options;
    }

    public string Hash(string password)
        => Hash(Encoding.UTF8.GetBytes(password));

    public string Hash(string password, string salt)
        => Hash(Encoding.UTF8.GetBytes(password), Encoding.UTF8.GetBytes(salt));

    public string Hash(byte[] password)
        => Hash(password, GenerateSalt(Math.Max(Argon2Constants.MinSaltLength, password.Length)));

    public string Hash(byte[] password, byte[] salt)
    {
        var hash = new byte[_options.HashLength];
        var encoded = new byte[39 + (password.Length + salt.Length) * 4];
        var error = argon2_hash(
            timeCost: _options.TimeCost, 
            memoryCost: _options.MemoryCost,
            parallelism: _options.Parallelism, 
            password: password, 
            passwordLength: password.Length, 
            salt: salt, 
            saltLength: salt.Length,
            hash: hash, 
            hashLength: _options.HashLength, 
            encoded: encoded,
            encodedLength: encoded.Length, 
            type: _options.Type, 
            version: _options.Version);

        if (error != Argon2Error.None)
        {
            throw new CryptographicException($"Failed to generate Argon2 hash. (Error: {error})");
        }

        return Encoding.ASCII.GetString(encoded);
    }

    [DllImport("libargon2", CallingConvention = CallingConvention.Cdecl, CharSet = CharSet.Unicode)]
    private static extern Argon2Error argon2_hash(int timeCost, int memoryCost, int parallelism, byte[] password, int passwordLength, byte[] salt, int saltLength, byte[] hash,
        int hashLength, byte[] encoded, int encodedLength, Argon2Type type, Argon2Version version);

    private static byte[] GenerateSalt(int length)
    {
        using var rng = RandomNumberGenerator.Create();
        
        var salt = new byte[length];
        
        rng.GetBytes(salt);

        return salt;
    }
}