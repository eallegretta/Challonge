using System;
using System.Collections.Generic;
using System.Text;
using System.Xml.Serialization;
using Newtonsoft.Json;

namespace Challonge.Backend.Entities
{
	[JsonObject("match")]
	public class Match
	{
		[JsonProperty("created_at")]
		public DateTime CreatedAt { get; set; }

		[JsonProperty("has_attachment")]
		public bool HasAttachment { get; set; }

		[JsonProperty("id")]
		public int Id { get; set; }

		[JsonProperty("identifier")]
		public string Identifier { get; set; }

		[JsonProperty("looser_id")]
		public int LooserId { get; set; }

		[JsonProperty("player1_id")]
		public int? Player1Id { get; set; }

		[JsonProperty("player1_is_prereq_series_loser")]
		public bool Player1IsPrereqSeriesLoser { get; set; }

		[JsonProperty("player1_prereq_series_id")]
		public int? Player1PrereqSeriesId { get; set; }

		[JsonProperty("player2_id")]
		public int? Player2Id { get; set; }

		[JsonProperty("player2_is_prereq_series_loser")]
		public bool Player2IsPrereqSeriesLoser { get; set; }

		[JsonProperty("player2_prereq_series_id")]
		public int? Player2PrereqSeriesId { get; set; }

		[JsonProperty("round")]
		public int? Round { get; set; }

		[JsonProperty("started_at")]
		public DateTime? StartedAt { get; set; }

		[JsonProperty("state")]
		public string State { get; set; }

		[JsonProperty("tournament_id")]
		public int TournamentId { get; set; }

		[JsonProperty("updated_at")]
		public DateTime UpdatedAt { get; set; }

		[JsonProperty("winner_id")]
		public int? WinnerId { get; set; }

		[JsonProperty("prerequisite_match_ids_csv")]
		public string PrerequisiteMatchIdsCsv { get; set; }

		[JsonProperty("scores_csv")]
		public string ScoresCsv { get; set; }

		[JsonIgnore]
		public bool IsComplete
		{
			get
			{
				return State.Equals("complete", StringComparison.InvariantCultureIgnoreCase);
			}
		}

		[JsonIgnore]
		public bool IsTie
		{
			get
			{
				return IsComplete && WinnerId == null;
			}
		}

		[JsonIgnore]
		public int GoalsPlayer1
		{
			get
			{
				return GetGoals(0);
			}
		}

		[JsonIgnore]
		public int GoalsPlayer2
		{
			get
			{
				return GetGoals(1);
			}
		}

		private int GetGoals(int index)
		{
			if (string.IsNullOrEmpty(ScoresCsv))
				return 0;

			int goals = 0;
			foreach (var score in ScoresCsv.Split(','))
			{
				goals += int.Parse(score.Split('-')[index]);
			}

			return goals;
		}
	}
}
