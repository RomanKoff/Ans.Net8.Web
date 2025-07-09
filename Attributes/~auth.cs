using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using System.Security.Claims;

namespace Ans.Net8.Web.Attributes
{

	public class ActionAccessAttribute
		: TypeFilterAttribute
	{
		public ActionAccessAttribute(
			string catalog,
			string controller,
			string action)
			: base(typeof(ActionAccessFilter))
		{
			Arguments = [catalog, controller ?? "", action ?? ""];
		}
	}



	public class ActionAccessFilter(
		string catalog,
		string controller,
		string action)
		: IAuthorizationFilter
	{

		private readonly string _catalog = catalog;
		private readonly string _controller = controller;
		private readonly string _action = action;


		/* methods */


		public void OnAuthorization(
			AuthorizationFilterContext context)
		{
			var context1 = context.HttpContext;
			var user1 = context1.User;
			if (context1.IsClaimsAdmin()
				|| user1.TestClaimsAction(_catalog, _controller, _action))
				return;
			context.Result = new ForbidResult();
		}

	}



	public class ClaimRequirementAttribute
		: TypeFilterAttribute
	{
		public ClaimRequirementAttribute(
			string claimType,
			string claimValue)
			: base(typeof(ClaimRequirementFilter))
		{
			Arguments = [new Claim(claimType, claimValue)];
		}
	}



	public class ClaimRequirementFilter(
		Claim claim)
		: IAuthorizationFilter
	{
		private readonly Claim _claim = claim;

		public void OnAuthorization(
			AuthorizationFilterContext context)
		{
			var hasClaim1 = context.HttpContext.User.Claims.Any(
				x => x.Type == _claim.Type && x.Value == _claim.Value);
			if (!hasClaim1)
				context.Result = new ForbidResult();
		}
	}

}