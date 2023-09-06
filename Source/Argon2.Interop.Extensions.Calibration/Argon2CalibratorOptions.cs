namespace Argon2.Interop;

/// <summary>
/// Options used to configure the Argon2 calibrator.
/// </summary>
public class Argon2CalibratorOptions
{
    /// <summary>
    /// The maximum memory cost to use during calibration.
    /// </summary>
    public uint MaxMemoryCost { get; init; } = 1024 * 64;
    
    /// <summary>
    /// The minimum memory cost to use during calibration.
    /// </summary>
    public uint MinMemoryCost { get; init; } = 1024 * 19;

    /// <summary>
    /// The maximum time cost to use during calibration.
    /// </summary>
    public uint MaxTimeCost { get; init; } = 99;
    
    /// <summary>
    /// The minimum time cost to use during calibration.
    /// </summary>
    public uint MinTimeCost { get; init; } = 12;

    /// <summary>
    /// The password size in bytes to use during calibration.
    /// </summary>
    public nuint PasswordSize { get; init; } = 64;
    
    /// <summary>
    /// The salt size in bytes to use during calibration.
    /// </summary>
    public nuint SaltSize { get; init; } = 64;

    /// <summary>
    /// The parallelism to use during calibration.
    /// </summary>
    public uint Parallelism { get; init; } = (uint) Environment.ProcessorCount;

    /// <summary>
    /// The hash length to produce during calibration.
    /// </summary>
    public nuint HashLength { get; init; } = 64;
    
    /// <summary>
    /// The maximum Argon2 executions to perform during calibration.
    /// </summary>
    public uint MaxExecutions { get; init; } = 10;

    /// <summary>
    /// The Argon2 type to use during calibration.
    /// </summary>
    public Argon2Type Type { get; init; } = Argon2Type.ID;
    
    /// <summary>
    /// The Argon2 version to use during calibration.
    /// </summary>
    public Argon2Version Version { get; init; } = Argon2Version.Nineteen;
    
    /// <summary>
    /// The maximum duration which Argon2 hash generation should not exceed in order to pass calibration.
    /// </summary>
    public TimeSpan MaxDuration { get; init; } = TimeSpan.FromSeconds(1);

    /// <summary>
    /// Whether to use threaded execution.
    /// </summary>
    /// <remarks>
    /// Threaded execution is designed to simulate a high-load environment and may
    /// provide a more accurate calibration when under load. Threaded execution
    /// will take longer to calibrate, and may result in less secure hashes as
    /// options such as time cost and memory cost may be lower than necessary when
    /// under average or light loads. 
    /// </remarks>
    public bool UseThreadedExecution { get; init; } = true;
    
    /// <summary>
    /// The number of threads to use during threaded execution.
    /// </summary>
    /// <remarks>
    /// Each thread will generate hashes until the execution duration has been exceeded.
    /// </remarks>
    public uint ExecutionThreadCount { get; init; } = (uint) Environment.ProcessorCount;
    
    /// <summary>
    /// The duration for which to generate hashes during threaded execution.
    /// </summary>
    public TimeSpan ExecutionDuration { get; init; } = TimeSpan.FromSeconds(5);
}