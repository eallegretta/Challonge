using System;
using System.Web.Mvc;

namespace Challonge.WebSite.Controllers
{
	/// <summary>
	/// Description of Caching.
	/// </summary>
	public class Caching : Controller
	{
		public string Clear()
		{
			return "Cache borrado";
		}
	}
}