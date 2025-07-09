using Ans.Net8.Common;
using Microsoft.AspNetCore.Html;

namespace Ans.Net8.Web
{

	public class QueryStringService
		: QueryStringHelper
	{

		private readonly CurrentContext _current;


		/* ctor */


		public QueryStringService(
			CurrentContext current)
			: base(current.HttpContext.Request.Query)
		{
			_current = current;
		}


		/* functions */


		public QueryStringHelper GetHelper(
			params string[] ignoreParams)
		{
			return new(_current.HttpContext.Request.Query, ignoreParams);
		}


		public HtmlString GetSortingButton(
			string name,
			string innerHtml,
			bool useTypograf)
		{
			var qs1 = GetHelper();
			var nameDesc1 = $"-{name}";
			if (useTypograf)
				innerHtml = SuppTypograph.GetTypografMin(innerHtml);
			var css1 = new TagClassBuilder("text-nowrap", "sorting");
			string inner1;
			if (TestValue("order", name))
			{
				qs1.Append("order", nameDesc1);
				css1.Append("sorting-asc");
				inner1 = $"<span class=\"text-wrap\">{innerHtml}</span><i class=\"bi-arrow-down\"></i>";
			}
			else if (TestValue("order", nameDesc1))
			{
				qs1.Append("order", name);
				css1.Append("sorting-desc");
				inner1 = $"<span class=\"text-wrap\">{innerHtml}</span><i class=\"bi-arrow-up\"></i>";
			}
			else
			{
				qs1.Remove("page");
				qs1.Append("order", name);
				inner1 = $"<span class=\"text-wrap\">{innerHtml}</span>";
			}
			return new HtmlString(
				$"<a class=\"{css1}\" href=\"{qs1.MakeQueryString("")}\">{inner1}</a>");
		}

	}

}
