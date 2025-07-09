using Ans.Net8.Common;
using Microsoft.AspNetCore.Html;
using System.Text;

namespace Ans.Net8.Web
{

	public class HiddenInputsHtml
	{

		/* ctor */


		public HiddenInputsHtml(
			string name,
			string[] value)
		{
			Name = name;
			Value = value;
		}


		/* readonly properties */


		public string Name { get; }
		public string[] Value { get; }


		/* functions */


		public override string ToString()
		{
			var sb1 = new StringBuilder();
			foreach (var item1 in Value)
				sb1.Append($"<input type=\"hidden\" name=\"{Name}[]\"{item1.Make(" value=\"{0}\"")} />");
			return sb1.ToString();
		}


		public HtmlString ToHtml()
		{
			return new HtmlString(ToString());
		}

	}

}
