using Ans.Net8.Common.Crud;
using Microsoft.AspNetCore.Mvc;

namespace Ans.Net8.Web.Crud
{

	public interface ICrudSlaveController<TSlaveEntity>
		: ICrudController<TSlaveEntity>
		where TSlaveEntity : class, ISlaveEntity
	{
		ActionResult List(int ptr, string order = null, bool descending = false);
		ActionResult Add(int ptr);
		ActionResult Add(int ptr, TSlaveEntity model);
	}



	public class _CrudSlaveController_Base<TSlaveEntity>(
		ICrudSlaveRepository<TSlaveEntity> repository)
		: _CrudController_Base<TSlaveEntity>(repository),
		ICrudSlaveController<TSlaveEntity>
		where TSlaveEntity : class, ISlaveEntity
	{

		private ICrudSlaveRepository<TSlaveEntity> _slaveRepository
			=> _repository as ICrudSlaveRepository<TSlaveEntity>;


		/* actions */


		[Route("")]
		[HttpGet]
		public ActionResult List(
			int ptr,
			string order = null,
			bool descending = false)
		{
			InitView();
			var model1 = _slaveRepository.GetItems(null, ptr, order, descending);
			SaveRouteValues(order, descending);
			PrepareModelForList(model1);
			return CustomListViewName == null
				? View(model1)
				: View(CustomListViewName, model1);
		}


		[Route("{ptr:int}/add")]
		[HttpGet]
		public ActionResult Add(
			int ptr)
		{
			InitView();
			var model1 = _slaveRepository.GetNew(ptr);
			InitModel(model1);
			return model1 == null
				? NotFound()
				: _getAddView(model1);
		}


		[Route("{ptr:int}/add")]
		[HttpPost]
		[ValidateAntiForgeryToken]
		public ActionResult Add(
			int ptr,
			TSlaveEntity model)
		{
			InitView();
			ModelFix(model);
			ModelEncode(model);
			TestModelForCreate(model);
			if (ModelState.IsValid)
			{
				try
				{
					((ISlaveEntity)model).MasterPtr = ptr;
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


		public virtual void PrepareModelForList(IEnumerable<TSlaveEntity> model) => PrepareView();


		/* overrides */


		public override void PrepareRedirectToList(
			TSlaveEntity model)
		{
			ListRouteValues.Add("ptr", ((ISlaveEntity)model).MasterPtr);
			base.PrepareRedirectToList(model);
		}

	}

}
