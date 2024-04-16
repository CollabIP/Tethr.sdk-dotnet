namespace Tethr.Sdk.Model;

public enum InteractionPurgeStatus
{
    /// <summary>
    /// An error occurred (see supplied error message).
    /// </summary>
    Error,
		
    /// <summary>
    /// No interactions where not found that match the specified criteria, this will also happen if the interaction has already been purged.
    /// </summary>
    NotFound,
		
    /// <summary>
    /// The call is in the process of being purged.
    /// </summary>
    Purging,
		
    /// <summary>
    /// If WhatIf is specified, the interaction will not be purged, but the status will be returned as WhatIf.
    /// </summary>
    WhatIf
}