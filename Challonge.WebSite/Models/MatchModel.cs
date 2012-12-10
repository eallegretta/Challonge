using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Challonge.Backend.Entities;

namespace Challonge.WebSite.Models
{
	public class MatchModel
	{
		public static MatchModel Create(Match match, IList<Participant> participants)
		{
			var model = new MatchModel();
			model.Player1 = PlayerModel.Create(participants.Where(p => p.Id == match.Player1Id).First());
			model.Player2 = PlayerModel.Create(participants.Where(p => p.Id == match.Player2Id).First());

			if (!string.IsNullOrEmpty(match.ScoresCsv))
			{
				var matchArr = match.ScoresCsv.Split('-');
				model.ScorePlayer1 = int.Parse(matchArr[0]);
				model.ScorePlayer2 = int.Parse(matchArr[1]);
			}

			model.Played = match.State == "complete";

			model.Round = match.Round.Value;

			return model;
		}

		public bool Played { get; private set; }


		public PlayerModel Player1 { get; private set; }

		public int ScorePlayer1 { get; private set; }

		public PlayerModel Player2 { get; private set; }

		public int ScorePlayer2 { get; private set; }

		public int Round { get; private set; }
	}
}
