using Challonge.WebSite.Backend.Entities;
using Newtonsoft.Json;

namespace Challonge.WebSite.Backend.Services
{
	public class MatchResponse
	{
		[JsonProperty("match")]
		public Match Match { get; set; }
	}
}
