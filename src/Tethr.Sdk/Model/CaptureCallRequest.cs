using System.ComponentModel.DataAnnotations;
using System.Diagnostics;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace Tethr.Sdk.Model;

public class CaptureCallRequest : ITethrMetadata
{
    private DateTime _utcStart;
    private DateTime? _utcEnd;

    /// <summary>
    /// (Required) The sessionID is unique identify per recording.
    /// </summary>
    /// <remarks>
    /// Session ID needs to be unique to any and all recordings uploaded to a given instance of Tethr
    /// 
    /// When not used in conjunction with a MasterId, Session ID should be something 
    /// that can be used by other systems to connect a call in Tethr to the same call in other systems.
    /// 
    /// When a MasterId is provided, the Master ID can be used link a Tethr call to other systems.
    /// But the SessionID should still be something that the originating call recorder can use to look up
    /// a given recording with.  This allows for the recorder to validate that Tethr did in fact get a call
    /// as part of a regular integrity or verification process.
    /// </remarks>
    [Required]
    public string? SessionId { get; set; }

    /// <summary>
    /// A unique identifier that may be shared between one or more calls, that together represent one complete call.
    /// </summary>
    /// <remarks>
    /// Some call records will split a call into multiple calls when the caller is transferred or another participant is added.
    /// This master ID can be used to tell Tethr that a set of calls are really one call.
    /// 
    /// This is actually a good thing in many cases, it allows us to treat each part of the call as own call but still be able
    /// to link what happened in a prior call to an event in this call, or even a future call.
    /// </remarks>
    public string? MasterId { get; set; }

    /// <summary>
    /// The ID to use to create composite cases that do NOT treat the group as a single composite interaction for the purposes of analysis
    /// </summary>
    public string? CaseReferenceId { get; set; }

    /// <summary>
    /// The ID to use to create composite interactions that do NOT treat the group as a single correlated interaction for the purposes of analysis
    /// </summary>
    public string? CollectionId { get; set; }

    /// <summary>
    /// The start time of the call, this value should be in UTC.
    /// </summary>
    public DateTime UtcStart
    {
        get => _utcStart;
        set
        {
            if (value.Kind == DateTimeKind.Local)
                _utcStart = value.ToUniversalTime();
            else
            {
                Debug.Assert(value.Kind == DateTimeKind.Utc,
                    "Start time kind is Unspecified, this could result in incorrect times showing up in Tethr");
                _utcStart = value;
            }
        }
    }

    /// <summary>
    /// The end time of the call, this value should be in UTC.
    /// </summary>
    /// <remarks>
    /// End time is optional, if not provided Tethr will set the runtime based on the length of the audio.
    /// </remarks>
    public DateTime? UtcEnd
    {
        get => _utcEnd;
        set
        {
            if (value?.Kind == DateTimeKind.Local)
                _utcEnd = value.Value.ToUniversalTime();
            else if (value == null)
                _utcEnd = null;
            else
            {
                Debug.Assert(value?.Kind == DateTimeKind.Utc,
                    "End time kind is Unspecified, this could result in incorrect times showing up in Tethr");
                _utcEnd = value.Value;
            }
        }
    }

    /// <summary>
    /// A list of contacts that participated in the call.
    /// </summary>
    [JsonPropertyName("Participant")]
    public List<CaptureCallContact> Participants { get; set; } = new();

    /// <summary>
    /// (Optional) The direction the call was originated
    /// </summary>
    /// <remarks>
    /// This value MUST be one of the following or null, or the server will reject the request with a 400 status code.
    /// <para>
    /// Valid values:
    /// </para><para>
    /// - Unknown = The direction is not known.</para><para>
    /// - InBound = The call originated from outside the telephony system.</para><para>
    /// - Outbound = The call originated from something inside the telephony system, and is dialing an outside line.</para><para>
    /// - Internal = The call originated from something inside the telephony system, and is dialing another device inside the telephony system.</para><para>
    /// If the value is null, it will be assumed to be "Unknown".
    /// </para><para>
    /// The definitions are a loose outline and can be adapted to the customers needs, but should be consistent.
    /// </para>
    /// </remarks>
    public InteractionDirection Direction { get; set; }

    /// <summary>
    /// (Optional) The number that was dialed that initiated this call.
    /// </summary>
    /// <remarks>
    /// This is primarily used with inbound calls, where the number dialed is a trunk line and any number
    /// of internal devices could pick up the call.  It's particularly useful when combined with special
    /// promotions where each promotion is given it's own phone number.
    /// 
    /// Where the number dialed is a Direct Inward Dialing of a single station or an outbound number
    /// this value is best left null or empty.
    /// </remarks>
    public string? NumberDialed { get; set; }

    /// <summary>
    /// Information about the type of Audio that is being uploaded.
    /// </summary>
    /// <remarks>
    /// In a future release, when sending Archived Recordings the media type of the audio part is used
    /// and this property is ignored.
    /// 
    /// This property will still be used in Live Call processing, and with some out of band audio feeds.
    /// </remarks>
    // TODO: There is no corresponding element on the web service. Should this be [JsonIgnore]?
    public Audio Audio { get; set; } = new();

    /// <summary>
    /// Any extra metadata related to the call that could be used by Tethr.
    /// </summary>
    /// <remarks>
    /// The metadata is read by Tethr and used classify calls, Examples:
    /// Contact Center the call was answered in, the disposition code entered by the agent,
    /// the skill group / or product the call was related to, the call routing group from the IVR,
    /// an identifier for phone the agent used to make the call.
    /// 
    /// There is really no limit to what can be put in there, as long as it can be converted to JSON.
    /// </remarks>
    public JsonElement Metadata { get; set; }
}