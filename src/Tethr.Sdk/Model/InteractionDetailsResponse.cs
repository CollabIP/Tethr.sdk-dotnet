using System.Text.Json;

namespace Tethr.Sdk.Model;

public class InteractionDetailsResponse
	{
		/// <summary>
		/// The Tethr Integration ID, or Flow ID if this has a InteractionType of Flow.
		/// </summary>
		public string? Id { get; set; }

		/// <summary>
		/// The Session Id from the source system.
		/// </summary>
		public string? SessionId { get; set; }

		/// <summary>
		/// An ID that may be shared between one or more interaction, that together represent one complete call.
		/// </summary>
		/// <remarks>
		/// Some call records will split a call into multiple calls when the caller is transferred or another participant is added.
		/// This master call ID can be used to tell Tethr that a set of call are really one call.
		///
		/// This is actually a good thing in many cases, it allows us to treated each part of the call as own call, but still be able
		/// to link what happened in prior call to an event in this call, or even a future call.
		/// </remarks>
		public string? MasterId { get; set; }

		/// <summary>
		/// The Case Id this interaction is associated with.
		/// </summary>
		public string? CaseReferenceId { get; set; }

		/// <summary>
		/// The Collection ID this interaction is associated with.
		/// </summary>
		public string? CollectionId { get; set; }

		/// <summary>
		/// The 3 letter language code (ISO 639-3 Alpha-3 Code) that this interaction is most likely/predominantly going to contain.
		/// </summary>
		/// <see cref="http://www-01.sil.org/iso639-3/codes.asp"/>
		public string? Language { get; set; }

		/// <summary>
		/// The 3 letter country code (ISO 3166 ALPHA-3 Code) of the dialect most likely spoken on this interaction.
		/// </summary>
		/// <see cref="http://www.iso.org/iso/home/standards/country_codes.htm"/>
		/// <see cref="http://www.nationsonline.org/oneworld/country_code_list.htm"/>
		public string? Country { get; set; }

		/// <summary>
		/// The Tethr Web UI URI for this interaction.
		/// </summary>
		/// <remarks>
		/// This URI can be used in a WebBrowser to open the Archive for this interactions in Tethr.
		/// a query string parameter of “transcript” with a value of the time in the interaction in milliseconds (example https://demo.tethr.io/calls/bbqfbbbow?transcript=15610)
		/// </remarks>
		public string? TethrUri { get; set; }

		/// <summary>
		/// The start time of the interaction, in UTC time.
		/// </summary>
		public DateTime UtcStart { get; set; }

		/// <summary>
		/// The end time of the interaction, in UTC time.
		/// </summary>
		public DateTime UtcEnd { get; set; }

		/// <summary>
		/// The duration of the interaction in milliseconds.
		/// </summary>
		public int DurationMs { get; set; }

		/// <summary>
		/// The duration of the mixed audio file of the interaction in milliseconds.
		/// </summary>	
		public int AudioDurationMs { get; set; }

		/// <summary>
		/// The display name for the interaction based on it's custom groups in Tethr.
		/// </summary>
		public string? DisplayName { get; set; }

		/// <summary>
		/// Details about the custom groups assigned to this interaction in Tethr.
		/// </summary>
		public List<CallGroup> CustomGroups { get; set; } = new();

		/// <summary>
		/// A dynamic JSON object that contains all the extra metadata send along with the interaction, from the originating audio ingestion system.
		/// </summary>
		public JsonElement Metadata { get; set; }

		/// <summary>
		/// Any Classifications associated to this interaction by Tethr, not specific to any participant.
		/// </summary>
		public List<CallClassification> Classifications { get; set; } = new();

		/// <summary>
		/// Any custom text field hits associated to this interaction by Tethr.
		/// </summary>
		public List<CallCustomTextHit> TextHits { get; set; } = new();

		/// <summary>
		/// The participants that took part in the interaction. Typically these are a company employee/agent and a customer.
		/// </summary>
		public List<InteractionParticipant> Participants { get; set; } = new();

		/// <summary>
		/// The phone number that was dialed that started the call.
		/// </summary>
		public string? NumberDialed { get; set; }

		/// <summary>
		/// The direction of the interactions, is primarily used for calls
		/// </summary>
		/// <remarks>
		///	can be inbound (e.g. customer calls the agent), outbound (e.g. agent calls the customer), or internal (e.g. agent talking with supervisor).
		/// </remarks>
		public InteractionDirection Direction { get; set; }

		/// <summary>
		/// <para>
		/// Confidence of participant type match algorithms run after the diarization processor. Null if diarization processor was not run.
		/// </para><para>
		/// For calls that have been analyzed by the diarization processor,
		/// we have an extra process that attempts to match the participant to the audio.
		/// This value is the confidence value from that process on a 0 to 1 scale, with 1 being absolutely certain.
		///</para>
		/// <para>
		/// If the diarization processor was not run on the audio, this value will be null.
		/// </para><para>
		/// Values:
		/// </para><para>
		///  0 = No confidence.
		/// </para><para>0.4 = Looked at who spoke first
		/// </para><para>0.6 = Have one participant on spoken words
		/// </para><para>0.8 = Have two participant on spoken words
		/// </para><para>1 = A human has looked at it.</para>
		/// </summary>
		public float? ParticipantIdentificationConfidence { get; set; }

		/// <summary>
		/// The call was Diarization run on the audio (e.g. the call was mono, and was split into multiple audio streams, one for each participant).
		/// </summary>
		public bool HadDiarization { get; set; }

		/// <summary>
		/// The interaction type. Will be Flow if this is a composite call or case (flow).
		/// </summary>
		public CallType InteractionType { get; set; }

		/// <summary>
		/// The Flow type id. Only available if this is a flow.
		/// </summary>	
		public string? FlowTypeId { get; set; }

		/// <summary>
		/// The flows this interaction is a member of.
		/// </summary>
		public List<CallFlow> Flows { get; set; } = new();

		/// <summary>
		/// Custom fields defined as text data type
		/// </summary>
		/// <remarks>
		/// Text type custom fields are used for free-form text inputs
		/// </remarks>
		public List<CustomFieldString> CustomTexts { get; set; } = new();

		/// <summary>
		/// Custom fields defined as string or keyword lookup data type
		/// </summary>
		/// <remarks>
		/// keyword lookup custom fields are used for values that are used as lookup fields.
		/// This would be things like Account Number, or Product Sku where the same value is
		/// used on multiple interactions.
		/// </remarks>
		public List<CustomFieldString> CustomStrings { get; set; } = new();

		/// <summary>
		/// Custom fields defined as long number data type
		/// </summary>
		/// <remarks>
		/// Number data types are typically used for whole numbers with a finite range. 
		/// </remarks>
		public List<CustomFieldLong> CustomLongs { get; set; } = new();

		/// <summary>
		/// Custom fields defined as double / float data type
		/// </summary>
		/// <remarks>
		/// Double data type is used for things like currency or other values that are
		/// used in ranges.
		/// </remarks>
		public List<CustomFieldDouble> CustomDoubles { get; set; } = new();

		/// <summary>
		/// Custom fields defined as data times
		/// </summary>
		/// <remarks>
		/// Date time are fields that contain a date and time.
		/// </remarks>
		public List<CustomFieldDateTime> CustomDateTimes { get; set; } = new();

		/// <summary>
		/// Custom fields that were evaluated
		/// </summary>	
		public List<string> EvaluatedCustomFields { get; set; } = new();

		/// <summary>
		/// Custom fields for which a value was found
		/// </summary>	
		public List<string> MatchedCustomFields { get; set; } = new();

		/// <summary>
		/// The timestamp in UTC of the last time this interaction was updated
		/// </summary>
		public DateTime LastUpdatedUtc { get; set; }

		/// <summary>
		/// The time the internal participants was talking on the interaction in milliseconds
		/// </summary>
		public int InternalTalkTimeMs { get; set; }

		/// <summary>
		/// The time the external participants was talking on the interaction in milliseconds
		/// </summary>
		public int ExternalTalkTimeMs { get; set; }

		/// <summary>
		/// The sum of all silence time on the interaction in milliseconds
		/// </summary>
		public int SilenceTimeMs { get; set; }

		/// <summary>
		/// The length of silence time at the start of the interaction in milliseconds
		/// </summary>
		public int StartSilenceTimeMs { get; set; }

		/// <summary>
		/// The length of silence time at the end of the interaction in milliseconds
		/// </summary>
		public int EndSilenceTimeMs { get; set; }

		/// <summary>
		/// The percent of silence time on the interaction (from 0-1)
		/// </summary>	
		public double SilencePercent { get; set; }

		/// <summary>
		/// The percent of internal participant talk time on the interaction (from 0-1)
		/// </summary>	
		public double InternalTalkPercent { get; set; }

		/// <summary>
		/// The percent of external participant talk time on the interaction (from 0-1)
		/// </summary>	
		public double ExternalTalkPercent { get; set; }

		/// <summary>
		/// Transcription confidence level between 0 and 100. Null if not applicable.
		/// </summary>
		public int? TranscriptConfidence { get; set; }

		/// <summary>
		/// The name for the A/B test run if any
		/// </summary>
		public string? TranscriptionAbTestName { get; set; }
	}
	
