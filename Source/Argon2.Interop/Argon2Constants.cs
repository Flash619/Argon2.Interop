namespace Argon2.Interop;

/// <summary>
/// Argon2 constants based on the C implementation.
/// </summary>
public class Argon2Constants
{
    /// <summary>
    /// The minimum lanes.
    /// </summary>
    public const int MinLanes = 1;
    
    /// <summary>
    /// The minimum threads.
    /// </summary>
    public const int MinThreads = 1;
    
    /// <summary>
    /// The minimum output length.
    /// </summary>
    public const int MinOutputLength = 4;
    
    /// <summary>
    /// The minimum time cost.
    /// </summary>
    public const int MinTimeCost = 1;
    
    /// <summary>
    /// The minimum salt length.
    /// </summary>
    public const int MinSaltLength = 8;
}