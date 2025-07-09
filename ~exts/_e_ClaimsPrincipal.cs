using System.Security.Claims;

namespace Ans.Net8.Web
{

	public static partial class _e_ClaimsPrincipal
	{

		public static TestClaimsActionHelper GetTestClaimsActionHelper(
			this ClaimsPrincipal principal,
			string catalog,
			params string[] controllers)
		{
			return new TestClaimsActionHelper(
				principal, catalog, controllers);
		}


		public static bool HasActionClaim(
			this ClaimsPrincipal principal,
			string value)
		{
			return principal.HasClaim(
				_Consts.CLAIM_ACTIONS_TYPE, value);
		}


		public static bool HasResourceClaim(
			this ClaimsPrincipal principal,
			string value)
		{
			return principal.HasClaim(
				_Consts.CLAIM_RESOURCES_TYPE, value);
		}


		public static bool TestClaimsAction(
			this ClaimsPrincipal principal,
			string catalog,
			string controller,
			string action)
		{
			var action1 = $"{controller}.{action}";
			var notAction1 = $"!{action1}";
			if (principal.HasActionClaim(catalog))
				return !(principal.HasActionClaim($"!{controller}")
					|| principal.HasActionClaim(notAction1));
			if (principal.HasActionClaim(controller))
				return !principal.HasActionClaim(notAction1);
			return principal.HasActionClaim(action1);
		}


		public static bool TestClaimsAction(
			this ClaimsPrincipal principal,
			bool allowCatalog,
			string controller,
			string action)
		{
			var action1 = $"{controller}.{action}";
			var notAction1 = $"!{action1}";
			if (allowCatalog)
				return !(principal.HasActionClaim($"!{controller}")
					|| principal.HasActionClaim(notAction1));
			if (principal.HasActionClaim(controller))
				return !principal.HasActionClaim(notAction1);
			return principal.HasActionClaim(action1);
		}

	}

}
