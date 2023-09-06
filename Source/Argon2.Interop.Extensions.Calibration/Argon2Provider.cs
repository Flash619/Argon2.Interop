namespace Argon2.Interop;

/// <summary>
/// A class which returns a new Argon2Interop instance per request.
/// </summary>
public class Argon2Provider
{
    /// <summary>
    /// Returns a new Argon2Interop instance with the options provided.
    /// </summary>
    /// <param name="options">The options.</param>
    /// <returns>A new Argon2Interop instance.</returns>
    public Argon2Interop GetArgon2(Argon2Options options)
    {
        return new Argon2Interop(options);
    }
}