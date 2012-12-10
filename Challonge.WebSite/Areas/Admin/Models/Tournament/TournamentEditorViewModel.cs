using System;
using System.Collections.Generic;
using System.Web;
using Challonge.Backend.Entities;
using System.Web.Mvc;
using Newtonsoft.Json;
using Challonge.WebSite.Models;

namespace Challonge.WebSite.Areas.Admin.Models.TournamentModels
{
	public class TournamentEditorViewModel
	{
		public TournamentEditorViewModel(EditorAction action, TournamentModel tournament)
		{
			Action = action;
			Tournament = tournament;

			TournamentTypes = new List<SelectListItem>();
			TournamentTypes.Add(new SelectListItem
			{
				Selected = tournament != null && tournament.TournamentType == TournamentType.League,
				Text = "Liga",
				Value = TournamentType.League.ToString()
			});

			TournamentTypes.Add(new SelectListItem
			{
				Selected = tournament != null && tournament.TournamentType == TournamentType.SingleElimination,
				Text = "Eliminación Simple",
				Value = TournamentType.SingleElimination.ToString()
			});


			TournamentTypes.Add(new SelectListItem
			{
				Selected = tournament != null && tournament.TournamentType == TournamentType.DoubleElimination,
				Text = "Eliminación Doble",
				Value = TournamentType.DoubleElimination.ToString()
			});


			IList<PlayerModel> players = new List<PlayerModel>();

			if (tournament != null && tournament.Players != null)
				players = tournament.Players;

			PlayersJson = JsonConvert.SerializeObject(players);

			if (tournament != null)
			{
				TournamentTypeForDisplay = tournament.TournamentType == TournamentType.DoubleElimination ? "Eliminación" : "Liga";
			}

		}

		public string TournamentTypeForDisplay { get; private set; }

		public EditorAction Action { get; private set; }

		public TournamentModel Tournament { get; private set; }

		public string PlayersJson { get; private set; }

		public IList<SelectListItem> TournamentTypes { get; private set; }
	}
}
