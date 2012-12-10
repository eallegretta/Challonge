using System;
using System.Collections.Generic;
using System.Web;
using System.Web.Configuration;
using System.Web.UI;
using System.IO;
using System.Web.Mvc;

namespace Challonge.WebSite.Extensions
{
	public static class CaptchaExtensions
	{
		public static string GenerateCaptcha(this HtmlHelper helper)
		{
			var captchaControl = new Recaptcha.RecaptchaControl
			{
				ID = "recaptcha",
				Theme = "blackglass",
				PublicKey = WebConfigurationManager.AppSettings["CaptchaPublicKey"],
				PrivateKey = WebConfigurationManager.AppSettings["CaptchaPrivateKey"]
			};

			var htmlWriter = new HtmlTextWriter(new StringWriter());

			captchaControl.RenderControl(htmlWriter);

			return htmlWriter.InnerWriter.ToString();
		}
	}
}
