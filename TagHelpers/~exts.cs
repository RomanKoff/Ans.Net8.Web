using Ans.Net8.Common;
using Microsoft.AspNetCore.Html;
using Microsoft.AspNetCore.Mvc.TagHelpers;
using Microsoft.AspNetCore.Mvc.ViewFeatures;
using Microsoft.AspNetCore.Razor.TagHelpers;
using Microsoft.Extensions.Configuration;
using System.Text.Encodings.Web;

namespace Ans.Net8.Web.TagHelpers
{

	/*
	 * <a href-email="user@host.ru"></a>
	 * <a href-tel="+7-123-123-12-12"></a>
	 * <a href-site-res="data/nav.json"></a>
	 * <a href-node-res="docs/otchet.pdf"></a>
	 * <a href-page-res="img1.jpg"></a>
	 * <a href-site="sveden/struct"></a>
	 * <a href-node="docs/forms"></a>
	 * <a href-page="list"></a>
	 * 
	 * <img src-site-res="images/logo1.svg"/>
	 * <img src-node-res="persons/head.png"/>
	 * <img src-page-res="img1.jpg"/>
	 */



	[HtmlTargetElement("a", Attributes = Href_Email_AttributeName)]
	[HtmlTargetElement("a", Attributes = Href_Tel_AttributeName)]
	[HtmlTargetElement("a", Attributes = Code_AttributeName)]
	[HtmlTargetElement("a", Attributes = Href_Site_Res_AttributeName)]
	[HtmlTargetElement("a", Attributes = Href_Node_Res_AttributeName)]
	[HtmlTargetElement("a", Attributes = Href_Page_Res_AttributeName)]
	[HtmlTargetElement("a", Attributes = Href_Site_AttributeName)]
	[HtmlTargetElement("a", Attributes = Href_Node_AttributeName)]
	[HtmlTargetElement("a", Attributes = Href_Page_AttributeName)]
	public partial class Exts_ATagHelper(
		IConfiguration config,
		IHtmlGenerator generator,
		CurrentContext current)
		: AnchorTagHelper(generator)
	{

		private const string Href_Email_AttributeName = "href-email";
		private const string Href_Tel_AttributeName = "href-tel";
		private const string Code_AttributeName = "tel-code";
		private const string Href_Site_Res_AttributeName = "href-site-res";
		private const string Href_Node_Res_AttributeName = "href-node-res";
		private const string Href_Page_Res_AttributeName = "href-page-res";
		private const string Href_Site_AttributeName = "href-site";
		private const string Href_Node_AttributeName = "href-node";
		private const string Href_Page_AttributeName = "href-page";

		private readonly LibWebOptions _options = config.GetLibWebOptions();
		private readonly CurrentContext _current = current;


		/* properties */


		[HtmlAttributeName(Href_Email_AttributeName)]
		public string HrefEmailData { get; set; }


		[HtmlAttributeName(Href_Tel_AttributeName)]
		public string HrefTelData { get; set; }


		[HtmlAttributeName(Code_AttributeName)]
		public string CodeData
		{
			get => _code ?? _options.DefaultTelCode;
			set => _code = value;
		}
		private string _code;


		[HtmlAttributeName(Href_Site_Res_AttributeName)]
		public string HrefSiteResData { get; set; }


		[HtmlAttributeName(Href_Node_Res_AttributeName)]
		public string HrefNodeResData { get; set; }


		[HtmlAttributeName(Href_Page_Res_AttributeName)]
		public string HrefPageResData { get; set; }


		[HtmlAttributeName(Href_Site_AttributeName)]
		public string HrefSiteData { get; set; }


		[HtmlAttributeName(Href_Node_AttributeName)]
		public string HrefNodeData { get; set; }


		[HtmlAttributeName(Href_Page_AttributeName)]
		public string HrefPageData { get; set; }


		/* methods */


		public override void Process(
			TagHelperContext context,
			TagHelperOutput output)
		{
			base.Process(context, output);
			output.TagName = "a";
			output.TagMode = TagMode.StartTagAndEndTag;

			if (!string.IsNullOrEmpty(HrefEmailData))
				_makeEmail(output, HrefEmailData);
			else if (!string.IsNullOrEmpty(HrefTelData))
				_makeTel(output, HrefTelData);

			else if (!string.IsNullOrEmpty(HrefSiteResData))
				_makeHref(output, _current.GetResUrl($"site:{HrefSiteResData}"));
			else if (!string.IsNullOrEmpty(HrefNodeResData))
				_makeHref(output, _current.GetResUrl($"node:{HrefNodeResData}"));
			else if (!string.IsNullOrEmpty(HrefPageResData))
				_makeHref(output, _current.GetResUrl($"page:{HrefPageResData}"));

			else if (!string.IsNullOrEmpty(HrefSiteData))
				_makeHref(output, _current.GetUrl($"site:{HrefSiteData}"));
			else if (!string.IsNullOrEmpty(HrefNodeData))
				_makeHref(output, _current.GetUrl($"node:{HrefNodeData}"));
			else if (!string.IsNullOrEmpty(HrefPageData))
				_makeHref(output, _current.GetUrl($"page:{HrefPageData}"));
		}


		/* privates */


		private static void _makeEmail(
			TagHelperOutput output,
			string email)
		{
			var a1 = email.Split('@');
			if (a1.Length == 2)
			{
				var user1 = a1[0];
				var host1 = a1[1];
				var email1 = $"{user1}@{host1}";
				output.Attributes.SetAttribute("href", new HtmlString($"mailto:{email1}"));
				output.Attributes.Add("itemprop", "email");
				output.AddClass("link-email", HtmlEncoder.Default);
				output.AddClass("text-nowrap", HtmlEncoder.Default);
				var s1 = output.GetChildContent();
				output.AppendHtml(string.IsNullOrEmpty(s1) ? email1 : s1);
			}
			else
				_makeError(output, "ERROR EMAIL FORMAT");
		}


		private void _makeTel(
			TagHelperOutput output,
			string tel)
		{
			/*
			 * 3122107         -> +7-812-312-21-07             // местный
			 * +79817321620    -> +7-981-732-16-20             // федеральный
			 * +78137551204    -> +7-813-755-12-04             // настраиваемый
			 * 3122107,1234    -> +7-812-312-21-07 доп. 1234   // с подкодом    
			 */
			var num1 = tel[0] == '+' ? tel : $"{CodeData}{tel}";
			var a1 = num1.Split(','); //.Split('w');
			var num2 = SuppValues.FixTelephoneRuCityCode(
				SuppValues.GetDigitalOnly(a1[0]));
			var href1 = $"+{num2}";
			var tel1 = SuppValues.GetTelephoneNumber(num2);
			if (a1.Length == 2)
			{
				href1 = $"{href1},{a1[1]}";
				tel1 = string.Format(
					Resources.Common.Template_PhoneAddon,
					tel1, a1[1]);
			}
			output.Attributes.SetAttribute("href", new HtmlString($"tel:{href1}"));
			output.Attributes.Add("itemprop", "telephone");
			output.AddClass("link-telephone", HtmlEncoder.Default);
			output.AddClass("text-nowrap", HtmlEncoder.Default);
			output.AppendHtml($"{tel1}{output.GetChildContent().Make(" {0}")}");
		}


		private void _makeHref(
			TagHelperOutput output,
			string url1)
		{
			var href1 = new HtmlString(url1);
			output.Attributes.SetAttribute("href", href1);
			output.AppendHtml(output.GetChildContent());
		}


		private static void _makeError(
			TagHelperOutput output,
			string message)
		{
			output.AppendHtml($"<em>{{{message}}}</em>");
		}

	}



