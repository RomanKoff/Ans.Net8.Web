using Microsoft.AspNetCore.Mvc;

namespace Ans.Net8.Web.Controllers
{

	public class _NodesController_Base(
		ICurrentContext current)
		: Controller
	{

		/* readonly properties */


		public ICurrentContext Current { get; private set; } = current;


		/* actions */


		public virtual IActionResult Index(
			string path)
		{
			return Current.Request.ParserPage(path)
				? View($"~/Views/Nodes{Current.Request.ViewPath}.cshtml")
				: NotFound();
		}

	}

}
