using Ans.Net8.Common;
using Microsoft.AspNetCore.Html;

namespace Ans.Net8.Web
{

	public class DictHtml
		: _Dict_Proto<string, HtmlString>
	{

		/* ctors */


		public DictHtml()
			: base()
		{
		}


		public DictHtml(
			string serialization)
			: base(serialization)
		{
		}


		public DictHtml(
			IEnumerable<string> serialization)
			: base(serialization)
		{
		}


		/* overrides */


		public override string StringToKey(
			string key)
		{
			return key;
		}


		public override HtmlString StringToValue(
			string value)
		{
			return value.ToHtml(UseTypograf);
		}


		public override string KeyToString(
			string key)
		{
			return key;
		}


		public override string ValueToString(
			HtmlString value)
		{
			return value.ToString();
		}


		/* properties */


		public bool UseTypograf { get; set; } = true;

	}

}
