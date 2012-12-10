using System.Web.Mvc;

namespace Challonge.WebSite.Areas.Admin
{
	public class AdminAreaRegistration : AreaRegistration
	{
		public override string AreaName
		{
			get
			{
				return "Admin";
			}
		}

		public override void RegisterArea(AreaRegistrationContext context)
		{
			context.MapRoute(
				"Admin_default",
				"Admin.aspx/{controller}/{action}/{id}",
				new { action = "Index", id = UrlParameter.Optional }
			);
		}
	}
}
