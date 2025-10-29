using Ans.Net8.Common;
using Microsoft.AspNetCore.Html;

namespace Ans.Net8.Web
{

	public class DictHtml
		: Dict<HtmlString>,
		IDictionary<string, HtmlString>
	{
		public DictHtml()
			: base()
		{
		}

		public DictHtml(
			string serialization)
			: base(serialization)
		{
		}

		public bool UseTypograf { get; set; } = true;

		public override HtmlString ToValue(
			string value)
		{
			return value.ToHtml(UseTypograf);
		}
	}

}