public class CustomFieldString
{
	/// <summary>
	/// The value of the custom field
	/// </summary>
	public string? Value { get; set; }

	/// <summary>
	/// A SHA1 Hash in Hex format of the value from the custom field
	/// </summary>
	public string? Hash { get; set; }
		
	/// <summary>
	/// The Tethr Id of the custom field definition
	/// </summary>
	public string? Id { get; set; }
		
	/// <summary>
	/// The Display Name of the custom field definition
	/// </summary>
	public string? DisplayName { get; set; }
}

public class CustomFieldLong
{
	/// <summary>
	/// The value of the custom field
	/// </summary>
	public long Value { get; set; }
	/// <summary>
	/// The Tethr Id of the custom field definition
	/// </summary>
	public string? Id { get; set; }
	/// <summary>
	/// The Display Name of the custom field definition
	/// </summary>
	public string? DisplayName { get; set; }
}

public class CustomFieldDouble
{
	/// <summary>
	/// The value of the custom field
	/// </summary>
	public double Value { get; set; }
	/// <summary>
	/// The Tethr Id of the custom field definition
	/// </summary>
	public string? Id { get; set; }
	/// <summary>
	/// The Display Name of the custom field definition
	/// </summary>
	public string? DisplayName { get; set; }
}

public class CustomFieldDateTime
{
	/// <summary>
	/// The value of the custom field
	/// </summary>
	public DateTime Value { get; set; }
	/// <summary>
	/// The Tethr Id of the custom field definition
	/// </summary>
	public string? Id { get; set; }
	/// <summary>
	/// The Display Name of the custom field definition
	/// </summary>
	public string? DisplayName { get; set; }
}
