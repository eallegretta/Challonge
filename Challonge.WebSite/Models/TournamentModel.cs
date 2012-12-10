using System;
using System.Collections.Generic;
using System.Web;
using System.ComponentModel.DataAnnotations;
using Challonge.Backend.Entities;
using System.Linq;

namespace Challonge.WebSite.Models
{
	public class TournamentModel
	{
		public static TournamentModel CreateLeageTournament(TournamentModel tournamentModel, Tournament tournament, IList<Participant> participants,
			IList<Match> matches, string loggedParticipantUsername)
		{
			foreach (var participant in participants)
			{
				var playerModel = PlayerModel.Create(participant);
				tournamentModel.Players.Add(playerModel);

				var playerMatches = matches.Where(m => (m.Player1Id == participant.Id || m.Player2Id == participant.Id) && m.IsComplete);

				var playerPosition = new PlayerTablePositionModel
				{
					Player = playerModel,
					Wins = playerMatches.Where(m => m.WinnerId == participant.Id).Count(),
					Tie = playerMatches.Where(m => m.IsTie).Count(),
					Lost = playerMatches.Where(m => !m.IsTie && m.WinnerId != participant.Id).Count(),
					GoalsFavor = matches.Where(m => m.Player1Id == participant.Id).Select(m => m.GoalsPlayer1).Sum() +
									matches.Where(m => m.Player2Id == participant.Id).Select(m => m.GoalsPlayer2).Sum(),
					GoalsAgainst = matches.Where(m => m.Player1Id == participant.Id).Select(m => m.GoalsPlayer2).Sum() +
									matches.Where(m => m.Player2Id == participant.Id).Select(m => m.GoalsPlayer1).Sum()

				};

				playerPosition.Score = playerPosition.Wins * (int)tournament.RoundRobinPointsForMatchWin + playerPosition.Tie * (int)tournament.RoundRobinPointsForMatchTie;


				tournamentModel.PlayerPositions.Add(playerPosition);
			}




			var matchGrouping = matches.GroupBy(m => m.Round.Value);
			tournamentModel.Matches = new Dictionary<int, IList<MatchModel>>();
			foreach (var group in matchGrouping)
			{
				IList<MatchModel> matchModels = new List<MatchModel>();
				foreach (var match in group.ToList())
				{
					matchModels.Add(MatchModel.Create(match, participants));
				}
				tournamentModel.Matches.Add(group.Key, matchModels);
			}

			tournamentModel.Players = tournamentModel.Players.OrderBy(p => p.Name).ToList();

			tournamentModel.PlayerPositions = tournamentModel.PlayerPositions
													.OrderByDescending(p => p.Score)
													.ThenByDescending(p => p.GoalsFavor - p.GoalsAgainst)
													.ThenByDescending(p => p.GoalsFavor)
													.ToList();

			var loggedParticipant = tournamentModel.Players.Where(p => string.Equals(p.Name, loggedParticipantUsername, StringComparison.InvariantCultureIgnoreCase)).FirstOrDefault();

			if (loggedParticipant != null)
			{
				var notPlayedPlayerIds = matches.Where(m => !m.IsComplete && (m.Player1Id == loggedParticipant.Id || m.Player2Id == loggedParticipant.Id))
										.Select(m =>
										{
											if (m.Player1Id == loggedParticipant.Id)
												return m.Player2Id;
											else
												return m.Player1Id;
										});

				tournamentModel.CanSendResults = true;

				tournamentModel.NotPlayedPlayers = tournamentModel.Players.Where(p => notPlayedPlayerIds.Contains(p.Id)).ToList();

				tournamentModel.LoggedParticipant = loggedParticipant;
			}

			return tournamentModel;
		}

		public static TournamentModel Create(Tournament tournament, IList<Participant> participants, 
			IList<Match> matches, string loggedParticipantUsername)
		{
			var tournamentModel = new TournamentModel
			{
				Id = tournament.Id,
				Name = tournament.Name,
				Description = tournament.DescriptionSource
			};

			switch(tournament.TournamentType)
			{
				case "single elimination":
					tournamentModel.TournamentType = TournamentType.SingleElimination;
					break;
				case "double elimination":
					tournamentModel.TournamentType = TournamentType.DoubleElimination;
					break;
				case "round robin":
					tournamentModel.TournamentType = TournamentType.League;
					break;
			}

			if (tournamentModel.TournamentType == TournamentType.League)
				return CreateLeageTournament(tournamentModel, tournament, participants, matches, loggedParticipantUsername);

			return tournamentModel;
		}

		public TournamentModel()
		{
			Players = new List<PlayerModel>();
			Matches = new Dictionary<int, IList<MatchModel>>();
			PlayerPositions = new List<PlayerTablePositionModel>();
		}

		public int Id { get; set; }

		[Required]
		public string Name { get; set; }

		public string Description { get; set; }

		public TournamentType TournamentType { get; set; }

		public IList<PlayerModel> Players { get; private set; }

		public IDictionary<int, IList<MatchModel>> Matches { get; private set; }

		public IList<PlayerTablePositionModel> PlayerPositions { get; private set; }

		public IList<PlayerModel> NotPlayedPlayers { get; private set; }

		public PlayerModel LoggedParticipant { get; private set; }

		public bool CanSendResults { get; private set; }
	}
}
