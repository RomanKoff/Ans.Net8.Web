using Ans.Net8.Common;
using Microsoft.AspNetCore.Html;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.Resources;

namespace Ans.Net8.Web.Forms
{

	public static partial class _e
	{
		public static HtmlString ToHtml(
			this IFormCellControl control)
		{
			return control.ToString().ToHtml();
		}
	}



	public interface IFormCellControl
	{
		string ToString();
	}



	public interface IFormFieldControl
		: IFormCellControl
	{
		string Name { get; }
	}



	public interface IFormViewControl : IFormFieldControl { }
	public interface IFormEditControl : IFormFieldControl { }



	public class FormHelper
	{

		private readonly Dictionary<string, CrudFaceHelper> _faces = [];


		/* ctor */


		public FormHelper(
			IHtmlHelper helper,
			params ResourceManager[] resources)
		{
			Helper = helper;
			Res = new FormResources(resources);
		}


		/* readonly properties */


		public IHtmlHelper Helper { get; }
		public FormResources Res { get; }


		/* functions */


		public CrudFaceHelper Face(
			string name)
		{
			if (_faces.TryGetValue(name, out CrudFaceHelper face1))
				return face1;
			var face2 = Res.GetFaceHelper(name);
			_faces.Add(name, face2);
			return face2;
		}


		public HtmlString AddCell(
			IFormCellControl control,
			string cssClasses = null,
			string styles = null,
			string attributes = null)
		{
			var tag1 = new TagBuilderExt("td", TagRenderMode.Normal);
			tag1.Prepare(cssClasses, styles, attributes);
			tag1.InnerHtml.AppendHtml(control.ToString());
			return tag1.ToHtml();
		}


		public HtmlString AddView(
			IFormViewControl control)
		{
			return _addField(control, false, null);
		}


		public HtmlString AddEdit(
			IFormEditControl control,
			bool isRequired = false)
		{
			var errors1 = Helper.ViewContext.ModelState.GetFieldErrors(control.Name);
			return _addField(control, isRequired, errors1);
		}


		/* privates */


		private HtmlString _addField(
			IFormFieldControl control,
			bool isRequired,
			string[] errors)
		{
			var tag1 = new TagBuilderExt("div", TagRenderMode.Normal);
			tag1.AddCssClass("form-field");
			var face1 = Res?.GetFaceHelper(control.Name);
			var label1 = new LabelFieldTag(control.Name, isRequired, face1, errors);
			tag1.InnerHtml.AppendHtmlLine(label1.ToString());
			if (face1.HasDescription)
				tag1.InnerHtml.AppendHtmlLine(
					$"<div id=\"{control.Name}_desc\" class=\"form-text\">{SuppTypograph.GetTypografMin(face1.Description)}</div>");
			if (face1.HasSample)
				tag1.InnerHtml.AppendHtmlLine(
					$"<div id=\"{control.Name}_sample\" class=\"form-text\">Пример: <code>{face1.Sample}</code></div>");
			tag1.InnerHtml.AppendHtmlLine(control.ToString());
			return tag1.ToHtml();
		}

	}

}
