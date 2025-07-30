using Ans.Net8.Common;
using Ans.Net8.Common.Crud;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Routing;

namespace Ans.Net8.Web.Crud
{

	public interface ICrudMasterController<T>
		: ICrudController<T>
		where T : class, IMasterEntity
	{
		ActionResult List(string order, int page, int itemsOnPage);
		ActionResult Add();
		ActionResult AddPost(T model);
	}



	public abstract class _CrudMasterController_Proto<T>(
		ICrudMasterRepository<T> repository)
		: __CrudController_Base<T>(repository),
		ICrudMasterController<T>
		where T : class, IMasterEntity
	{

		/* readonly properties */


		public new ICrudMasterRepository<T> Repository
			=> (ICrudMasterRepository<T>)base.Repository;


		/* overrides */


		public override void InitView(T model) => InitView();


		/* virtuals */


		public virtual void InitView() { }


		public virtual ActionResult RedirectToAdd()
		{
			return RedirectToAction(
				"Add",
				null,
				null);
		}


		public virtual ActionResult RedirectToEdit(
			T model)
		{
			return RedirectToAction(
				"Edit",
				null,
				new RouteValueDictionary {
					{ "id", model.Id }
				});
		}


		public virtual ActionResult RedirectToDetails(
			T model)
		{
			return RedirectToAction(
				"Details",
				null,
				new RouteValueDictionary {
					{ "id", model.Id }
				});
		}


		/* actions */


		// [HttpGet("")]
		public virtual ActionResult List(
			string order,
			int page,
			int itemsOnPage)
		{
			var query1 = GetListQuery();

			var paginator1 = new PaginationDataModel(
				order ?? DefaultOrder ?? "Id",
				page, itemsOnPage,
				query1.Count(),
				DefaultItemsOnPage, MaxItemsOnPage);

			var model1 = paginator1.GetQueryTransform(query1)
				.AsEnumerable();

			ViewData.SetPaginationData(paginator1);

			return GetListView(model1);
		}


		// [HttpGet("add")]
		public virtual ActionResult Add()
		{
			var model1 = Repository.GetNew();
			return GetAddView(model1);
		}


		// [HttpPost("add")]
		// [ActionName("Add")]
		// [ValidateAntiForgeryToken]
		public virtual ActionResult AddPost(
			T model)
		{
			if (model == null)
				return NotFound();
			FixModelAfterInput(model);
			BeforeAdd(model);
			EncodeModelBeforeSave(model);
			ValidationModel(model);
			if (ModelState.IsValid)
			{
				try
				{
					Repository.Add(model);
					Repository.DbContext.SaveChanges();
					AfterAdd(model);
					PrepareRedirectToList(model);
					return ViewAfterAdd switch
					{
						CrudViewEnum.Add => RedirectToAdd(),
						CrudViewEnum.Edit => RedirectToEdit(model),
						CrudViewEnum.Details => RedirectToDetails(model),
						_ => RedirectToList(model)
					};
				}
				catch (Exception ex)
				{
					ParseException(ex);
				}
			}
			return GetAddView(model);
		}


		/* functions */


		public virtual IQueryable<T> GetListQuery()
		{
			return Repository.GetItemsAsQueryable(ListFilter);
		}


		/* services */


		public ActionResult GetListView(
			IEnumerable<T> model)
		{
			InitView();
			PrepareForList(model);
			return CustomListViewName == null
				? View(model)
				: View(CustomListViewName, model);
		}

	}

}
