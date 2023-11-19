using Ans.Net8.Web.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ViewEngines;

namespace Ans.Net8.Web.Controllers
{

	public class _NodesController_Base
		: Controller
	{

		/* ctors */


		public _NodesController_Base(
			ICurrentContext current)
		{
			Current = current;
		}


		/* readoly properties */



		public ICurrentContext Current { get; private set; }


		/* actions */


		public virtual IActionResult Index(
			string path)
		{
			if (!Current.Request.Parser(path))
				return NotFound();			
			return View($"~/Views/Nodes{Current.Request.ViewPath}.cshtml");
		}

	}

}
