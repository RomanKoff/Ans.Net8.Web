using Ans.Net8.Common;
using System.Text;

namespace Ans.Net8.Web
{

	public class WidthWrapper(
		string html,
		WidthsEnum width,
		string cssClass)
	{

		public string Html { get; } = html;
		public WidthsEnum Width { get; } = width;
		public string CssClass { get; } = cssClass;


		public override string ToString()
		{
			var sb1 = new StringBuilder();
			sb1.Append(Width switch
			{
				WidthsEnum.Full => "w-100",
				WidthsEnum.ExtraLarge => "w-40rem",
				WidthsEnum.Large => "w-30rem",
				WidthsEnum.Medium => "w-20rem",
				WidthsEnum.Small => "w-15rem",
				WidthsEnum.ExtraSmall => "w-10rem",
				_ => "w-6rem"
			});
			if (!string.IsNullOrEmpty(CssClass))
			{
				if (sb1.Length > 0)
					sb1.Append(' ');
				sb1.Append(CssClass);
			}
			return (sb1.Length == 0)
				? Html
				: $"<div class='{sb1}'>{Html}</div>";
		}

	}

}
