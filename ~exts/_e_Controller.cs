using Ans.Net8.Common;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace Ans.Net8.Web
{

	public static partial class _e_Controller
	{

		public static IActionResult GetOkApiResult(
			this ControllerBase controller,
			string title)
		{
			var s1 = $"{title} : success";
			Console.WriteLine(s1);
			return controller.Ok(new ApiResultModel(s1));
		}


		public static IActionResult GetErrorApiResult(
			this ControllerBase controller,
			string title)
		{
			var s1 = $"{title} : error";
			Console.WriteLine(s1);
			return controller.BadRequest(new ApiResultModel(s1));
		}


		public static IActionResult GetPaginatedList<TEntity>(
			this Controller controller,
			DbSet<TEntity> dbSet,
			Expression<Func<TEntity, bool>> filter,
			string order,
			int page,
			int itemsOnPage,
			int defaultItemsOnPage,
			int maxItemsOnPages,
			string viewName)
			where TEntity : class
		{
			var data1 = new PaginationDataModel(
				order,
				page, itemsOnPage,
				dbSet.Count(),
				defaultItemsOnPage, maxItemsOnPages);

			var query1 = filter == null
				? dbSet.AsQueryable()
				: dbSet.Where(filter);

			var model1 = data1.GetQueryTransform(query1)
				.AsEnumerable();

			controller.ViewData.SetPaginationData(data1);

			return viewName == null
				? controller.View(model1)
				: controller.View(viewName, model1);
		}

	}

}
