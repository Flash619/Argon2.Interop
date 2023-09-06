using System.Diagnostics;

namespace Argon2.Interop;

/// <summary>
/// A basic Argon2 executor which will execute Argon2 once per request.
/// </summary>
public class Argon2Executor
{
    protected readonly Argon2Provider Provider;

    /// <summary>
    /// Creates a new Argon2Executor instance using the Argon2Provider provided.
    /// </summary>
    /// <param name="provider">The provider</param>
    public Argon2Executor(Argon2Provider provider)
    {
        Provider = provider;
    }

    /// <summary>
    /// Executes Argon2 returning a duration representing the total time Argon2 took to generate a hash with the options provided.
    /// </summary>
    /// <param name="password">The password.</param>
    /// <param name="salt">The salt.</param>
    /// <param name="options">The options.</param>
    /// <returns>The duration.</returns>
    public virtual TimeSpan Execute(byte[] password, byte[] salt, Argon2Options options)
    {
        var stopwatch = Stopwatch.StartNew();
        
         Provider.GetArgon2(options).Hash(password, salt);
        
        stopwatch.Stop();

        return stopwatch.Elapsed;
    }
}