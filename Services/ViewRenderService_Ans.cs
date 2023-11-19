using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Abstractions;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Microsoft.AspNetCore.Mvc.Razor;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.AspNetCore.Mvc.ViewEngines;
using Microsoft.AspNetCore.Mvc.ViewFeatures;
using Microsoft.AspNetCore.Routing;

namespace Ans.Net8.Web.Services
{

	/*
	 *	Add_AnsWeb()
     *		builder.Services.AddScoped<IViewRenderService, ViewRenderService_Ans>();
     */



	public interface IViewRenderService
	{
		ActionContext GetActionContext();
		ViewEngineResult GetViewEngineResult(ActionContext actionContext, string viewName);
		ViewEngineResult GetViewEngineResult(string viewName);
		Task<string> RenderToStringAsync(string viewName, object model);
	}



	public class ViewRenderService_Ans
		: IViewRenderService
	{

		private readonly IRazorViewEngine _razorViewEngine;
		private readonly ITempDataProvider _tempDataProvider;
		private readonly IServiceProvider _serviceProvider;


		/* ctor */


		public ViewRenderService_Ans(
			IRazorViewEngine razorViewEngine,
			ITempDataProvider tempDataProvider,
			IServiceProvider serviceProvider)
		{
			_razorViewEngine = razorViewEngine;
			_tempDataProvider = tempDataProvider;
			_serviceProvider = serviceProvider;
		}


		/* functions */


		public ActionContext GetActionContext()
		{
			return new(
				new DefaultHttpContext { RequestServices = _serviceProvider },
				new RouteData(),
				new ActionDescriptor());
		}


		public ViewEngineResult GetViewEngineResult(
			ActionContext actionContext,
			string viewName)
		{
			return _razorViewEngine.FindView(
				actionContext, viewName, false);
		}


		public ViewEngineResult GetViewEngineResult(
			string viewName)
		{
			return GetViewEngineResult(
				GetActionContext(), viewName);
		}


		public async Task<string> RenderToStringAsync(
			string viewName,
			object model)
		{
			var action1 = GetActionContext();
			var result1 = GetViewEngineResult(action1, viewName);
			if (result1.View == null)
				throw new ArgumentNullException(
					$"{viewName} does not match any available view");
			var dictionary1 = new ViewDataDictionary(
				new EmptyModelMetadataProvider(),
				new ModelStateDictionary())
			{ Model = model };
			using var writer1 = new StringWriter();
			var view1 = new ViewContext(
				action1, result1.View, dictionary1,
				new TempDataDictionary(action1.HttpContext, _tempDataProvider),
				writer1, new HtmlHelperOptions());
			await result1.View.RenderAsync(view1);
			return writer1.ToString();
		}

	}

}
