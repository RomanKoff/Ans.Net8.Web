using Microsoft.AspNetCore.Mvc;

namespace Ans.Net8.Web.Controllers
{

	public class _NodeController_Base(
		CurrentContext current)
		: Controller
	{

		/* actions */


		public virtual IActionResult Page(
			string path)
		{
			var s1 = current.Request.NodesParsePath(path);
			return s1 switch
			{
				null => NotFound(),
				"" => View($"/Views/Nodes{current.Request.ViewPath}.cshtml"),
				_ => Redirect(s1)
			};
		}

	}

}
