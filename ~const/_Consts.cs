using Microsoft.AspNetCore.Mvc;

namespace Ans.Net8.Web
{

	public static partial class _Consts
	{

		public const string FORM_RESOURCES_KEY = "Ans_Form_Resources";

		public static readonly CacheProfile CACHE_PROFILE_D60 = new()
		{
			Duration = 60,
			Location = ResponseCacheLocation.Any,
			NoStore = false,
			//VaryByHeader = "User-Agent",
			VaryByQueryKeys = ["*"]
		};

		public static readonly CacheProfile CACHE_PROFILE_D30 = new()
		{
			Duration = 30,
			Location = ResponseCacheLocation.Any,
			NoStore = false,
			//VaryByHeader = "User-Agent",
			VaryByQueryKeys = ["*"]
		};

		public const string CORS_POLICY_ALLOW_ALL = "CORS_ALLOW_ALL";

		public const string AUTH_POLICY_APP = "APP";
		public const string AUTH_POLICY_APP_ADMINS = "APP_ADMINS";
		public const string AUTH_POLICY_APP_MODERATORS = "APP_MODERATORS";
		public const string AUTH_POLICY_APP_EDITORS = "APP_EDITORS";
		public const string AUTH_POLICY_APP_READERS = "APP_READERS";
		public const string AUTH_POLICY_APP_USERS = "APP_USERS";

	}

}
