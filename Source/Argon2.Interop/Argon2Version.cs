
namespace Argon2.Interop;

/// <summary>
/// Argon2 versions.
/// </summary>
public enum Argon2Version
{
    /// <summary>
    /// Argon2 v16 - Included only for backwards compatibility.
    /// </summary>
    /// <remarks>
    /// Not recommended for new hashes.
    /// </remarks>
    Sixteen = 16,
    
    /// <summary>
    /// Argon v19 - Recommended for new hashes.
    /// </summary>
    /// <remarks>
    /// The latest, most secure, implementation of Argon2.
    /// </remarks>
    Nineteen = 19
}