using Ans.Net8.Common;
using Ans.Net8.Common.Crud;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Routing;

namespace Ans.Net8.Web.Crud
{

	public interface ICrudSlaveController<T>
		: ICrudController<T>
		where T : class, ISlaveEntity
	{
		ActionResult List(int masterPtr, string order, int page, int itemsOnPage);
		ActionResult Add(int masterPtr);
		ActionResult AddPost(int masterPtr, T model);
	}



	public abstract class _CrudSlaveController_Proto<T>(
		ICrudSlaveRepository<T> repository)
		: __CrudController_Base<T>(repository),
		ICrudSlaveController<T>
		where T : class, ISlaveEntity
	{

		/* readonly properties */


		public new ICrudSlaveRepository<T> Repository
			=> (ICrudSlaveRepository<T>)base.Repository;


		/* overrides */


		public override void InitView(T model) => InitView(model.MasterPtr);


		/* virtuals */


		public virtual void InitView(int masterPtr) { }


		public override void PrepareRedirectToList(
			T model)
		{
			ListRouteValues.Add("masterPtr", model.MasterPtr);
			base.PrepareRedirectToList(model);
		}


		public virtual ActionResult RedirectToAdd(
			int masterPtr)
		{
			return RedirectToAction(
				"Add",
				null,
				new RouteValueDictionary {
					{ "masterPtr", masterPtr },
				});
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


		// [HttpGet("{masterPtr:int}")]
		public virtual ActionResult List(
			int masterPtr,
			string order,
			int page,
			int itemsOnPage)
		{
			var query1 = GetListQuery(masterPtr);

			var paginator1 = new PaginationDataModel(
				order ?? DefaultOrder ?? "Id",
				page, itemsOnPage,
				query1.Count(),
				DefaultItemsOnPage, MaxItemsOnPage);

			var model1 = paginator1.GetQueryTransform(query1)
				.AsEnumerable();

			ViewData.SetPaginationData(paginator1);

			return GetListView(model1, masterPtr);
		}


		// [HttpGet("{masterPtr:int}/add")]
		public virtual ActionResult Add(
			int masterPtr)
		{
			var model1 = Repository.GetNew(masterPtr);
			return GetAddView(model1);
		}


		// [HttpPost("{masterPtr:int}/add")]
		// [ActionName("Add")]
		// [ValidateAntiForgeryToken]
		public virtual ActionResult AddPost(
			int masterPtr,
			T model)
		{
			if (model == null)
				return NotFound();
			FixModelAfterInput(model);
			BeforeAdd(model);
			EncodeModelBeforeSave(model);
			model.MasterPtr = masterPtr;
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
						CrudViewEnum.Add => RedirectToAdd(masterPtr),
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


		public IQueryable<T> GetListQuery(
			int masterPtr)
		{
			var filter1 = ListFilter == null
				? x => x.MasterPtr == masterPtr
				: ListFilter.And(x => x.MasterPtr == masterPtr);
			return Repository.GetItemsAsQueryable(filter1);
		}


		/* services */


		public ActionResult GetListView(
			IEnumerable<T> model,
			int masterPtr)
		{
			InitView(masterPtr);
			PrepareForList(model);
			return CustomListViewName == null
				? View(model)
				: View(CustomListViewName, model);
		}

	}

}
