namespace Argon2.Interop;

/// <summary>
/// Argon2 errors based on the C implementation.
/// </summary>
public enum Argon2Error
{
    /// <summary>
    /// Indicates no error is present.
    /// </summary>
    None = 0,
    
    /// <summary>
    /// Indicates the pointer for the Argon2 hash output was null.
    /// </summary>
    OutputPointerNull = -1,
    
    /// <summary>
    /// Indicates the Argon2 hash output length provided was too short.
    /// </summary>
    OutputTooShort = -2,
    
    /// <summary>
    /// Indicates the Argon2 hash output length provided was too long.
    /// </summary>
    OutputTooLong = -3,
    
    /// <summary>
    /// Indicates the password was too short.
    /// </summary>
    PasswordTooShort = -4,
    
    /// <summary>
    /// Indicates the password was too long.
    /// </summary>
    PasswordTooLong = -5,
    
    /// <summary>
    /// Indicates the salt was too short.
    /// </summary>
    SaltTooShort = -6,
    
    /// <summary>
    /// Indicates the salt was too long.
    /// </summary>
    SaltTooLong = -7,
        
    /// <summary>
    /// Indicates the associated data was too short.
    /// </summary>
    AdTooShort = -8,
    
    /// <summary>
    /// Indicates the associated data was too long.
    /// </summary>
    AdTooLong = -9,
    
    /// <summary>
    /// Indicates the secret was too short.
    /// </summary>
    SecretTooShort = -10,
    
    /// <summary>
    /// Indicates the secret was too long.
    /// </summary>
    SecretTooLong = 11,
    
    /// <summary>
    /// Indicates the time cost was too small.
    /// </summary>
    TimeTooSmall = -12,
    
    /// <summary>
    /// Indicates the time cost was too large.
    /// </summary>
    TimeTooLarge = -13,
    
    /// <summary>
    /// Indicates the memory cost was too small.
    /// </summary>
    MemoryTooLittle = -14,
    
    /// <summary>
    /// Indicates the memory cost was too large.
    /// </summary>
    MemoryTooMuch = -15,
        
    /// <summary>
    /// Indicates there was too few lanes.
    /// </summary>
    LanesTooFew = -16,
    
    /// <summary>
    /// Indicates there was too many lanes.
    /// </summary>
    LanesTooMany = -17, 
    
    /// <summary>
    /// Indicates the pointer provided for the password was NULL but password length was not 0.
    /// </summary>
    PasswordPointerMismatch = -18,
    
    /// <summary>
    /// Indicates the pointer provided for the salt was NULL but salt length was not 0.
    /// </summary>
    SaltPointerMismatch = -19,
    
    /// <summary>
    /// Indicates the pointer provided for the secret was NULL but secret length was not 0.
    /// </summary>
    SecretPointerMismatch = -20,
    AdPointerMismatch = -21,
    
    /// <summary>
    /// Indicates there was an error while allocating memory.
    /// </summary>
    MemoryAllocationError = -22,
        
    /// <summary>
    /// Indicates the free memory callback was null.
    /// </summary>
    FreeMemoryCbkNull = -23,
    
    /// <summary>
    /// Indicates the allocate memory callback was null.
    /// </summary>
    AllocateMemoryCbkNull = -24,
    
    /// <summary>
    /// Indicates a parameter was incorrect.
    /// </summary>
    IncorrectParameter = -25,
    
    /// <summary>
    /// Indicates the Argon2 type provided was unknown.
    /// </summary>
    IncorrectType = -26,
    
    /// <summary>
    /// Indicates the Argon2 hash output pointer was incorrect.
    /// </summary>
    OutputPointerMismatch = -27,
        
    /// <summary>
    /// Indicates there was not enough threads.
    /// </summary>
    ThreadsTooFew = -28,
    
    /// <summary>
    /// Indicates there was too many threads.
    /// </summary>
    ThreadsTooMan = -29,
    
    /// <summary>
    /// Indicates required arguments were missing.
    /// </summary>
    MissingArgs = -30,
    
    /// <summary>
    /// Indicates there was an error while encoding the Argon2 encoded hash string.
    /// </summary>
    EncodingFail = -31,
    
    /// <summary>
    /// Indicates there was an error while decoding the Argon2 encoded hash string.
    /// </summary>
    DecodingFail = -32,
    
    /// <summary>
    /// Indicates there was an error related to threading.
    /// </summary>
    ThreadFail = -33, 
        
    /// <summary>
    /// Indicates some encoded parameters were too long or too short.
    /// </summary>
    DecodingLengthFail = -34,
    
    /// <summary>
    /// Indicates hash verification failed.
    /// </summary>
    VerifyMismatch = -35
}