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
	 *	LibStartup.Add_AnsNet8Web()
	 *		builder.Services.AddScoped<IViewRenderService, AnsViewRenderService>();
     */



	public interface IViewRenderService
	{
		ActionContext GetActionContext();

		ViewEngineResult GetViewEngineResult(
			ActionContext actionContext,
			string viewName);

		ViewEngineResult GetViewEngineResult(
			string viewName);

		ViewEngineResult GetPartialEngineResult(
			string viewName);

		Task<string> RenderViewToStringAsync(
			string viewName,
			object model);

		Task<string> RenderPartialToStringAsync(
			string viewName,
			object model);
	}



	public class AnsViewRenderService(
		IRazorViewEngine razorViewEngine,
		ITempDataProvider tempDataProvider,
		IServiceProvider serviceProvider)
		: IViewRenderService
	{

		private readonly IRazorViewEngine _razorViewEngine = razorViewEngine;
		private readonly ITempDataProvider _tempDataProvider = tempDataProvider;
		private readonly IServiceProvider _serviceProvider = serviceProvider;


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


		public ViewEngineResult GetPartialEngineResult(
			string viewName)
		{
			return _razorViewEngine.GetView(
				viewName, viewName, false);
		}


		public async Task<string> RenderViewToStringAsync(
			string viewName,
			object model)
		{
			var action1 = GetActionContext();
			var result1 = GetViewEngineResult(action1, viewName);
			if (result1.View == null)
				throw new ArgumentNullException(
					$"{viewName} does not match any available view");
			return await _renderAsync(result1, model);
		}


		public async Task<string> RenderPartialToStringAsync(
			string viewName,
			object model)
		{
			var result1 = GetPartialEngineResult(viewName);
			if (result1.View == null)
				throw new ArgumentNullException(
					$"{viewName} does not match any available partial");
			return await _renderAsync(result1, model);
		}


		/* privates */


		private async Task<string> _renderAsync(
			ViewEngineResult result,
			object model)
		{
			var action1 = GetActionContext();
			var dictionary1 = new ViewDataDictionary(
				new EmptyModelMetadataProvider(),
				new ModelStateDictionary())
			{ Model = model };
			using var writer1 = new StringWriter();
			var view1 = new ViewContext(
				action1, result.View, dictionary1,
				new TempDataDictionary(action1.HttpContext, _tempDataProvider),
				writer1, new HtmlHelperOptions());
			await result.View.RenderAsync(view1);
			return writer1.ToString();
		}

	}

}
