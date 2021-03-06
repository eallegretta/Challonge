﻿using System.Linq;
using Challonge.WebSite.Backend.Caching;
using Challonge.WebSite.Backend.Entities;

namespace Challonge.WebSite.Backend.Services
{
	public class ParticipantService
	{
		public Participant[] List(int tournamentId)
		{
			return CacheManager.GetOrSet<Participant[]>("participants-" + tournamentId, () => {
			
			WebClient client = new WebClient();
			var response = client.Get<ParticipantResponse[]>(string.Format("tournaments/{0}/participants.json", tournamentId));

			var query = from r in response
						select r.Participant;
			
			return query.ToArray();
			                                            });
		}

		public void Create(Participant participant)
		{
			WebClient client = new WebClient();
			participant = client.Post<Participant, Participant>(string.Format("tournaments/{0}/participants.json", participant.TournamentId), participant);
		}
	}
}
