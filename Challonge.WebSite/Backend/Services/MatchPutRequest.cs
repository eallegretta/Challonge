using Newtonsoft.Json;

namespace Challonge.WebSite.Backend.Services
{
    [JsonObject]
	public class MatchPutRequest
	{
		[JsonProperty("match[winner_id]")]
		public string WinnerId
		{
			set;
			get;
		}

        [JsonProperty("match[scores_csv]")]
		public string ScoresCsv
		{
			get;
			set;
		}
	}
}
