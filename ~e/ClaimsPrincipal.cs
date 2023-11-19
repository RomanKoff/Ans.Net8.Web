using System.Security.Claims;

namespace Ans.Net8.Web
{

	public static partial class _e
	{

		/*
		 * void AddRole(this ClaimsIdentity identity, ClaimsPrincipal principal, string role);
         * void AddRoles(this ClaimsPrincipal principal, params string[] roles);
         * 
         * string GetEmail(this ClaimsPrincipal principal);
         * string GetName(this ClaimsPrincipal principal);
         * IEnumerable<string> GetRoles(this ClaimsPrincipal identity);
         * IEnumerable<string> GetClaims(this ClaimsPrincipal identity, string typePrefix)
         */


		/* methods */


		public static void AddRole(
			this ClaimsIdentity identity,
			ClaimsPrincipal principal,
			string role)
		{
			if (!principal.HasClaim(
				x => x.Type == ClaimTypes.Role && x.Value == role))
				identity.AddClaim(new Claim(ClaimTypes.Role, role));
		}


		public static void AddRoles(
			this ClaimsPrincipal principal,
			params string[] roles)
		{
			var identity1 = new ClaimsIdentity();
			if (roles != null && roles.Any())
				foreach (var role in roles)
					identity1.AddRole(principal, role);
			principal.AddIdentity(identity1);
		}


		/* functions */


		public static string GetEmail(
			this ClaimsPrincipal principal)
		{
			return principal.Claims.FirstOrDefault(
				x => x.Type.Equals(ClaimTypes.Email))?.Value;
		}


		public static string GetName(
			this ClaimsPrincipal principal)
		{
			return principal.Claims.FirstOrDefault(
				x => x.Type.Equals("name"))?.Value;
		}


		public static IEnumerable<string> GetRoles(
			this ClaimsPrincipal identity)
		{
			return identity.Claims
				.Where(x => x.Type == ClaimTypes.Role)
				.Select(x => x.Value);
		}


		public static IEnumerable<string> GetClaims(
			this ClaimsPrincipal identity,
			string typePrefix)
		{
			return identity.Claims
				.Where(x => x.Type.StartsWith(typePrefix))
				.Select(x => $"{x.Type} {x.Value}");
		}





		/*
		public static bool TestAuthorizePolicy(
			this IAuthorizationService authorization,
			ClaimsPrincipal principal,
			string policy)
		{
			return authorization
				.AuthorizeAsync(principal, policy)
					.Result.Succeeded;
		}
		*/

	}

}
