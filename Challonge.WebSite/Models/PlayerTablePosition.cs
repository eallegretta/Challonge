using System;
using System.Collections.Generic;
using System.Web;

namespace Challonge.WebSite.Models
{
	public class PlayerTablePositionModel
	{

		public PlayerModel Player { get; set; }

		public int Wins { get; set; }

		public int Tie { get; set; }

		public int Lost { get; set; }

		public int GoalsFavor { get; set; }

		public int GoalsAgainst { get; set; }

		public int Score { get; set; }

		public int Played
		{
			get
			{
				return Wins + Tie + Lost;
			}
		}
	}
}
