using Ans.Net8.Common;
using Microsoft.AspNetCore.Razor.TagHelpers;
using Microsoft.Extensions.Configuration;
using System.Text;
using System.Text.RegularExpressions;

namespace Ans.Net8.Web.TagHelpers
{

	/*
	 * <ans-link email="" tel="" code="">content</ans-link>
	 */



	public partial class AnsLinkTagHelper
		: TagHelper
	{
		[GeneratedRegex("[^0-9]+")]
		private static partial Regex _regexNumbs();

		private readonly LibOptions _options;
		private string _content;

		public AnsLinkTagHelper(
			IConfiguration configuration)
		{
			_options = configuration.GetLibOptions();
		}

		public string Email { get; set; }
		public string Tel { get; set; }

		public string Code
		{
			get => _code ?? _options.DefaultTelCode;
			set => _code = value;
		}
		private string _code;

		public override void Process(
			TagHelperContext context,
			TagHelperOutput output)
		{
			output.TagMode = TagMode.StartTagAndEndTag;
			_content = output.GetChildContentAsync().Result.GetContent();
			if (!string.IsNullOrEmpty(Email))
			{
				_outEmail(output, Email);
				return;
			}
			if (!string.IsNullOrEmpty(Tel))
			{
				_outTel(output, Tel);
				return;
			}
			output.TagName = "em";
			output.Content.Append("{ERROR LINK FORMAT}");
		}

		private static string _filter(
			string number)
		{
			return _regexNumbs().Replace(number, "");
		}

		private void _outEmail(
			TagHelperOutput output,
			string email)
		{
			var a1 = email.Split(new char[] { '@' });
			if (a1.Length == 2)
			{
				output.TagName = "a";
				string user = a1[0];
				string host = a1[1];
				string s1 = $"{user}@{host}";
				output.Attributes.Add("href", $"mailto:{s1}");
				output.Attributes.Add("class", "link-email text-nowrap");
				if (string.IsNullOrEmpty(_content))
					output.Content.AppendHtml(s1);
				else
					output.Content.AppendHtml(_content);
				return;
			}
			output.TagName = "em";
			output.Content.Append("{ERROR EMAIL FORMAT}");
			return;
		}

		private void _outTel(
			TagHelperOutput output,
			string tel)
		{
			// 3122107         -> +7-812-312-21-07             // местный
			// +79817321620    -> +7-981-732-16-20             // федеральный
			// +78137551204    -> +7-813-755-12-04             // настраиваемый
			// 3122107w1234    -> +7-812-312-21-07 доп. 1234   // с подкодом            
			var num1 = tel[0] == '+' ? tel : $"{Code}{tel}";
			var a1 = num1.Split(','); //.Split('w');
			var num2 = _filter(a1[0]);
			var num_href = new StringBuilder($"+{num2}");
			var num_cont = new StringBuilder($"+{SuppFormat.ToPhone(num2)}");
			if (a1.Length == 2)
			{
				num_href.Append($",{a1[1]}"); //.Append($"w{a1[1]}");
				num_cont.Append($" *{a1[1]}");
			}
			output.TagName = "a";
			output.Attributes.Add("class", "link-tel text-nowrap");
			output.Attributes.Add("itemprop", "telephone");
			output.Attributes.Add("href", $"tel:{num_href}".ToHtml());
			output.Content.AppendHtml($"{num_cont}{_content}");
			return;
		}
	}

}
