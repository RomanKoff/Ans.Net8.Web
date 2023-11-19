using Microsoft.AspNetCore.Mvc.Razor;
using Microsoft.AspNetCore.Mvc.ViewEngines;

namespace Ans.Net8.Web
{

    public static partial class _e
    {

        /*
         * string GetViewName(this IView view);
         * string GetViewName(this IRazorPage view);
         */


        public static string GetViewName(
            this IView view)
        {
            string s1 = view.Path;
            return Path.GetFileNameWithoutExtension(
                s1[s1.LastIndexOf('/')..]);
        }


        public static string GetViewName(
            this IRazorPage view)
        {
            string s1 = view.Path;
            return Path.GetFileNameWithoutExtension(
                s1[s1.LastIndexOf('/')..]);
        }

    }

}
