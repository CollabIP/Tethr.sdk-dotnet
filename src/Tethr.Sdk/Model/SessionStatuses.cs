namespace Tethr.Sdk.Model;

public enum SessionStatuses
{
    /// <summary>
    /// Default value, indicates Tethr didn't return a status.
    /// </summary>
    Unknown,
    
    /// <summary>
    /// The call currently in progress.
    /// </summary>
    InProgress,

    /// <summary>
    /// The Call has completed, but we are still processing it.
    /// </summary>
    Concluded,

    /// <summary>
    /// The call has completed, and we are done processing it.
    /// </summary>
    Complete,

    /// <summary>
    /// No call was found for the session.
    /// </summary>
    NotFound,

    /// <summary>
    /// The call was excluded during processing.
    /// </summary>
    Excluded
}