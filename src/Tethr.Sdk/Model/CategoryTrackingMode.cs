namespace Tethr.Sdk.Model;

[Flags]
public enum CategoryTrackingMode
{
    None = 0,
    Hits = 1,
    Misses = 2,
    HitsAndMisses = 3
}
