using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Challonge.Backend.Services;
using Challonge.WebSite.Models;
using Challonge.WebSite.Filters;
using System.Xml;
using System.Text;
using Challonge.WebSite.ActionResults;
using System.ServiceModel.Syndication;

namespace Challonge.WebSite.Controllers
{
    public class HomeController : Controller
    {
		private readonly TournamentService _tournamentService = new TournamentService();
		private readonly ParticipantService _participantService = new ParticipantService();
		private readonly MatchService _matchService = new MatchService();

		public ActionResult Index()
        {
            return View(_tournamentService.List());
        }

		public ActionResult Tournament(int id)
		{
			var participants = _participantService.List(id);

			var tournamentModel = TournamentModel.Create(
				_tournamentService.Get(id),
				participants,
				_matchService.List(id),
				User.Identity.Name);

			return View(tournamentModel);
		}

		public ActionResult Rss(int id)
		{
			var items = new List<SyndicationItem>();
			var tournament = _tournamentService.Get(id);
			var participants = _participantService.List(id);
			foreach (var match in _matchService.List(id).Where(m => m.IsComplete).OrderByDescending(m => m.UpdatedAt))
			{
				var homeParticipant = PlayerModel.Create(participants.Where(p => p.Id == match.Player1Id).First());
				var awayParticipant = PlayerModel.Create(participants.Where(p => p.Id == match.Player2Id).First());

				//string homeName = homeParticipant.Id == match.WinnerId ? "<b style='color:blue'>" + homeParticipant.Name + "</b>" : homeParticipant.Name;
				//string awayName = awayParticipant.Id == match.WinnerId ? "<b style='color:blue'>" + awayParticipant.Name + "</b>" : awayParticipant.Name;
				string homeName = homeParticipant.Name;
				string awayName = awayParticipant.Name;

				string title = string.Format("{0} {1} vs {2} {3}", homeName, match.GoalsPlayer1, match.GoalsPlayer2, awayName);
				items.Add(new SyndicationItem(title, string.Empty, null));
			}
			SyndicationFeed feed = new SyndicationFeed(tournament.Name, string.Empty, Request.Url, items);

			return new RssActionResult { Feed = feed };
		}

		[HttpPost]
		[CaptchaValidator]
		public ActionResult SendResult(ScoreModel scoreModel, bool captchaValid)
		{
			if (!captchaValid)
				return Content("El texto Captcha ingresado es incorrecto");

			if (scoreModel.HomePlayerId == scoreModel.AwayPlayerId)
				return Content("Los jugadores deben ser distintos");

			if (scoreModel.HomePlayerId == 0)
				return Content("Debe elegir un jugador local");

			if (scoreModel.AwayPlayerId == 0)
				return Content("Debe elegir un jugador visitante");


			var matches = _matchService.List(scoreModel.TournamentId);
			
			var match = matches.Where(m =>
				{
					return ((m.Player1Id == scoreModel.HomePlayerId &&
							m.Player2Id == scoreModel.AwayPlayerId) ||
							(m.Player1Id == scoreModel.AwayPlayerId &&
							m.Player2Id == scoreModel.HomePlayerId));
				}).FirstOrDefault();


			if (match == null)
				return Content("El partido no no existe");

			if (match.IsComplete)
				return Content("El partidono ya ha sido disputado");

			_matchService.SendResult(scoreModel.TournamentId, match.Id,
				scoreModel.WinnerId, scoreModel.GetScoresCsv(match));

			return RedirectToAction("SendResultCompleted", new { id = scoreModel.TournamentId });
		}

		public ActionResult SendResultCompleted(int id)
		{
			ViewData["tournamentId"] = id;
			return View();
		}
    }
}
