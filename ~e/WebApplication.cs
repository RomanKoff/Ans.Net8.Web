using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Routing;

namespace Ans.Net8.Web
{

    public static partial class _e
    {

        /*
         * void AddRoute(this IEndpointRouteBuilder endpoints, string routeDef);
         * void AddRoutes(this IEndpointRouteBuilder endpoints, params string[] routeDefs);
         * void AddRoutes(this IEndpointRouteBuilder endpoints, string routeDefs);
         */


        /// <summary>
        /// Добавляет Route из строки
        /// </summary>
        /// <param name="routeDef">name|template|controller|action</param>
        public static void AddRoute(
            this IEndpointRouteBuilder endpoints,
            string routeDef)
        {
            if (endpoints == null)
                throw new ArgumentNullException(nameof(endpoints));
            if (string.IsNullOrEmpty(routeDef))
                throw new ArgumentNullException(nameof(routeDef));
            var a1 = routeDef.Split(new char[] { '|' });
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
            if (endpoints == null)
                throw new ArgumentNullException(nameof(endpoints));
            if (routeDefs == null || !routeDefs.Any())
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
            if (endpoints == null)
                throw new ArgumentNullException(nameof(endpoints));
            if (string.IsNullOrEmpty(routeDefs))
                throw new ArgumentNullException(nameof(routeDefs));
            endpoints.AddRoutes(routeDefs.Split(';'));
        }

    }

}
