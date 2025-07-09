using Ans.Net8.Common;
using Ans.Net8.Common.Crud;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Routing;
using System.Linq.Expressions;

namespace Ans.Net8.Web.Crud
{

	public enum CrudViewEnum
	{
		List,
		Add,
		Edit,
		Details
	}



	public interface ICrudController<T>
		where T : class
	{
		ActionResult Details(int id);
		ActionResult Edit(int id);
		ActionResult EditPost(int id, T model);
		ActionResult Delete(int id);
		ActionResult DeletePost(int id);
	}



	// [Authorize()]
	// [Route("/")]
	// [ApiExplorerSettings(IgnoreApi = true)]
	public abstract class __CrudController_Base<T>
		: Controller,
		ICrudController<T>
		where T : class
	{

		/* ctor */


		public __CrudController_Base(
			ICrudRepository<T> repository)
		{
			Repository = repository;
			InitController();
		}


		/* abstracts */


		public abstract void InitView(T model);


		/* virtuals */


		public virtual void InitController() { }

		public virtual bool AllowDetails(T model) => true;
		public virtual bool AllowChange(T model) => true;
		public virtual bool AllowEdit(T model) => AllowChange(model);
		public virtual bool AllowDelete(T model) => AllowChange(model);

		public virtual void Prepare(T model) { }
		public virtual void PrepareForAdd(T model) => Prepare(model);
		public virtual void PrepareForDetails(T model) => Prepare(model);
		public virtual void PrepareForEdit(T model) => PrepareForDetails(model);
		public virtual void PrepareForDelete(T model) => PrepareForDetails(model);
		public virtual void PrepareForList(IEnumerable<T> model)
		{
			foreach (var item1 in model)
				DecodeModelBeforeView(item1);
		}

		public virtual void FixModelAfterInput(T model) { }
		public virtual void EncodeModelBeforeSave(T model) { }
		public virtual void DecodeModelBeforeEdit(T model) { }
		public virtual void DecodeModelBeforeView(T model) { }

		public virtual void ValidationModel(T model) { }

		public virtual void BeforeChange(T model) { }
		public virtual void BeforeAdd(T model) => BeforeChange(model);
		public virtual void BeforeUpdate(T model) => BeforeChange(model);
		public virtual void BeforeDelete(T model) { }

		public virtual void AfterChange(T model) { }
		public virtual void AfterAdd(T model) => AfterChange(model);
		public virtual void AfterUpdate(T model) => AfterChange(model);
		public virtual void AfterDelete() { }

		public virtual void PrepareRedirectToList(T model) { }


		public virtual ActionResult RedirectToList(
			T model)
		{
			return RedirectToAction(
				"List", null, ListRouteValues);
		}


		public virtual void ParseException(
			Exception exception)
		{
			if (exception.TestDuplicateKey(ModelState))
				return;
			if (exception.TestDeleteReference(ModelState))
				return;
			ModelState.AddModelError(
				"", exception.GetExceptionMessage());
		}


		/* readonly properties */


		public ICrudRepository<T> Repository { get; }


		/* properties */


		public string DefaultOrder { get; set; }
		public int DefaultItemsOnPage { get; set; } = 25;
		public int MaxItemsOnPage { get; set; } = 500;
		public CrudViewEnum ViewAfterAdd { get; set; } = CrudViewEnum.List;
		public string CustomListViewName { get; set; }
		public string CustomAddViewName { get; set; }
		public string CustomDetailsViewName { get; set; }
		public string CustomEditViewName { get; set; }
		public string CustomDeleteViewName { get; set; }
		public RouteValueDictionary ListRouteValues { get; set; } = [];
		public Expression<Func<T, bool>> ListFilter { get; set; } = null;


		/* actions */


		// [HttpGet("details/{id:int}")]
		public virtual ActionResult Details(
			int id)
		{
			var model1 = Repository.GetItem(id);
			DecodeModelBeforeView(model1);
			return GetDetailView(model1);
		}


		// [HttpGet("edit/{id:int}")]
		public virtual ActionResult Edit(
			int id)
		{
			var model1 = Repository.GetItem(id);
			DecodeModelBeforeEdit(model1);
			return GetEditView(model1);
		}


		// [HttpPost("edit/{id:int}")]
		// [ActionName("Edit")]
		// [ValidateAntiForgeryToken]
		public virtual ActionResult EditPost(
			int id,
			T model)
		{
			if (model == null)
				return NotFound();
			if (!AllowEdit(model))
				return Forbid();
			FixModelAfterInput(model);
			BeforeUpdate(model);
			EncodeModelBeforeSave(model);
			ValidationModel(model);
			if (ModelState.IsValid)
			{
				try
				{
					Repository.UpdateEvery(model);
					Repository.DbContext.SaveChanges();
					AfterUpdate(model);
					PrepareRedirectToList(model);
					return RedirectToList(model);
				}
				catch (Exception ex)
				{
					ParseException(ex);
				}
			}
			return GetEditView(model);
		}


		// [HttpGet("delete/{id:int}")]
		public virtual ActionResult Delete(
			int id)
		{
			var model1 = Repository.GetItem(id);
			DecodeModelBeforeView(model1);
			return GetDeleteView(model1);
		}


		// [HttpPost("delete/{id:int}")]
		// [ActionName("Delete")]
		// [ValidateAntiForgeryToken]
		public virtual ActionResult DeletePost(
			int id)
		{
			var model1 = Repository.GetItem(id);
			if (model1 == null)
				return NotFound();
			if (!AllowDelete(model1))
				return Forbid();
			BeforeDelete(model1);
			try
			{
				Repository.Remove(id);
				Repository.DbContext.SaveChanges();
				AfterDelete();
				PrepareRedirectToList(model1);
				return RedirectToList(model1);
			}
			catch (Exception ex)
			{
				ParseException(ex);
			}
			return GetDeleteView(model1);
		}


		/* services */


		public ActionResult GetAddView(
			T model)
		{
			if (model == null)
				return NotFound();
			InitView(model);
			PrepareForAdd(model);
			return CustomAddViewName == null
				? View(model)
				: View(CustomAddViewName, model);
		}


		public ActionResult GetDetailView(
			T model)
		{
			if (model == null)
				return NotFound();
			if (!AllowDetails(model))
				return Forbid();
			InitView(model);
			PrepareForDetails(model);
			return CustomDetailsViewName == null
				? View(model)
				: View(CustomDetailsViewName, model);
		}


		public ActionResult GetEditView(
			T model)
		{
			if (model == null)
				return NotFound();
			if (!AllowEdit(model))
				return Forbid();
			InitView(model);
			PrepareForEdit(model);
			return CustomEditViewName == null
				? View(model)
				: View(CustomEditViewName, model);
		}


		public ActionResult GetDeleteView(
			T model)
		{
			if (model == null)
				return NotFound();
			if (!AllowDelete(model))
				return Forbid();
			InitView(model);
			PrepareForDelete(model);
			return CustomDeleteViewName == null
				? View(model)
				: View(CustomDeleteViewName, model);
		}

	}

}
