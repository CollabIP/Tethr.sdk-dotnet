namespace Tethr.Sdk.Model;

public class ArrayResponse<T>
{
    public IEnumerable<T> Results { get; set; } = new List<T>();
    
    public static implicit operator ArrayResponse<T>(List<T> array)
    {
        return new ArrayResponse<T> { Results = array };
    }
}