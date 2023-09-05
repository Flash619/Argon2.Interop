# Argon2 Interop
An interop allowing for Argon2 hashing in .NET Core using the [Argon2 C implementation](https://github.com/P-H-C/phc-winner-argon2).

**This library is still in early development. More documentation will be released as time permits.**

## How to Use
To use Argon2 Interop you will need to install the Argon2.Interop nuget package, as well as ensure the proper DLL is available at runtime based on your runtime architecture.

1. Install the `Argon2.Interop` nuget package.
2. Use the `Argon2Interop` class from the `Argon2.Interop` namespace to generate hashes.
3. Place a precompiled `libargon2` DLL from the `Assets` directory next to your processes executable.

### Which DLL to Use
The DLL you choose to use should be based on your runtime architecture. A simple table has been created below.

| Operating System | x86              | x64              | arm64                |
|------------------|------------------|------------------|----------------------|
| Linux            | x86/libargon.so  | x64/libargon.so  |                      |
| MacOS            |                  |                  | arm64/libargon.dylib |
| Windows          | x86/libargon.dll | x64/libargon.dll |                      |

**Several architectures are not available pre-compiled.** This is due to my own limitations as I do not have all
hardware available on which to compile libargon. **If your desired architecture is not available** you may compile
libargon yourself, it has been included as a submodule under `Source/External/phc-winner-argon2/` for your convenience.

## Options
All common Argon2 options are available and their default values reflect that of a secure configuration. In most
cases options should not need to be adjusted. Default option values can be located within the [Argon2Options](https://github.com/Flash619/Argon2Interop/blob/main/Source/Argon2.Interop/Argon2Options.cs) class.

## Usage Examples

### Password Hashing
Under normal use, excluding extraordinary circumstances or requirements, the default option values are secure enough for
most use cases. The following is all you typically will need.

**Hashing a password:**
```c#
var argon = new Argon2Interop();
var hash = argon2.Hash("ilikecheese"); // Returnes an encoded hashed password.
```

### Verifying a Password
To verify a password, pass both the encoded password hash and the password to compare it against to the `Verify` function.

```c#
var argon = new Argon2Interop();

if (! argon.Verify(encoded, "ilikecheese")) {
    throw new InvalidPasswordException();
}
```

### Custom Options
Custom options can be passed to the `Argon2` constructor.

**Using custom options:**
```c#
var argon = new Argon2Interop(new Argon2InteropOptions() {
    MemoryCost = 1024 * 64,
    TimeCost = 24,
    Parallelism = 6,
    HashLength = 128,
    Type = Argon2Type.D,
    Version = Argon2Version.Nineteen
    });

var hash = argon2.Hash("ilikecheese");
```

### Hashing to Bytes
In certain circumstances you may wish to work with the hash bytes directly. Hash bytes can be obtained by using the `Hash`
overload that outputs both the hash bytes as well as the encoded hash string.

**Hashing to bytes:**
```c#
var argon = new Argon2Interop();
argon2.Hash("ilikecheese", out var hash, out var encoded); // Outputs hash bytes as well as an encoded hash string.
```

### Custom Salts
Custom salts can be passed, though they are not necessarily required. By default a random cryptographically secured salt
will be generated based on the password length.

**Using custom salts:**
```c#
var argon = new Argon2Interop();
argon2.Hash("ilikecheese", "qwe123!@#");
```