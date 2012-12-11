using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Challonge.WebSite.Areas.Admin.Models.Tournament;
using Challonge.WebSite.Backend.Entities;
using Challonge.WebSite.Backend.Services;
using Newtonsoft.Json;
using Challonge.WebSite.Models;
using Challonge.WebSite.Areas.Admin.Models;

namespace Challonge.WebSite.Areas.Admin.Controllers
{
    public class TournamentController : Controller
    {
		private readonly TournamentService _tournamentService = new TournamentService();
		private readonly ParticipantService _participantService = new ParticipantService();
		private readonly MatchService _matchService = new MatchService();

        public ActionResult Index()
        {

			var tournaments = _tournamentService.List();

			var model = new TournamentIndexViewModel(
				tournaments.Where(t => t.CompletedAt.HasValue).ToList(),
				tournaments.Where(t => !t.CompletedAt.HasValue && t.StartedAt.HasValue).ToList(),
				tournaments.Where(t => !t.StartedAt.HasValue && !t.PublishedAt.HasValue).ToList(),
				tournaments.Where(t => !t.StartedAt.HasValue && t.PublishedAt.HasValue).ToList());


            return View(model);
        }

        public ActionResult Details(int id)
        {
			var tournament = TournamentModel.Create(
				_tournamentService.Get(id),
				_participantService.List(id),
				_matchService.List(id),
				null);


            return View(new TournamentEditorViewModel(EditorAction.Details, tournament));
        }

        public ActionResult Create()
        {
            return View(new TournamentEditorViewModel(EditorAction.Create, null));
        } 

        [HttpPost]
        public ActionResult Create(TournamentModel tournamentModel)
        {
            try
            {
				if(!ModelState.IsValid)
					return View(new TournamentEditorViewModel(EditorAction.Create, tournamentModel));

				var tournament = new Tournament();
				tournament.Name = tournamentModel.Name;
				switch (tournamentModel.TournamentType)
				{
					case TournamentType.SingleElimination:
						tournament.TournamentType = "single elimination";
						break;
					case TournamentType.DoubleElimination:
						tournament.TournamentType = "double elimination";
						break;
					case TournamentType.League:
						tournament.TournamentType = "round robin";
						break;
				}
				tournament.Url = tournamentModel.Name.Replace(" ", "_").Trim();
				tournament.OpenSignup = false;
				if (tournamentModel.TournamentType == TournamentType.SingleElimination || 
					tournamentModel.TournamentType == TournamentType.DoubleElimination)
					tournament.HoldThirdPlaceMatch = true;
				tournament.RoundRobinPointsForMatchTie = 1;
				tournament.RoundRobinPointsForMatchWin = 3;

				_tournamentService.Create(tournament);

				foreach (var player in tournamentModel.Players)
				{
					var participant = new Participant
					{
						TournamentId = tournament.Id,
						Name = player.Name,
						Misc = JsonConvert.SerializeObject(player)
					};

					_participantService.Create(participant);
				}

                return RedirectToAction("Index");
            }
            catch
            {
				return View(new TournamentEditorViewModel(EditorAction.Create, tournamentModel));
            }
        }
        
        public ActionResult Edit(int id)
        {
            return View();
        }

        [HttpPost]
        public ActionResult Edit(int id, FormCollection collection)
        {
            try
            {
                // TODO: Add update logic here
 
                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        public ActionResult Delete(int id)
        {
            return View();
        }

        [HttpPost]
        public ActionResult Delete(int id, FormCollection collection)
        {
            try
            {
                // TODO: Add delete logic here
 
                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }
    }
}
