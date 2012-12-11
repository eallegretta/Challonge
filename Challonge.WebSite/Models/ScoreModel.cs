using System;
using System.Collections.Generic;
using System.Web;
using System.Text;
using Challonge.WebSite.Backend.Entities;

namespace Challonge.WebSite.Models
{
	public class ScoreModel
	{
		public int TournamentId { get; set; }

		public int HomePlayerId { get; set; }

		public int[] HomePlayerScores { get; set; }

		public int AwayPlayerId { get; set; }

		public int[] AwayPlayerScores { get; set; }

		public string WinnerId
		{
			get
			{
				int homePlayerScores = 0;
				int awayPlayerScores = 0;

				for (int index = 0; index < HomePlayerScores.Length; index++)
				{
					homePlayerScores += HomePlayerScores[index];
					awayPlayerScores += AwayPlayerScores[index];
				}

				if (homePlayerScores > awayPlayerScores)
					return HomePlayerId.ToString();
				else if (homePlayerScores < awayPlayerScores)
					return AwayPlayerId.ToString();
				else
					return "tie";
			}
		}

		public string GetScoresCsv(Match match)
		{
			string scoringFormat = string.Empty;
			if (match.Player1Id == HomePlayerId)
				scoringFormat = "{0}-{1}";
			else
				scoringFormat = "{1}-{0}";

			StringBuilder scores = new StringBuilder();
			for (int index = 0; index < HomePlayerScores.Length; index++)
			{
				if(index > 0)
					scores.Append(",");
				scores.AppendFormat(scoringFormat, HomePlayerScores[index], AwayPlayerScores[index]);
			}
			return scores.ToString();
		}
	}
}
