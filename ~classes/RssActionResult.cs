using Microsoft.AspNetCore.Mvc;
using System.ServiceModel.Syndication;
using System.Text;
using System.Xml;

namespace Ans.Net8.Web
{

	public class RssActionResult
		: ActionResult
	{

		/* properties */


		public SyndicationFeed Feed { get; set; }


		/* methods */


		public override void ExecuteResult(
			ActionContext context)
		{
			context.HttpContext.Response.ContentType = "application/rss+xml";
			var rss1 = new Rss20FeedFormatter(Feed);
			using var writer1 = XmlWriter.Create(
				context.HttpContext.Response.Body,
				new XmlWriterSettings()
				{
					Async = false,
					Encoding = Encoding.UTF8
				});
			rss1.WriteTo(writer1);
		}

	}

}
