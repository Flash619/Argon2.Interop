namespace Argon2.Interop;

/// <summary>
/// A result from a single Argon2 execution during calibration.
/// </summary>
/// <remarks>
/// An Argon2 execution represents a hash generating event with a predefined
/// set of Argon2 options and a duration representing the length of time the
/// resulting hash took to generate.
/// </remarks>
/// <param name="Options">The options.</param>
/// <param name="Duration">The duration.</param>
public record Argon2ExecutionResult(Argon2Options Options, TimeSpan Duration)
{
    /// <summary>
    /// Generates a string representation of the execution result using Argon2
    /// style option labels.
    /// </summary>
    /// <returns>The string.</returns>
    public override string ToString()
    {
        return $"{Options}, D: {Duration.TotalSeconds}s";
    }
};