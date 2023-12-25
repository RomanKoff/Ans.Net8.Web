using Ans.Net8.Common;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace Ans.Net8.Web
{

	public class FormHelper(
		IHtmlHelper helper)
		: _HtmlWrapper_Base(helper)
	{

		/* functions */


		public string GetModelErrors()
		{
			var errors1 = ModelState.GetModelErrors();
			if (!(errors1?.Length > 0))
				return null;
			return errors1.MakeFromCollection(
				x => x, "<div class=\"alert alert-danger\">{0}</div>", "<p>{0}</p>", null);
		}


		public string GetFieldErrors(
			string name)
		{
			var errors1 = ModelState.GetFieldErrors(name);
			if (!(errors1?.Length > 0))
				return null;
			return errors1.MakeFromCollection(
				x => x, null, "<p class=\"text-danger\">{0}</p>", null);
		}


		public FieldHelper GetFieldHelper(
			string name)
		{
			return new FieldHelper(this, name);
		}

	}

}