	[HtmlTargetElement("img", Attributes = Src_Site_Res_AttributeName)]
	[HtmlTargetElement("img", Attributes = Src_Node_Res_AttributeName)]
	[HtmlTargetElement("img", Attributes = Src_Page_Res_AttributeName)]
	public partial class Exts_ImgTagHelper(
		IConfiguration config,
		CurrentContext current)
		: _AnsTagHelper_Base(current)
	{

		private const string Src_Site_Res_AttributeName = "src-site-res";
		private const string Src_Node_Res_AttributeName = "src-node-res";
		private const string Src_Page_Res_AttributeName = "src-page-res";

		private readonly LibWebOptions _options = config.GetLibWebOptions();
		private readonly CurrentContext _current = current;


		/* properties */


		[HtmlAttributeName(Src_Site_Res_AttributeName)]
		public string SrcSiteResData { get; set; }


		[HtmlAttributeName(Src_Node_Res_AttributeName)]
		public string SrcNodeResData { get; set; }


		[HtmlAttributeName(Src_Page_Res_AttributeName)]
		public string SrcPageResData { get; set; }


		/* methods */


		public override void Process(
			TagHelperContext context,
			TagHelperOutput output)
		{
			base.Process(context, output);
			output.TagName = "img";
			output.TagMode = TagMode.SelfClosing;

			if (!string.IsNullOrEmpty(SrcSiteResData))
				_makeSrc(output, _current.GetResUrl($"site:{SrcSiteResData}"));
			else if (!string.IsNullOrEmpty(SrcNodeResData))
				_makeSrc(output, _current.GetResUrl($"node:{SrcNodeResData}"));
			else if (!string.IsNullOrEmpty(SrcPageResData))
				_makeSrc(output, _current.GetResUrl($"page:{SrcPageResData}"));
		}


		/* privates */


		private void _makeSrc(
			TagHelperOutput output,
			string url1)
		{
			var src1 = new HtmlString(url1);
			output.Attributes.SetAttribute("src", src1);
		}

	}

}
