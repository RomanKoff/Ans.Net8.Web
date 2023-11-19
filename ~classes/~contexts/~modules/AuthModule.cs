using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Authorization.Policy;

namespace Ans.Net8.Web
{

	public class AuthModule
		: _ContextModule_Base
	{

		private readonly IAuthorizationPolicyProvider _policyProvider;
		private readonly IPolicyEvaluator _policyEvaluator;


		/* ctor */


		public AuthModule(
			ICurrentContext current,
			IAuthorizationPolicyProvider policyProvider,
			IPolicyEvaluator policyEvaluator)
			: base(current)
		{
			_policyProvider = policyProvider;
			_policyEvaluator = policyEvaluator;
		}


		/* readonly properties */


		public bool IsApp
			=> IsAuthAsync(_Consts.AUTH_POLICY_APP, null, null).Result;

		public bool IsAdmin
			=> IsAuthAsync(_Consts.AUTH_POLICY_APP_ADMINS, null, null).Result;

		public bool IsModerator
			=> IsAuthAsync(_Consts.AUTH_POLICY_APP_MODERATORS, null, null).Result;

		public bool IsEditor
			=> IsAuthAsync(_Consts.AUTH_POLICY_APP_EDITORS, null, null).Result;

		public bool IsReader
			=> IsAuthAsync(_Consts.AUTH_POLICY_APP_READERS, null, null).Result;

		public bool IsUser
			=> IsAuthAsync(_Consts.AUTH_POLICY_APP_USERS, null, null).Result;


		/* functions */


		public async Task<bool> IsAuthAsync(
			string policy,
			string roles,
			string authenticationSchemes)
		{
			if (!_current.HttpContext.User.Identity.IsAuthenticated)
				return false;
			var policy1 = await AuthorizationPolicy.CombineAsync(
				_policyProvider, new[] {
					new AuthorizeData {
						Policy = policy,
						Roles = roles,
						AuthenticationSchemes = authenticationSchemes
					}
				});
			var authenticate1 = await _policyEvaluator.AuthenticateAsync(
				policy1, _current.HttpContext);
			var authorize1 = await _policyEvaluator.AuthorizeAsync(
				policy1, authenticate1, _current.HttpContext, null);
			return authorize1.Succeeded;
		}


		public bool IsRoles(
			string roles)
		{
			return IsAuthAsync(null, roles, null).Result;
		}

	}



	public class AuthorizeData
		: IAuthorizeData
	{
		public string Policy { get; set; }
		public string Roles { get; set; }
		public string AuthenticationSchemes { get; set; }
	}

}
