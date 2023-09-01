namespace Argon2.Interop;

public enum Argon2Error
{
    None = 0,
    OutputPointerNull = -1,
    
    OutputTooShort = -2,
    OutputTooLong = -3,
    
    PasswordTooShort = -4,
    PasswordTooLong = -5,
    
    SaltTooShort = -6,
    SaltTooLong = -7,
    
    AdTooShort = -8,
    AdTooLong = -9,
    
    SecretTooShort = -10,
    SecretTooLong = 11,
    
    TimeTooSmall = -12,
    TimeTooLarge = -13,
    
    MemoryTooLittle = -14,
    MemoryTooMuch = -15,
    
    LanesTooFew = -16,
    LanesTooMany = -17, 
    
    PasswordPointerMismatch = -18,
    SaltPointerMismatch = -19,
    SecretPointerMismatch = -20,
    AdPointerMismatch = -21,
    
    MemoryAllocationError = -22,
    
    FreeMemoryCbkNull = -23,
    AllocateMemoryCbkNull = -24,
    
    IncorrectParameter = -25,
    IncorrectType = -26,
    
    OutputPointerMismatch = -27,
    
    ThreadsTooFew = -28,
    ThreadsTooMan = -29,
    
    MissingArgs = -30,
    
    EncodingFail = -31,
    
    DecodingFail = -32,
    
    ThreadFail = -33, 
    
    DecodingLengthFail = -34,
    
    VerifyMismatch = -35
}