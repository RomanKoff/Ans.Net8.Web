using System.Security.Claims;

namespace Ans.Net8.Web
{

	public class TestClaimsActionHelper
	{

		public TestClaimsActionHelper(
			ClaimsPrincipal principal,
			string catalog,
			params string[] controllers)
		{
			var allowCatalog1 = principal.HasActionClaim(catalog);
			foreach (var item1 in controllers)
				AllowControllers.Add(
					item1, principal.TestClaimsAction(allowCatalog1, item1, null));
		}


		/* readonly properties */


		public Dictionary<string, bool> AllowControllers { get; } = [];


		public bool AllowCatalog
		{
			get
			{
				foreach (var item1 in AllowControllers)
					if (item1.Value)
						return true;
				return false;
			}
		}


		/* functions */


		public bool TestAllowController(
			string controller)
		{
			if (AllowControllers.TryGetValue(controller, out var allow))
				return allow;
			return false;
		}

	}

}
