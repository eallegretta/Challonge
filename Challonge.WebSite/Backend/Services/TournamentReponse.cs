using Challonge.WebSite.Backend.Entities;
using Newtonsoft.Json;

namespace Challonge.WebSite.Backend.Services
{
	public class TournamentResponse
	{
		[JsonProperty("tournament")]
		public Tournament Tournament { get; set; }
	}
}
