using System;
using System.Collections.Generic;
using System.Web;
using System.Linq;
using Challonge.Backend.Entities;

namespace Challonge.WebSite.Areas.Admin.Models.TournamentModels
{
	public class TournamentIndexViewModel
	{
		public TournamentIndexViewModel(IList<Tournament> completedTournaments,
			IList<Tournament> inProgressTournaments,
			IList<Tournament> notPublishedTournaments,
			IList<Tournament> publishedTournaments)
		{
			CompletedTournaments = completedTournaments;
			InProgressTouranments = inProgressTournaments;
			PublishedTournaments = publishedTournaments;
			NotPublishedTournaments = notPublishedTournaments;
		}

		public IList<Tournament> CompletedTournaments { get; private set; }

		public IList<Tournament> InProgressTouranments { get; private set; }

		public IList<Tournament> NotPublishedTournaments { get; private set; }

		public IList<Tournament> PublishedTournaments { get; private set; }


	}
}
