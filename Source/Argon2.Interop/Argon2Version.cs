
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
    /// Vulnerable to single-pass Argon2i function using between a quarter and a fifth of the desired space with no time penalty, and compute a multiple-pass Argon2i using only
    /// N/e (≈ N/2.72) space with no time penalty. <a href="https://en.wikipedia.org/wiki/Argon2">Reference</a>
    /// </remarks>
    Sixteen = 16,
    
    /// <summary>
    /// Argon v19 - Recommended for new hashes.
    /// </summary>
    /// <remarks>
    /// Vulnerable to Argon2i attack where Argon2i can be computed by an algorithm which has complexity O(n7/4 log(n)) for all choices of parameters σ (space cost), τ (time cost),
    /// and thread-count such that n=σ∗τ when passes (time cost) is at or less than 10. <a href="https://en.wikipedia.org/wiki/Argon2">Reference</a>
    /// </remarks>
    Nineteen = 19,
    
    /// <summary>
    /// The latest Argon2 version, currently <see cref="Nineteen"/>.
    /// </summary>
    Latest = Nineteen
}