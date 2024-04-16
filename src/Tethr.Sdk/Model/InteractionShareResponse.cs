namespace Tethr.Sdk.Model;

public class InteractionShareResponse
{
    /// <summary>
    /// The time of expiration of the call link.
    /// </summary>
    public DateTime Expiration { get; set; }

    /// <summary>
    /// The call link that gives the guest user with the specified email access to the call.
    /// </summary>
    public string ShareUri { get; set; } = string.Empty;
}