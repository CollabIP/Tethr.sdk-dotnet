namespace Tethr.Sdk.Model;

[Flags]
public enum CategoryResultMode
{
    None = 0, // default Classifications
    Classifications = 1,
    Segments = 2,
    ClassificationsAndSegments = 3
}
