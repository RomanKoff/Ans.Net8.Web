using Microsoft.AspNetCore.Mvc.Razor;

namespace Ans.Net8.Web
{

	public static partial class _e
    {

        /*
         * async Task<bool> TrySectionAsync(this RazorPage page, string sectionName);
         */


        /// <summary>
        /// Возвращает false, если секция не определена,
        /// иначе рендерит секцию и возвращает true
        /// </summary>
        public static async Task<bool> TrySectionAsync(
            this RazorPage page,
            string sectionName)
        {
            if (!page.IsSectionDefined(sectionName))
                return false;
            await page.RenderSectionAsync(sectionName);
            return true;
        }

    }

}
