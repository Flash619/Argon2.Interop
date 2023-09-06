namespace Argon2.Interop;

/// <summary>
/// Options to use when generating Argon2 hashes.
/// </summary>
public class Argon2Options
{
    /// <summary>
    /// The time cost (t) Argon2 will use when generating hashes.
    /// </summary>
    /// <remarks>
    /// Defaults to 12. Vulnerabilities exist when set at or less than 10.
    /// </remarks>
    public uint TimeCost { get; init; } = 12U;

    /// <summary>
    /// The memory cost (m) Argon2 will use when generating hashes.
    /// </summary>
    /// <remarks>
    /// Defaults to 19.
    /// </remarks>
    public uint MemoryCost { get; init; } = 1024U * 19U;

    /// <summary>
    /// The parallelism (p) Argon2 will use when generating hashes.
    /// </summary>
    /// <remarks>
    /// Defaults to double the number of logical processors available.
    /// </remarks>
    public uint Parallelism { get; init; } = (uint) Environment.ProcessorCount * 2;

    /// <summary>
    /// The The length of the hash Argon2 should generate.
    /// </summary>
    /// <remarks>
    /// Defaults to 32.
    /// </remarks>
    public nuint HashLength { get; init; } = 32U;

    /// <summary>
    /// The algorithm type Argon2 should use when generating hashes.
    /// </summary>
    /// <remarks>
    /// Defaults to ID.
    /// </remarks>
    public Argon2Type Type { get; init; } = Argon2Type.ID;
    
    /// <summary>
    /// The version of Argon2 to use when generating hashes.
    /// </summary>
    /// <remarks>
    /// Defaults to 19.
    /// </remarks>
    public Argon2Version Version { get; init; } = Argon2Version.Nineteen;

    /// <summary>
    /// Creates a new Argon2Options instance with default values.
    /// </summary>
    public Argon2Options()
    {
    }

    /// <summary>
    /// Creates a new Argon2Options instance, copying the values from the instance provided.
    /// </summary>
    /// <param name="options">The instance.</param>
    public Argon2Options(Argon2Options options)
    {
        TimeCost = options.TimeCost;
        MemoryCost = options.MemoryCost;
        Parallelism = options.Parallelism;
        HashLength = options.HashLength;
        Type = options.Type;
        Version = options.Version;
    }
}