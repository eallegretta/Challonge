using System;
using System.Collections.Generic;
using System.Web;
using System.ComponentModel.DataAnnotations;
using Challonge.WebSite.Backend.Entities;
using Newtonsoft.Json;

namespace Challonge.WebSite.Models
{
	public class PlayerModel
	{
		public static PlayerModel Create(Participant participant)
		{
			string misc = participant.Misc;

			if (string.IsNullOrEmpty(misc))
			{
				if (participant.Name.StartsWith("{"))
				{
					misc = participant.Name;
				}
				else
				{
					return new PlayerModel
					{
						Id = participant.Id,
						Name = participant.Name,
						Email = participant.NewUserEmail
					};
				}
			}


			var playerModel = JsonConvert.DeserializeObject<PlayerModel>(misc);
			playerModel.Id = participant.Id;

			return playerModel;
		}

		public int Id { get; set; }

		[Required]
		public string Name { get; set; }

		[Required]
		[DataType(DataType.EmailAddress)]
		public string Email { get; set; }

		[Required]
		public int GameRangerId { get; set; }

		[Required]
		public string EAId { get; set; }

	}
}
