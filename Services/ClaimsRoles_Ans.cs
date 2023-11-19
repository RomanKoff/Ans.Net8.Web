using Microsoft.AspNetCore.Authentication;
using System.Security.Claims;

namespace Ans.Net8.Web.Services
{

	/*
	 *  Add_AnsWeb()
	 *      _addSsoAuthentication()
     *          builder.Services.AddTransient<IClaimsTransformation, ClaimsRoles_Ans>();
     */



	public class ClaimsRoles_Ans
		: IClaimsTransformation
	{
		private readonly IRolesProvider _rolesProvider;

		public ClaimsRoles_Ans(
			IRolesProvider rolesProvider)
		{
			_rolesProvider = rolesProvider;
		}

		public Task<ClaimsPrincipal> TransformAsync(
			ClaimsPrincipal principal)
		{
			if (principal.Identity.IsAuthenticated)
			{
				var roles1 = _rolesProvider.GetRoles(principal.Identity.Name);
				principal.AddRoles(roles1);
			}
			return Task.FromResult(principal);
		}
	}

}
