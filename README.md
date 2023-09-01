# Argon2 Interop
An interop allowing for Argon2 hashing in .NET Core.

**This library is still in early development. More documentation will be released soon.**

## How to use:

1. Install the `Argon2.Interop` nuget package.
2. Use the `Argon2` class to generate hashes.
3. Place a precompiled `libargon2` DLL in your projects bin folder. (This can be done as a post-compile script, examples coming soon!)
4. Run your process.
5. Profit!