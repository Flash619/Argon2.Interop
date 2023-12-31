﻿using System.Runtime.InteropServices;
using System.Security.Cryptography;
using System.Text;

namespace Argon2.Interop;

/// <summary>
/// Argon2 interop wrapper used to hash and verify passwords using the Argon2 C implementation.
/// </summary>
public class Argon2Interop
{
    private readonly Argon2Options _options;

    /// <summary>
    /// The name of the compiled Argon2 DLL Argon2Interop will attempt to use when communicating with Argon2.
    /// </summary>
    public const string DllName = "libargon2";

    /// <summary>
    /// Creates a new Argon2 wrapper with default option values.
    /// </summary>
    public Argon2Interop() : this(new Argon2Options())
    {
    }
    
    /// <summary>
    /// Creates a new Argon2 wrapper with the options provided.
    /// </summary>
    /// <param name="options">The options.</param>
    public Argon2Interop(Argon2Options options)
    {
        _options = options;
    }

    /// <summary>
    /// Hashes the given password returning an Argon2 encoded string.
    /// </summary>
    /// <remarks>
    /// The password will be converted to UTF-8 bytes prior to hashing.
    /// Salt bytes will automatically be generated with a length equal to that
    /// of the password bytes, or the minimum salt length, whichever is greater.
    /// </remarks>
    /// <param name="password">The password.</param>
    /// <returns>The encoded string.</returns>
    public string Hash(string password)
        => Hash(Encoding.UTF8.GetBytes(password));

    /// <summary>
    /// Hashes the given password with the provided salt returning an Argon2 encoded string.
    /// </summary>
    /// <remarks>
    /// The password and salt will be converted to UTF-8 bytes prior to hashing.
    /// </remarks>
    /// <param name="password">The password.</param>
    /// <param name="salt">The salt.</param>
    /// <returns>The encoded string.</returns>
    public string Hash(string password, string salt)
        => Hash(Encoding.UTF8.GetBytes(password), Encoding.UTF8.GetBytes(salt));

    /// <summary>
    /// Hashes the given password bytes returning an Argon2 encoded string.
    /// </summary>
    /// <remarks>
    /// Salt bytes will automatically be generated with a length equal to that of the password bytes, or
    /// the minimum salt length, whichever is greater.
    /// </remarks>
    /// <param name="password">The password.</param>
    /// <returns>The encoded string.</returns>
    public string Hash(byte[] password)
        => Hash(password, GenerateSalt(password.Length));

    /// <summary>
    /// Hashes the given password bytes using the provided salt bytes returning an Argon2 encoded string.
    /// </summary>
    /// <param name="password">The password bytes.</param>
    /// <param name="salt">The salt bytes.</param>
    /// <returns>The encoded string.</returns>
    public string Hash(byte[] password, byte[] salt)
    {
        Hash(password, salt, out var hash, out var encoded);

        return encoded;
    }

    /// <summary>
    /// Hashes the given password outputting the generated hash bytes and Argon2 encoded string.
    /// </summary>
    /// <remarks>
    /// The password will be converted to UTF-8 bytes prior to hashing.
    /// Salt bytes will automatically be generated with a length equal to that
    /// of the password bytes or the minimum salt length, whichever is greater.
    /// </remarks>
    /// <param name="password">The password.</param>
    /// <param name="hash">The hash bytes.</param>
    /// <param name="encoded">The encoded string.</param>
    public void Hash(string password, out byte[] hash, out string encoded)
    {
        Hash(Encoding.UTF8.GetBytes(password), out var outHash, out var outEncoded);

        hash = outHash;
        encoded = outEncoded;
    }

    /// <summary>
    /// Hashes the given password using the provided salt outputting the generated hash bytes and Argon2 encoded string.
    /// </summary>
    /// <remarks>
    /// The password and salt will be converted to UTF-8 bytes prior to hashing.
    /// </remarks>
    /// <param name="password">The password.</param>
    /// <param name="salt">The salt.</param>
    /// <param name="hash">The hash bytes.</param>
    /// <param name="encoded">The encoded string.</param>
    public void Hash(string password, string salt, out byte[] hash, out string encoded)
    {
        Hash(Encoding.UTF8.GetBytes(password), Encoding.UTF8.GetBytes(salt), out var outHash, out var outEncoded);

        hash = outHash;
        encoded = outEncoded;
    }

