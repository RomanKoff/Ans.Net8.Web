using Microsoft.AspNetCore.Authentication;
using Microsoft.Extensions.Configuration;
using System.Security.Claims;

namespace Ans.Net8.Web.Services
{

	/*
	 *	Add_AnsWeb()
	 *		_addSsoAuthentication()
	 *			builder.Services.AddScoped<IRolesProvider, RolesProvider_Ans>();
     *          builder.Services.AddTransient<IClaimsTransformation, ClaimsRoles_Ans>();
	 */



	public interface IRolesProvider
	{
		string[] GetRoles(string username);
	}



	public class RolesProvider_Ans(
		IConfiguration configuration)
		: IRolesProvider
	{
		private readonly SsoUsers[] _usersRoles = configuration.GetLibOptions().Sso?.Users;

		public string[] GetRoles(
			string username)
		{
			return _usersRoles?
				.FirstOrDefault(x => x.Username == username)?
				.Roles;
		}
	}



	public class ClaimsRoles_Ans(
		IRolesProvider rolesProvider)
		: IClaimsTransformation
	{
		private readonly IRolesProvider _rolesProvider = rolesProvider;

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
