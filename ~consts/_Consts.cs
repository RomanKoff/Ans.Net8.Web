using Ans.Net8.Common;
using Microsoft.AspNetCore.Html;
using Microsoft.AspNetCore.Mvc;

namespace Ans.Net8.Web
{

	public static partial class _Consts
	{

		public static readonly Dictionary<string, CacheProfile> CACHE_PROFILES = new() {
			{
				"NONE",
				new() {
					Duration = 1,
					Location = ResponseCacheLocation.None,
					NoStore = true,
					VaryByQueryKeys = ["*"]
				}
			},
			{
				"D10",
				new() {
					Duration = 10,
					Location = ResponseCacheLocation.Any,
					NoStore = false,
					VaryByQueryKeys = ["*"]
				}
			},
			{
				"D30",
				new() {
					Duration = 30,
					Location = ResponseCacheLocation.Any,
					NoStore = false,
					VaryByQueryKeys = ["*"]
				}
			},
			{
				"D60",
				new() {
					Duration = 60,
					Location = ResponseCacheLocation.Any,
					NoStore = false,
					VaryByQueryKeys = ["*"]
				}
			}
		};


		public static readonly Dictionary<ContentGroupEnum, HtmlString> CONTENT_GROUPS_RU = new()
		{
			{ ContentGroupEnum.Archive, new HtmlString("Архив") },
			{ ContentGroupEnum.Audio, new HtmlString("Аудио") },
			{ ContentGroupEnum.Code, new HtmlString("Код") },
			{ ContentGroupEnum.Document, new HtmlString("Документ") },
			{ ContentGroupEnum.Image, new HtmlString("Изображение") },
			{ ContentGroupEnum.Text, new HtmlString("Текст") },
			{ ContentGroupEnum.Video, new HtmlString("Видео") },
			{ ContentGroupEnum.Bin, new HtmlString("<данные>" ) },
		};


		public const string CLAIM_AUTH_POLICY_TYPE = "ANS_AUTH_POL";
		public const string CLAIM_ACTIONS_TYPE = "ANS_ACTIONS";
		public const string CLAIM_RESOURCES_TYPE = "ANS_RESOURCES";

		public const string AUTH_POLICY_ADMINS = $"{CLAIM_AUTH_POLICY_TYPE}_ADMINS";
		public const string AUTH_POLICY_ADMINS_VALUE = "4";

		public const string AUTH_POLICY_MODERATORS = $"{CLAIM_AUTH_POLICY_TYPE}_MODERATORS";
		public const string AUTH_POLICY_MODERATORS_VALUE = "3";

		public const string AUTH_POLICY_WRITERS = $"{CLAIM_AUTH_POLICY_TYPE}_WRITERS";
		public const string AUTH_POLICY_WRITERS_VALUE = "2";

		public const string AUTH_POLICY_READERS = $"{CLAIM_AUTH_POLICY_TYPE}_READERS";
		public const string AUTH_POLICY_READERS_VALUE = "1";

		public const string AUTH_POLICY_USERS = $"{CLAIM_AUTH_POLICY_TYPE}_USERS";
		public const string AUTH_POLICY_USERS_VALUE = "0";

		public const string CORS_ALLOW_ALL = "ALLOW ALL";

		public const string FORM_RESOURCES_KEY = "ANS_FORM_RESOURCES";

		public const string OPTION_TABS = "&nbsp;&nbsp;&nbsp;";

		public static readonly HtmlString HTML_SPACE_PLACEHOLDER = new("&nbsp;");

	}

}
