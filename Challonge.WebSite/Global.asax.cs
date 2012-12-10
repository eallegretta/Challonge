/*
 * Created by SharpDevelop.
 * User: Paleta
 * Date: 09/12/2012
 * Time: 20:09
 * 
 * To change this template use Tools | Options | Coding | Edit Standard Headers.
 */
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;

namespace Challonge.WebSite
{
	public class MvcApplication : HttpApplication
	{
		public static void RegisterRoutes(RouteCollection routes)
		{
			routes.Ignore("{resource}.axd/{*pathInfo}");
			
			routes.MapRoute(
				"Default",
				"{controller}/{action}/{id}",
				new {
					controller = "Home",
					action = "Index",
					id = UrlParameter.Optional
				});
		}
		
		protected void Application_Start()
		{
			AreaRegistration.RegisterAllAreas();
			
			ViewEngines.Engines.Clear();
			ViewEngines.Engines.Add(new RazorViewEngine());
			
			RegisterRoutes(RouteTable.Routes);
		}
	}
}
