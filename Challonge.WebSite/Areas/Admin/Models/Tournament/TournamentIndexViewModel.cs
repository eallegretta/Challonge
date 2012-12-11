using System.Collections.Generic;

namespace Challonge.WebSite.Areas.Admin.Models.Tournament
{
	public class TournamentIndexViewModel
	{
		public TournamentIndexViewModel(IList<Backend.Entities.Tournament> completedTournaments,
			IList<Backend.Entities.Tournament> inProgressTournaments,
			IList<Backend.Entities.Tournament> notPublishedTournaments,
			IList<Backend.Entities.Tournament> publishedTournaments)
		{
			CompletedTournaments = completedTournaments;
			InProgressTouranments = inProgressTournaments;
			PublishedTournaments = publishedTournaments;
			NotPublishedTournaments = notPublishedTournaments;
		}

		public IList<Backend.Entities.Tournament> CompletedTournaments { get; private set; }

		public IList<Backend.Entities.Tournament> InProgressTouranments { get; private set; }

		public IList<Backend.Entities.Tournament> NotPublishedTournaments { get; private set; }

		public IList<Backend.Entities.Tournament> PublishedTournaments { get; private set; }


	}
}
