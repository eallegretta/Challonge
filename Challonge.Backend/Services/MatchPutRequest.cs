using System;
using System.Collections.Generic;
using System.Text;
using System.Xml.Serialization;
using System.Runtime.Serialization;

namespace Challonge.Backend.Services
{
	public class MatchPutRequest
	{
		[DataMember(Name = "match[winner_id]")]
		public string WinnerId
		{
			set;
			get;
		}

		[DataMember(Name = "match[scores_csv]")]
		public string ScoresCsv
		{
			get;
			set;
		}
	}
}
