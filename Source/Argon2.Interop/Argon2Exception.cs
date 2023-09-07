using System.Security.Cryptography;

namespace Argon2.Interop;

/// <summary>
/// Indicates an error occured during an Argon2 operation.
/// </summary>
public class Argon2Exception : CryptographicException
{
    /// <summary>
    /// The Argon2 error code that caused the exception.
    /// </summary>
    public readonly Argon2Error Error;

    /// <summary>
    /// Creates a new Argon2Exception instance for the error provided.
    /// </summary>
    /// <param name="error"></param>
    public Argon2Exception(Argon2Error error) : base($"An error occurred while performing the requested operation. Error: {(int) error} ({error})")
    {
        Error = error;
    }
}