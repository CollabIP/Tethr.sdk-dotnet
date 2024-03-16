namespace Tethr.Sdk.Session;

internal class TokenResponse
{
    // leaving access token as a string as there is no security gained from anything else and only 
    // slows down the calls.  In normal use cases this is used often for the lifetime of the Token, 
    // meaning that string is in clear text the entire time it's valid anyway.
    public string? AccessToken;

    public string? TokenType;

    public long ExpiresInSeconds;

    public DateTime CreatedTimeStamp { get; set; }

    public bool IsValid => CreatedTimeStamp + TimeSpan.FromSeconds(ExpiresInSeconds - 45) > DateTime.Now;
}