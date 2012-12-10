using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Challonge.Backend.Entities;

namespace Challonge.Backend.Services
{
	public class TournamentService
	{
		public Tournament[] List()
		{
			WebClient client = new WebClient();
			var response = client.Get<TournamentResponse[]>("tournaments.json");

			
			var query = from r in response
				select r.Tournament;
			
			return query.ToArray();
		}

		public Tournament Get(int id)
		{
			WebClient client = new WebClient();
			
			var response = client.Get<TournamentResponse>("tournaments/" + id + ".json");
			
			return response.Tournament;
		}

		public void Create(Tournament tournament)
		{
			WebClient client = new WebClient();
			tournament = client.Post<TournamentPostRequest, Tournament>("tournaments.json", new TournamentPostRequest(tournament));
		}
	}
}
