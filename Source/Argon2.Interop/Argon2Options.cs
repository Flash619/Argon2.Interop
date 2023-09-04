namespace Argon2.Interop;

public class Argon2Options
{
    public uint TimeCost { get; init; } = 12U;

    public uint MemoryCost { get; init; } = 1024U * 64U;

    public uint Parallelism { get; init; } = (uint) Environment.ProcessorCount * 2;

    public nuint HashLength { get; init; } = 32U;

    public Argon2Type Type { get; init; } = Argon2Type.ID;
    public Argon2Version Version { get; init; } = Argon2Version.Nineteen;
}