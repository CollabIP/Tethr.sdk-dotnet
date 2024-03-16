using System.Data.Common;

namespace Tethr.Sdk.Session
{
	/// <summary>
	/// Configuration options for <see cref="ITethrSession"/>.
	/// </summary>
	public class TethrOptions
	{
		/// <summary>
		/// The URI to the Tethr API server.
		/// </summary>
		public string Uri { get; set; }
		
		/// <summary>
		/// The Tethr API user name
		/// </summary>
		public string ApiUser { get; set; }
		
		/// <summary>
		/// The API password provided from Tethr.
		/// </summary>
		public string Password { get; set; }
	}
}