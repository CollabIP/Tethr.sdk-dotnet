namespace Tethr.Sdk.Model;

public class InteractionParticipant
{
		/// <summary>
		/// The Id for this Participant within Tethr
		/// </summary>
		public byte ParticipantId { get; set; }

		/// <summary>
		/// If this is a flow, this id will match the ParticipantId of this participant from the base interaction.
		/// </summary>
		public int FlowParticipantId { get; set; }

		/// <summary>
		/// If this is a flow, this is the id of the base Interaction this participant is from.
		/// </summary>
		public string? InteractionId { get; set; }

		/// <summary>
		/// The type for the external reference ID stored in "RefId" for this participant.
		/// </summary>
		/// <remarks>
		///  <para>
		/// This represents the kind of or what external system the ID given to a
		/// participant is from.  Reference Types are not pre-defined and are set per environment.
		/// Once created they can not be renamed, or deleted.  They can be hidden and will no longer
		/// be assigned to participants.
		/// </para><para>
		/// There can only be 1 primary reference type for a given customer, if more are needed, they
		/// can be placed in the Metadata.  But the Reference Type is what's used in Tethr for grouping
		/// callers.
		/// </para><para>
		/// Let's take an example of ACME Contact Center, they have out sourced their staffing, the our sourcing
		/// firm provided an ID for each of the agents answering the phones, for each of the Agents, we would have a
		/// RefType of "agent". However, ACME Contact Center also has employees that supervise, and sometimes
		/// answer a call, for a call that an employee was on, RefType would be "employee" and RefId would contain
		/// their employee ID in ACME Contact Center's HR database.
		/// </para><para>
		/// ACME Contact Center also has a CRM, and the Contact Center application used by the Agent, injects the
		/// customers ID from the CRM into the SIPRec data stream, Tethr detects that record, and would set the
		/// RefType to "customer" and the RefId to the Id from the CRM.
		/// </para><para>
		/// Or if a customer is Asked to enter their account number in the IVR when they first connect.
		/// That customer Id number could be inserted here as well. </para>
		/// </remarks>
		public string? RefType { get; set; }

		/// <summary>
		/// The external reference ID corresponding to the system specified in the RefType field.
		/// </summary>
		public string? RefId { get; set; }

		/// <summary>
		/// The unique ID give to this participant from the originating audio ingestion system.
		/// </summary>
		/// <remarks>
		/// If the interaction was injected via the archived call API, then this will be the channel
		/// of the participant in the original audio file.  This can be 0 or 1 based.
		///
		/// If the call was injected via the live call API, then this value will be determined
		/// by the ingestion system, but is often a SIP channel Id.
		/// </remarks>
		public string? ContactId { get; set; }

		/// <summary>
		/// The phone number from the Caller ID for this participant.
		/// </summary>
		public string? PhoneNumber { get; set; }

		/// <summary>
		/// The email address of the participant, if available.
		/// </summary>
		/// <remarks>
		/// It's very uncommon for Tethr to have the Email address of a participant.
		/// But if we have it, we will send it along.
		/// </remarks>
		public string? Email { get; set; }

		/// <summary>
		/// The First Name of the participant. if available.
		/// </summary>
		public string? FirstName { get; set; }

		/// <summary>
		/// The Last Name of the participant. if available.
		/// </summary>
		public string? LastName { get; set; }

		/// <summary>
		/// The general purpose Display Name of the participant, generated from the other fields.
		/// </summary>
		public string? DisplayName { get; set; }

		/// <summary>
		/// A flag indicating this participant is inside the contact center
		/// (e.g. this would be true if it's an employee/agent in the contact center, and false if it's an external customer).
		/// </summary>
		/// <remarks>
		/// This is typically determined by what side of the Phone System the participant was
		/// connected to.  E.g. If the participants connection was connected to the contact Centers PBX
		/// they are marked as Internal.  It would be possible that if an agent called another agent
		/// via an internal extension both participants would be marked and Internal, but if the first
		/// agent called the second agent using an outside number, it's possible one of the agents wouldn't
		/// be marked an Internal. Actual results are very depended on how the contact centers phone
		/// and audio ingestion systems are configured.
		/// </remarks>
		public bool IsInternal { get; set; }

		/// <summary>
		/// The different elements Tethr detected on an interaction from this participant, similar to classifications.
		/// </summary>
		public List<Segment> Segments { get; set; } = new();

		/// <summary>
		/// The utterances and the words from this participant, if provided.
		/// </summary>
		/// <remarks>
		/// If you require the full transcription from the interactions, please contact Tethr Support to have it enabled on for your API key.
		/// </remarks>
		public List<Utterance> Utterances { get; set; } = new();
}
