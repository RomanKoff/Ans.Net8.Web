using Microsoft.AspNetCore.Authorization;

namespace Ans.Net8.Web
{

	public static partial class __e_auth
	{

		/* methods */


		public static void AddAnsPolicies(
			this AuthorizationOptions options)
		{
			options.AddPolicy(
				_Consts.AUTH_POLICY_ADMINS,
				x => x.RequireClaim(_Consts.CLAIM_AUTH_POLICY_TYPE,
					_Consts.AUTH_POLICY_ADMINS_VALUE));

			options.AddPolicy(
				_Consts.AUTH_POLICY_MODERATORS,
				x => x.RequireClaim(_Consts.CLAIM_AUTH_POLICY_TYPE,
					_Consts.AUTH_POLICY_ADMINS_VALUE,
					_Consts.AUTH_POLICY_MODERATORS_VALUE));

			options.AddPolicy(
				_Consts.AUTH_POLICY_WRITERS,
				x => x.RequireClaim(_Consts.CLAIM_AUTH_POLICY_TYPE,
					_Consts.AUTH_POLICY_ADMINS_VALUE,
					_Consts.AUTH_POLICY_MODERATORS_VALUE,
					_Consts.AUTH_POLICY_WRITERS_VALUE));

			options.AddPolicy(
				_Consts.AUTH_POLICY_READERS,
				x => x.RequireClaim(_Consts.CLAIM_AUTH_POLICY_TYPE,
					_Consts.AUTH_POLICY_ADMINS_VALUE,
					_Consts.AUTH_POLICY_MODERATORS_VALUE,
					_Consts.AUTH_POLICY_WRITERS_VALUE,
					_Consts.AUTH_POLICY_READERS_VALUE));

			options.AddPolicy(
				_Consts.AUTH_POLICY_USERS,
				x => x.RequireClaim(_Consts.CLAIM_AUTH_POLICY_TYPE));
		}

	}



	/*	
	public class CatalogRequirement
		: IAuthorizationRequirement
	{
		protected internal string Catalog { get; set; }

		public CatalogRequirement(
			string catalog)
		{
			Catalog = catalog;
		}
	}

	public class CatalogHandler
		: AuthorizationHandler<CatalogRequirement>
	{
		protected override Task HandleRequirementAsync(
			AuthorizationHandlerContext context,
			CatalogRequirement requirement)
		{
			if (context.User.HasClaim(
				x => x.Type == "CATALOGS"))
			{
				if (context.User.FindFirst(
					x => x.Type == "CATALOGS").Value == requirement.Catalog)
					context.Succeed(requirement);
			}
			return Task.CompletedTask;
		}
	}
	*/

}
