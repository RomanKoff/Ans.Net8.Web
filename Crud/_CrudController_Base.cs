using Ans.Net8.Common;
using Ans.Net8.Common.Crud;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Routing;

namespace Ans.Net8.Web.Crud
{

	public interface ICrudController<TEntity>
		where TEntity : class
	{
		ActionResult Details(int id);
		ActionResult Edit(int id);
		ActionResult Edit(TEntity model);
		ActionResult Delete(int id);
		ActionResult DeletePost(int id);
	}



	public class _CrudController_Base<TEntity>
		: Controller,
		ICrudController<TEntity>
		where TEntity : class
	{

		internal readonly ICrudRepository<TEntity> _repository;


		/* ctor */


		public _CrudController_Base(
			ICrudRepository<TEntity> repository)
		{
			_repository = repository;
			ListControlName = null;
			ListRouteValues = new RouteValueDictionary();
		}


		/* properties */


		public bool IsAdvanced { get; set; }
		public string CustomListViewName { get; set; }
		public string CustomAddViewName { get; set; }
		public string CustomEditViewName { get; set; }
		public string CustomDetailsViewName { get; set; }
		public string CustomDeleteViewName { get; set; }
		public string ListControlName { get; set; }
		public RouteValueDictionary ListRouteValues { get; set; }


		/* actions */


		[Route("{id:int}")]
		[HttpGet]
		public virtual ActionResult Details(
			int id)
		{
			InitView();
			var model1 = _repository.GetItem(id);
			if (model1 == null)
				return NotFound();
			PrepareModelForDetails(model1);
			return CustomDetailsViewName == null
				? View(model1)
				: View(CustomDetailsViewName, model1);
		}


		[Route("edit/{id:int}")]
		[HttpGet]
		public virtual ActionResult Edit(
			int id)
		{
			InitView();
			var model1 = _repository.GetItem(id);
			return model1 == null || !AllowEdit(model1)
				? NotFound()
				: _getEditView(model1);
		}


		[Route("edit")]
		[HttpPost]
		[ValidateAntiForgeryToken]
		public virtual ActionResult Edit(
			TEntity model)
		{
			InitView();
			ModelFix(model);
			ModelEncode(model);
			TestModelForUpdate(model);
			if (ModelState.IsValid)
			{
				try
				{
					PrepareRedirectToList(model);
					BeforeUpdate(model);
					_repository.Update(model);
					_repository.DbContext.SaveChanges();
					AfterUpdate(model);
					return RedirectToList();
				}
				catch (Exception ex)
				{
					ParseException(ex);
				}
			}
			return _getEditView(model);
		}


		[Route("delete/{id:int}")]
		[HttpGet]
		public virtual ActionResult Delete(
			int id)
		{
			InitView();
			var model1 = _repository.GetItem(id);
			return model1 == null || !AllowDelete(model1)
				? NotFound()
				: _getDeleteView(model1);
		}


		[Route("delete/{id:int}")]
		[HttpPost]
		[ValidateAntiForgeryToken]
		[ActionName("Delete")]
		public virtual ActionResult DeletePost(
			int id)
		{
			InitView();
			var model1 = _repository.GetItem(id);
			if (model1 == null)
				return NotFound();
			TestModelForDelete(model1);
			if (ModelState.IsValid)
			{
				try
				{
					PrepareRedirectToList(model1);
					BeforeDelete(model1);
					_repository.Remove(id);
					_repository.DbContext.SaveChanges();
					AfterDelete();
					return RedirectToList();
				}
				catch (Exception ex)
				{
					ParseException(ex);
				}
			}
			return _getDeleteView(model1);
		}


		/* services */


		public virtual void SaveRouteValues(
			string order,
			bool descending)
		{
			if (string.IsNullOrEmpty(order))
				return;
			TempData.Add("ListRouteValues", new RouteValueDictionary
			{
				["order"] = order,
				["descending"] = descending
			});
		}


		public virtual void InitView() { }
		public virtual void InitModel(TEntity model) { }


		public virtual bool AllowChange(TEntity model) => true;
		public virtual bool AllowEdit(TEntity model) => AllowChange(model);
		public virtual bool AllowDelete(TEntity model) => AllowChange(model);


		public virtual void PrepareView() { }
		public virtual void PrepareModelForCreate(TEntity model) => PrepareView();
		public virtual void PrepareModelForDetails(TEntity model) => PrepareView();
		public virtual void PrepareModelForEdit(TEntity model) => PrepareView();
		public virtual void PrepareModelForDelete(TEntity model) => PrepareView();


		public virtual void ModelFix(TEntity model) { }
		public virtual void ModelEncode(TEntity model) { }
		public virtual void ModelDecode(TEntity model) { }


		public virtual void TestModel(TEntity model) { }
		public virtual void TestModelForCreate(TEntity model) => TestModel(model);
		public virtual void TestModelForUpdate(TEntity model) => TestModel(model);
		public virtual void TestModelForDelete(TEntity model) => TestModel(model);


		public virtual void BeforeChange(TEntity model) { }
		public virtual void BeforeCreateOrUpdate(TEntity model) { }
		public virtual void BeforeCreate(TEntity model)
		{
			BeforeChange(model);
			BeforeCreateOrUpdate(model);
		}
		public virtual void BeforeUpdate(TEntity model)
		{
			BeforeChange(model);
			BeforeCreateOrUpdate(model);
		}
		public virtual void BeforeDelete(TEntity model) => BeforeChange(model);


		public virtual void AfterChange(TEntity model) { }
		public virtual void AfterCreateOrUpdate(TEntity model) { }
		public virtual void AfterCreate(TEntity model)
		{
			AfterChange(model);
			AfterCreateOrUpdate(model);
		}
		public virtual void AfterUpdate(TEntity model)
		{
			AfterChange(model);
			AfterCreateOrUpdate(model);
		}
		public virtual void AfterDelete() { }


		public virtual void PrepareRedirectToList(
			TEntity model)
		{
			if (TempData["ListRouteValues"] is RouteValueDictionary routes1)
				foreach (var item1 in routes1.Where(x => x.Key != "ptr"))
					ListRouteValues.Add(item1.Key, item1.Value);
		}


		public virtual ActionResult RedirectToEdit(
			TEntity model)
		{
			return RedirectToAction(
				"Edit", null, new RouteValueDictionary { { "id", ((IMasterEntity)model).Id } });
		}


		public virtual ActionResult RedirectToList()
		{
			return RedirectToAction(
				"List", ListControlName, ListRouteValues);
		}


		public virtual void ParseException(
			Exception exception)
		{
			ModelState.AddModelError("", exception.GetExceptionMessage());
		}


		/* internals */


		internal ActionResult _getAddView(
			TEntity model)
		{
			PrepareModelForCreate(model);
			ModelDecode(model);
			return CustomAddViewName == null
				? View(model)
				: View(CustomAddViewName, model);
		}


		internal ActionResult _getEditView(
			TEntity model)
		{
			PrepareModelForEdit(model);
			ModelDecode(model);
			return CustomEditViewName == null
				? View(model)
				: View(CustomEditViewName, model);
		}


		internal ActionResult _getDeleteView(
			TEntity model)
		{
			PrepareModelForDelete(model);
			return CustomDeleteViewName == null
				? View(model)
				: View(CustomDeleteViewName, model);
		}

	}

}
