using Challonge.WebSite.Backend.Entities;
using Newtonsoft.Json;

namespace Challonge.WebSite.Backend.Services
{
	public class ParticipantResponse
	{
		[JsonProperty("participant")]
		public Participant Participant { get; set; }
	}
}
