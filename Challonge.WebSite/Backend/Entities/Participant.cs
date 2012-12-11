using System;
using Newtonsoft.Json;

namespace Challonge.WebSite.Backend.Entities
{
	[Serializable]
	[JsonObject("participant")]
	public class Participant
	{
		[JsonProperty("active")]
		public bool Active { get; set; }

		[JsonProperty("created_at")]
		public DateTime CreatedAt { get; set; }

		[JsonProperty("final_rank")]
		public int? FinalRank { get; set; }

		[JsonProperty("id")]
		public int Id { get; set; }

		[JsonProperty("misc")]
		public string Misc { get; set; }

		[JsonProperty("name")]
		public string Name { get; set; }
  
		[JsonProperty("new_user_email")]
		public string NewUserEmail { get; set; }

		[JsonProperty("seed")]
		public int Seed { get; set; }

		[JsonProperty("tournament_id")]
		public int TournamentId { get; set; }

		[JsonProperty("updated_at")]
		public DateTime UpdatedAt { get; set; }
  
		[JsonProperty("challonge_username")]
		public string ChallongeUsername { get; set; }
		
		[JsonProperty("challonge_email_address_verified")]
		public string ChallongeEmailAddressVerified { get; set; }

	}
}
