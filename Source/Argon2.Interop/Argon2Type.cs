namespace Argon2.Interop;

/// <summary>
/// Argon2 algorithm types.
/// </summary>
public enum Argon2Type
{
    /// <summary>
    /// Argon2d - Provides strong resistance from GPU attacks but might be vulnerable to side channel attacks
    /// under special circumstances.
    /// </summary>
    D = 0,
    
    /// <summary>
    /// Argon2i - Prevents side channel attacks but is more vulnerable to GPU based attacks.
    /// </summary>
    I = 1,
    
    /// <summary>
    /// Argon2id - Both Argon2i and Argon2d will be used for maximum protection against GPU and side channel attacks.
    /// </summary>
    ID = 2
}