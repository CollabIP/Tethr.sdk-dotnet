namespace Tethr.Sdk.Model
{
    public enum CallDirection
    {
        /// <summary>
        /// The direction is invalid, it must be set to something else.
        /// </summary>
        /// <remarks>
        /// This is placeholder for the default value, and should not be used.
        /// </remarks>
        Invalid,
        
        /// <summary>
        /// The direction is not known
        /// </summary>
        Unknown,

        /// <summary>
        /// The call originated from outside of the telephony system
        /// </summary>
        Inbound,

        /// <summary>
        /// The call originated from something inside the telephony system, and is dialing an outside line.
        /// </summary>
        Outbound,

        /// <summary>
        /// The call originated from something inside the telephony system, and is dialing another device inside the telephony system
        /// </summary>
        Internal
    }
}