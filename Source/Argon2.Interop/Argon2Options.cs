namespace Argon2.Interop;

public class Argon2Options
{
    public int TimeCost { get; init; } = 12;

    public int MemoryCost { get; init; } = 1024 * 64;

    public int Parallelism { get; init; } = Environment.ProcessorCount * 2;

    public int HashLength { get; init; } = 32;

    public Argon2Type Type { get; init; } = Argon2Type.ID;
    public Argon2Version Version { get; init; } = Argon2Version.Nineteen;
}