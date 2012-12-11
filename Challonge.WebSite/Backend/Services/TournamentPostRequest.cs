using Challonge.WebSite.Backend.Entities;
using Newtonsoft.Json;

namespace Challonge.WebSite.Backend.Services
{
	[JsonObject("tournament")]
	public class TournamentPostRequest
	{
		public TournamentPostRequest()
		{
		}

		public TournamentPostRequest(Tournament tournament)
		{
			Name = tournament.Name;
			TournamentType = tournament.TournamentType;
			Url = tournament.Url;
			OpenSignup = tournament.OpenSignup;
			HoldThirdPlaceMatch = tournament.HoldThirdPlaceMatch;
			RoundRobinPointsForMatchTie = tournament.RoundRobinPointsForMatchTie;
			RoundRobinPointsForMatchWin = tournament.RoundRobinPointsForMatchWin;
		}

		[JsonProperty("name")]
		public string Name { get; set; }

		[JsonProperty("tournament-type")]
		public string TournamentType { get; set; }

		[JsonProperty("url")]
		public string Url { get; set; }

		[JsonProperty("open-signup")]
		public bool OpenSignup { get; set; }

		[JsonProperty("hold-third-place-match")]
		public bool HoldThirdPlaceMatch { get; set; }

		[JsonProperty("rr-pts-for-match-tie")]
		public decimal RoundRobinPointsForMatchTie { get; set; }

		[JsonProperty("rr-pts-for-match-win")]
		public decimal RoundRobinPointsForMatchWin { get; set; }
	}
}