    /// <summary>
    /// Hashes the given password bytes outputting the generated hash bytes and Argon2 encoded string.
    /// </summary>
    /// <remarks>
    /// Salt bytes will automatically be generated with a length equal to that of the password bytes,
    /// or the minimum salt length, whichever is greater.
    /// </remarks>
    /// <param name="password">The password bytes.</param>
    /// <param name="hash">The hash bytes.</param>
    /// <param name="encoded">The encoded string.</param>
    public void Hash(byte[] password, out byte[] hash, out string encoded)
    {
        Hash(password, GenerateSalt(Math.Max(Argon2Constants.MinSaltLength, password.Length)), out var outHash, out var outEncoded);

        hash = outHash;
        encoded = outEncoded;
    }

    /// <summary>
    /// Hashes the given password bytes using the provided salt bytes outputting the hash bytes and Argon2 encoded string.
    /// </summary>
    /// <param name="password">The password bytes.</param>
    /// <param name="salt">The salt bytes.</param>
    /// <param name="hash">The hash bytes.</param>
    /// <param name="encoded">The encoded string.</param>
    public void Hash(byte[] password, byte[] salt, out byte[] hash, out string encoded)
    {
        try
        {
            var hashBuffer = new byte[_options.HashLength];
            var encodedLength = Argon2EncodedLength(_options.TimeCost, _options.MemoryCost, _options.Parallelism, (nuint)salt.Length, _options.HashLength, (uint)_options.Type);
            // We are able to determine our expected hash length, however Argon2 requires the encoded array to be padded with at least 1 extra byte. We remove it later on.
            var encodedBuffer = new byte[encodedLength + 1];

            var error = Argon2Hash(
                timeCost: _options.TimeCost,
                memoryCost: _options.MemoryCost,
                parallelism: _options.Parallelism,
                password: password,
                passwordLength: (nuint)password.Length,
                salt: salt,
                saltLength: (nuint)salt.Length,
                hash: hashBuffer,
                hashLength: _options.HashLength,
                encoded: encodedBuffer,
                encodedLength: (nuint)encodedBuffer.Length,
                type: (uint)_options.Type,
                version: (uint)_options.Version);

            CheckArgonResponse(error);

            hash = hashBuffer;
            // Remove padding.
            encoded = Encoding.ASCII.GetString(encodedBuffer.Where(x => x != 0x00).ToArray());
        }
        catch (DllNotFoundException e)
        {
            throw new DllNotFoundException($"The required Argon2 library ({DllName}) is not installed. For usage instructions visit https://github.com/Flash619/Argon2.Interop.", e);
        }
    }

    /// <summary>
    /// Verifies the given password matches the hash within the encoded string provided.
    /// </summary>
    /// <remarks>
    /// The password will be converted to UTF-8 bytes prior to verification. The encoded string
    /// is expected to be a valid Argon2 encoded string, and will be converted to ASCII bytes
    /// prior to verification.
    /// </remarks>
    /// <param name="encoded">The encoded string.</param>
    /// <param name="password">The password.</param>
    /// <returns>Whether verification succeeded.</returns>
    public bool Verify(string encoded, string password)
        => Verify(encoded, Encoding.UTF8.GetBytes(password));
    
