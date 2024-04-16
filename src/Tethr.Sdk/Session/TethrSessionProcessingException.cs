namespace Tethr.Sdk.Session;

/// <summary>
/// Thrown when a critical error occurs within the Tethr SDK, indicating a failure in session processing. This exception highlights severe issues that disrupt normal operation.
/// </summary>
public class TethrSessionProcessingException : Exception
{
    public TethrSessionProcessingException(string message) : base(message)
    {
    }
    
    public TethrSessionProcessingException(Exception exception) : base(exception.Message, exception)
    {
    }
}