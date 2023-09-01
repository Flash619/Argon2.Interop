namespace Argon2.Interop;

public class Argon2Options
{
    public int TimeCost { get; init; } = 12; // uint32

    public int MemoryCost { get; init; } = 1024 * 64; // uint32

    public int Parallelism { get; init; } = Environment.ProcessorCount * 2; // uint32

    public int HashLength { get; init; } = 32; // size_t

    public Argon2Type Type { get; init; } = Argon2Type.ID; // argon2_type
    public Argon2Version Version { get; init; } = Argon2Version.Nineteen; // argon2_version
}