using Ans.Net8.Common.Crud;
using Microsoft.AspNetCore.Mvc;

namespace Ans.Net8.Web.Crud
{

	public interface ICrudMasterController<TMasterEntity>
		: ICrudController<TMasterEntity>
		where TMasterEntity : class, IMasterEntity
	{
		ActionResult List(string order = null, bool descending = false);
		ActionResult Add();
		ActionResult Add(TMasterEntity model);
	}



	public class _CrudMasterController_Base<TMasterEntity>
		: _CrudController_Base<TMasterEntity>,
		ICrudMasterController<TMasterEntity>
		where TMasterEntity : class, IMasterEntity
	{

		private ICrudMasterRepository<TMasterEntity> _masterRepository
			=> _repository as ICrudMasterRepository<TMasterEntity>;


		/* ctor */


		public _CrudMasterController_Base(
			ICrudMasterRepository<TMasterEntity> repository)
			: base(repository)
		{
		}


		/* actions */


		[Route("")]
		[HttpGet]
		public virtual ActionResult List(
			string order = null,
			bool descending = false)
		{
			InitView();
			var model1 = _masterRepository.GetItems(null, order, descending);
			SaveRouteValues(order, descending);
			PrepareModelForList(model1);
			return CustomListViewName == null
				? View(model1)
				: View(CustomListViewName, model1);
		}


		[Route("add")]
		[HttpGet]
		public virtual ActionResult Add()
		{
			InitView();
			var model1 = _masterRepository.GetNew();
			InitModel(model1);
			return model1 == null
				? NotFound()
				: _getAddView(model1);
		}


		[Route("add")]
		[HttpPost]
		public virtual ActionResult Add(
			TMasterEntity model)
		{
			InitView();
			ModelFix(model);
			ModelEncode(model);
			TestModelForCreate(model);
			if (ModelState.IsValid)
			{
				try
				{
					PrepareRedirectToList(model);
					BeforeCreate(model);
					_repository.Add(model);
					_repository.DbContext.SaveChanges();
					AfterCreate(model);
					return IsAdvanced
						? RedirectToEdit(model)
						: RedirectToList();
				}
				catch (Exception ex)
				{
					ParseException(ex);
				}
			}
			return _getAddView(model);
		}


		/* services */


		public virtual void PrepareModelForList(IEnumerable<TMasterEntity> model) => PrepareView();

	}

}