    /// <summary>
    /// Verifies the given password bytes matches the hash within the encoded string provided.
    /// </summary>
    /// <remarks>
    /// The encoded string is expected to be a valid Argon2 encoded string, and will be converted
    /// to ASCII bytes prior to verification.
    /// </remarks>
    /// <param name="encoded">The encoded string.</param>
    /// <param name="password">The password bytes.</param>
    /// <returns>Whether verification succeeded.</returns>
    public bool Verify(string encoded, byte[] password)
    {
        var error = Argon2Verify(Encoding.ASCII.GetBytes(encoded), password, (nuint) password.Length, (uint) _options.Type);

        if (error != Argon2Error.None && error != Argon2Error.VerifyMismatch)
        {
            // Fall back to traditional error handling if the error is an actual error other than failed verification.
            CheckArgonResponse(error);
        }
        
        return error == Argon2Error.None;
    }

    /// <summary>
    /// Returns true if the Argon2 encoded string provided was created with an older version
    /// of Argon2 than the interop is configured for. 
    /// </summary>
    /// <remarks>
    /// In example, if the interop is configured to use version 19, and the encoded string was
    /// created with version 16, this method will return true. This method will only return
    /// true if the encoded string version is less than the configured version for the interop.
    /// </remarks>
    /// <param name="encoded">The encoded string.</param>
    /// <returns>Whether a re-hash is needed.</returns>
    public bool NeedsReHash(string encoded)
    {
        return GetVersion(encoded) < _options.Version;
    }

    /// <summary>
    /// Extracts and returns the Argon2 version used to generate an Argon2 encoded string.
    /// </summary>
    /// <remarks>
    /// Argon2 encoded strings all contain the version number of Argon2 which was used to
    /// generate the hash and encoded string. This function works as a helper to extract
    /// the Argon2 version number from the encoded string.
    /// </remarks>
    /// <param name="encoded">The encoded string.</param>
    /// <returns>The version.</returns>
    /// <exception cref="Argon2Exception">There was an error while decoding the encoded string.</exception>
    public Argon2Version GetVersion(string encoded)
    {
        var encodedParts = encoded.Split("$");

        if (encodedParts.Length != 6)
        {
            throw new Argon2Exception("Decoding failed. The encoded string was malformed.", Argon2Error.DecodingFail);
        }

        var versionPart = encodedParts[2];
        var versionParts = versionPart.Split("=");

        if (versionParts.Length != 2 || ! int.TryParse(versionParts[1], out var version) || ! Enum.IsDefined(typeof(Argon2Version), version))
        {
            throw new Argon2Exception("Decoding failed. The encoded string was malformed or the version does not exist.", Argon2Error.DecodingFail);
        }

        return (Argon2Version) version;
    }
    
    [DllImport(DllName, CallingConvention = CallingConvention.Cdecl, EntryPoint = "argon2_encodedlen", CharSet = CharSet.Unicode)]
    private static extern nuint Argon2EncodedLength(uint timeCost, uint memoryCost, uint parallelism, nuint saltLength, nuint hashLength, uint type);
    
    [DllImport(DllName, CallingConvention = CallingConvention.Cdecl, EntryPoint = "argon2_hash", CharSet = CharSet.Unicode)]
    private static extern Argon2Error Argon2Hash(uint timeCost, uint memoryCost, uint parallelism, byte[] password, nuint passwordLength, byte[] salt, nuint saltLength, byte[] hash, nuint hashLength, byte[] encoded, nuint encodedLength, uint type, uint version);

    [DllImport(DllName, CallingConvention = CallingConvention.Cdecl, EntryPoint = "argon2_verify", CharSet = CharSet.Unicode)]
    private static extern Argon2Error Argon2Verify(byte[] encoded, byte[] password, nuint passwordLength, uint type);
    
    [DllImport(DllName, CallingConvention = CallingConvention.Cdecl, EntryPoint = "argon2_error_message", CharSet = CharSet.Unicode)]
    private static extern string Argon2ErrorMessage(int errorCode);

    
    private static byte[] GenerateSalt(int length)
    {
        using var rng = RandomNumberGenerator.Create();
        
        var salt = new byte[length];
        
        rng.GetBytes(salt);

        return salt;
    }

    private static void CheckArgonResponse(Argon2Error error)
    {
        if (error != Argon2Error.None)
        {
            throw new Argon2Exception(Argon2ErrorMessage((int) error), error);
        }
    }
}