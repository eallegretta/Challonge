﻿using System;
using System.Collections.Generic;
using System.Text;
using System.Xml.Serialization;
using Challonge.Backend.Entities;
using Newtonsoft.Json;

namespace Challonge.Backend.Services
{
	public class TournamentResponse
	{
		[JsonProperty("tournament")]
		public Tournament Tournament { get; set; }
	}
}
