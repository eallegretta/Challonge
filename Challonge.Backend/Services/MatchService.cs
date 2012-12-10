﻿using System;
using System.Collections.Generic;
using System.Text;
using Challonge.Backend.Caching;
using Challonge.Backend.Entities;
using System.Linq;

namespace Challonge.Backend.Services
{
	public class MatchService
	{
		public Match[] List(int tournamentId)
		{
			return CacheManager.GetOrSet<Match[]>("matches-" + tournamentId, () =>
			                               {
			var client = new WebClient();
			
			var response = client.Get<MatchResponse[]>(string.Format("tournaments/{0}/matches.json", tournamentId));
			
			var query = from r in response
						select r.Match;
			
			return query.ToArray();
			                               });
		}

		public Match SendResult(int tournamentId, int matchId, string winnerId, string scoreCsv)
		{
			var client = new WebClient();
			return client.Put<MatchPutRequest, Match>(
					string.Format("tournaments/{0}/matches/{1}.json", tournamentId, matchId),
					new MatchPutRequest { WinnerId = winnerId, ScoresCsv = scoreCsv });
		}
	}
}
