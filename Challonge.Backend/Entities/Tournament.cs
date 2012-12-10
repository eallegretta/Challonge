using System;
using System.Collections.Generic;
using System.Web;
using System.Xml.Serialization;
using System.Xml.Schema;
using Newtonsoft.Json;

namespace Challonge.Backend.Entities
{
	[Serializable]
	[JsonObject("tournament")]
	public class Tournament
	{
		[JsonProperty("accept_attachments")]
		public bool AcceptAttachments { get; set; }

		[JsonProperty("completed_at")]
		public DateTime? CompletedAt { get; set; }

		[JsonProperty("created_at")]
		public DateTime CreatedAt { get; set; }

		[JsonProperty("created_by_api")]
		public bool CreatedByApi { get; set; }

		[JsonProperty("hide_forum")]
		public bool HideForum { get; set; }

		[JsonProperty("hold_third_place_match")]
		public bool HoldThirdPlaceMatch { get; set; }

		[JsonProperty("id")]
		public int Id { get; set; }

		[JsonProperty("name")]
		public string Name { get; set; }

		[JsonProperty("notify_users_when_matches_open")]
		public bool NotifyUsersWhenMatchesOpen { get; set; }

		[JsonProperty("notify_users_when_the_tournament_ends")]
		public bool NotifyUsersWhenTheTournamentEnds { get; set; }

		[JsonProperty("open_signup")]
		public bool OpenSignup { get; set; }

		[JsonProperty("private")]
		public bool Private { get; set; }

		[JsonProperty("progress_meter")]
		public int ProgressMeter { get; set; }

		[JsonProperty("pts_for_bye")]
		public decimal PointsForBye { get; set; }

		[JsonProperty("pts_for_game_tie")]
		public decimal PointsForGameTie { get; set; }

		[JsonProperty("pts_for_game_win")]
		public decimal PointsForGameWin { get; set; }

		[JsonProperty("pts_for_match_tie")]
		public decimal PointsForMatchTie { get; set; }

		[JsonProperty("pts_for_match_win")]
		public decimal PointsForMatchWin { get; set; }

		[JsonProperty("published_at")]
		public DateTime? PublishedAt { get; set; }

		[JsonProperty("ranked_by")]
		public string RankedBy { get; set; }

		[JsonProperty("require_score_agreement")]
		public bool RequireScoreAgreement { get; set; }

		[JsonProperty("rr_pts_for_game_tie")]
		public decimal RoundRobinPointsForGameTie { get; set; }

		[JsonProperty("rr_pts_for_game_win")]
		public decimal RoundRobinPointsForGameWin { get; set; }

		[JsonProperty("rr_pts_for_match_tie")]
		public decimal RoundRobinPointsForMatchTie { get; set; }

		[JsonProperty("rr_pts_for_match_win")]
		public decimal RoundRobinPointsForMatchWin { get; set; }

		[JsonProperty("second_place_id")]
		public int? SecondPlaceId { get; set; }

		[JsonProperty("sequential_pairings")]
		public bool SequentialPairings { get; set; }

		[JsonProperty("show_rounds")]
		public bool ShowRounds { get; set; }

		[JsonProperty("signup_requires_account")]
		public bool SingupRequiresAccount { get; set; }

		[JsonProperty("started_at")]
		public DateTime? StartedAt { get; set; }

		[JsonProperty("swiss_rounds")]
		public int SwissRounds { get; set; }

		[JsonProperty("third_place_id")]
		public int? ThirdPlaceId { get; set; }

		[JsonProperty("tournament_type")]
		public string TournamentType { get; set; }

		[JsonProperty("updated_at")]
		public DateTime UpdatedAt { get; set; }

		[JsonProperty("url")]
		public string Url { get; set; }

		[JsonProperty("winner_id")]
		public int? WinnedId { get; set; }

		[JsonProperty("description_source")]
		public string DescriptionSource { get; set; }

		[JsonProperty("subdomain")]
		public string Subdomain { get; set; }

		[JsonProperty("full_challonge_url")]
		public string FullChallongUrl { get; set; }

		[JsonProperty("live_image_url")]
		public string LiveImageUrl { get; set; }

		[JsonProperty("sign_up_url")]
		public string SignUpUrl { get; set; }
	}
}
