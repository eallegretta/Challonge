using System;
using System.Collections.Generic;
using System.Web;
using System.Security.Principal;
using System.Web.Configuration;
using System.Web.Security;
using MySql.Data.MySqlClient;
using System.Web.SessionState;
using System.Configuration;

namespace Challonge.WebSite.HttpModules
{
	public class FFVAAuthenticationHandler : IHttpHandler, IRequiresSessionState
	{
		#region IHttpHandler Members

		public bool IsReusable
		{
			get { return true; }
		}

		private string PhpSessionId
		{
			get
			{
				object sessionId = HttpContext.Current.Session["phpSessionId"];
				if (sessionId == null)
					return null;

				return sessionId.ToString();
			}
			set { HttpContext.Current.Session["phpSessionId"] = value; }
		}

		private string Username
		{
			get
			{
				object username = HttpContext.Current.Session["Username"];
				if(username == null)
					return null;

				return username.ToString();
			}
			set { HttpContext.Current.Session["Username"] = value; }
		}

		public void ProcessRequest(HttpContext context)
		{

			if (PhpSessionId == null)
				PhpSessionId = context.Request.QueryString["sid"];

			if (Username == null)
				Username = context.Request.QueryString["username"];

			if (Username != null)
			{
				FormsAuthentication.RedirectFromLoginPage(Username, false);
			}
			else
			{
				try
				{
					if (PhpSessionId == null)
						throw new Exception("Invalid Session Id");

					var connectionString = ConfigurationManager.ConnectionStrings["FFVA"].ConnectionString;

					using (var cn = new MySqlConnection(connectionString))
					{
						string joomlaSessionsTable = WebConfigurationManager.AppSettings["JoomlaSessionsTable"];
						
						cn.Open();

						using (var cmd = cn.CreateCommand())
						{
							cmd.CommandText = "select username from " +  joomlaSessionsTable + " where session_id = ?sid";

							cmd.Parameters.Add(new MySqlParameter("?sid", PhpSessionId));

							using (var reader = cmd.ExecuteReader())
							{
								if (reader.Read())
								{
									object username = reader["username"];
									if (username != null && username != DBNull.Value)
										FormsAuthentication.RedirectFromLoginPage(username.ToString(), false);
									else
										throw new Exception("Username is null");
								}
							}
						}
					}
				}
				catch (Exception ex)
				{
					context.Response.ContentType = "text/html";
					context.Response.Write("<html>");
					context.Response.Write("<body>");
					context.Response.Write("Por favor logueese al sitio y vuelva a acceder al torneo ");
					context.Response.Write("<div style='display:none'>" + ex.Message + "</div>");
					context.Response.Write("<div style='display:none'>" + ex.StackTrace + "</div>");
					context.Response.Write("</body>");
					context.Response.Write("</html>");
				}
			}

		}

		#endregion
	}
}
