namespace Argon2.Interop;

public class Argon2Options
{
    public uint TimeCost { get; init; } = 12;

    public uint MemoryCost { get; init; } = 1024 * 64;

    public uint Parallelism { get; init; } = (uint) Environment.ProcessorCount * 2;

    public nuint HashLength { get; init; } = 32;

    public Argon2Type Type { get; init; } = Argon2Type.ID;
    public Argon2Version Version { get; init; } = Argon2Version.Nineteen;
}