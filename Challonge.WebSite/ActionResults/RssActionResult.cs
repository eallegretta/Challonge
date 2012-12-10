﻿using System;
using System.Collections.Generic;
using System.ServiceModel.Syndication;
using System.Web;
using System.Web.Mvc;
using System.Xml;

namespace Challonge.WebSite.ActionResults
{
	public class RssActionResult : ActionResult
	{
		public SyndicationFeed Feed { get; set; }

		public override void ExecuteResult(ControllerContext context)
		{
			context.HttpContext.Response.ContentType = "application/rss+xml";

			Rss20FeedFormatter rssFormatter = new Rss20FeedFormatter(Feed);
			using (XmlWriter writer = XmlWriter.Create(context.HttpContext.Response.Output))
			{
				rssFormatter.WriteTo(writer);
			}
		}
	}

}
