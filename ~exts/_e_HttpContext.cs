using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Authorization.Policy;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;

namespace Ans.Net8.Web
{

	public class AuthorizeDataModel
		: IAuthorizeData
	{
		public string Policy { get; set; }
		public string Roles { get; set; }
		public string AuthenticationSchemes { get; set; }
	}



	public static partial class _e_HttpContext
	{

		/* functions */


		private static string _baseUrl;
		public static string GetBaseUrl(
			this HttpContext context)
		{
			return _baseUrl ??= $"{context.Request.Scheme}://{context.Request.Host}";
		}


		private static string _virtualPath;
		public static string GetVirtualPath(
			this HttpContext context)
		{
			return _virtualPath ??= context.Request.PathBase; // $"{context.Request.PathBase}";
		}


		private static string _applicationUrl;
		public static string GetApplicationUrl(
			this HttpContext context)
		{
			return _applicationUrl ??= $"{context.GetBaseUrl()}{context.GetVirtualPath()}";
		}


		public static bool IsClaimsAdmin(
			this HttpContext context)
		{
			return context.TestClaimsPolicy(
				_Consts.AUTH_POLICY_ADMINS);
		}


		public static bool IsClaimsModerator(
			this HttpContext context)
		{
			return context.TestClaimsPolicy(
				_Consts.AUTH_POLICY_MODERATORS);
		}


		public static bool IsClaimsWriter(
			this HttpContext context)
		{
			return context.TestClaimsPolicy(
				_Consts.AUTH_POLICY_WRITERS);
		}


		public static bool IsClaimsReader(
			this HttpContext context)
		{
			return context.TestClaimsPolicy(
				_Consts.AUTH_POLICY_READERS);
		}


		public static bool IsClaimsUser(
			this HttpContext context)
		{
			return context.TestClaimsPolicy(
				_Consts.AUTH_POLICY_USERS);
		}


		public static bool TestClaimsPolicy(
			this HttpContext context,
			string policy)
		{
			return context.TestClaimsAsync(policy, null).Result;
		}


		public static bool TestClaimsRoles(
			this HttpContext context,
			string roles)
		{
			return context.TestClaimsAsync(null, roles).Result;
		}


		public static async Task<bool> TestClaimsAsync(
			this HttpContext context,
			string policy,
			string roles)
		{
			if (!context.User.Identity.IsAuthenticated)
				return false;
			var policyProvider1 = context.RequestServices
				.GetService<IAuthorizationPolicyProvider>();
			var policyEvaluator1 = context.RequestServices
				.GetService<IPolicyEvaluator>();
			var policy1 = await AuthorizationPolicy.CombineAsync(
				policyProvider1,
				[new AuthorizeDataModel {
					Policy = policy?.ToString(),
					Roles = roles?.ToString(),
					AuthenticationSchemes = null // authenticationSchemes
				}]);
			var authenticate1 = await policyEvaluator1.AuthenticateAsync(
				policy1, context);
			var authorize1 = await policyEvaluator1.AuthorizeAsync(
				policy1, authenticate1, context, null);
			return authorize1.Succeeded;
		}

	}

}
