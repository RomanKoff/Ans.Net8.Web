using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Routing;

namespace Ans.Net8.Web
{

	public static partial class __e_routes
	{

		/* methods */


		/// <summary>
		/// Добавляет Route из строки
		/// </summary>
		/// <param name="routeDef">name|template|controller|action</param>
		public static void AddRoute(
			this IEndpointRouteBuilder endpoints,
			string routeDef)
		{
			ArgumentNullException.ThrowIfNull(endpoints);
			ArgumentException.ThrowIfNullOrEmpty(routeDef, nameof(routeDef));
			var a1 = routeDef.Split(['|']);
			if (a1.Length != 4)
				throw new ArgumentOutOfRangeException(nameof(routeDef));
			endpoints.MapControllerRoute(
				a1[0], a1[1],
				new { controller = a1[2], action = a1[3] });
		}


		/// <summary>
		/// Добавляет коллекцию Route из массива строк
		/// </summary>
		public static void AddRoutes(
			this IEndpointRouteBuilder endpoints,
			params string[] routeDefs)
		{
			ArgumentNullException.ThrowIfNull(endpoints);
			if (!(routeDefs?.Length > 0))
				throw new ArgumentNullException(nameof(routeDefs));
			foreach (var s1 in routeDefs)
				endpoints.AddRoute(s1);
		}


		/// <summary>
		/// Добавляет коллекцию Route из строки
		/// </summary>
		/// <param name="routeDefs">route1;route2...</param>
		public static void AddRoutes(
			this IEndpointRouteBuilder endpoints,
			string routeDefs)
		{
			ArgumentNullException.ThrowIfNull(endpoints);
			ArgumentException.ThrowIfNullOrEmpty(routeDefs, nameof(routeDefs));
			endpoints.AddRoutes(routeDefs.Split(';'));
		}

	}

}
