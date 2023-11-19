using Microsoft.Extensions.Configuration;

namespace Ans.Net8.Web.Services
{

	/*
	 *	Add_AnsWeb()
	 *		_addSsoAuthentication()
	 *			builder.Services.AddScoped<IRolesProvider, RolesProvider_Ans>();
	 */



	public interface IRolesProvider
	{
		string[] GetRoles(string username);
	}



	public class RolesProvider_Ans
		: IRolesProvider
	{
		private readonly SsoUsers[] _usersRoles;

		public RolesProvider_Ans(
			IConfiguration configuration)
		{
			_usersRoles = configuration.GetLibOptions().Sso?.Users;
		}

		public string[] GetRoles(
			string username)
		{
			if (_usersRoles == null)
				return null;
			return _usersRoles
				.FirstOrDefault(x => x.Username == username)?
				.Roles;
		}
	}

}
