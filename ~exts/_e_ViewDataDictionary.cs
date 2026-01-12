using Ans.Net8.Common;
using Microsoft.AspNetCore.Html;
using Microsoft.AspNetCore.Mvc.ViewFeatures;

namespace Ans.Net8.Web
{

	public static partial class _e_ViewDataDictionary
	{

		/* methods */


		public static void SetRegistryList(
			this ViewDataDictionary viewData,
			string name,
			RegistryList registry)
		{
			viewData[$"Reg_{name}"] = registry;
		}


		public static void SetRegistryList<T>(
			this ViewDataDictionary viewData,
			string name,
			IEnumerable<T> items,
			Func<T, RegistryItem> funcItem)
		{
			var reg1 = new RegistryList(
				items.Select(funcItem));
			viewData.SetRegistryList(name, reg1);
		}


		public static void SetRegistryList<T>(
			this ViewDataDictionary viewData,
			string name,
			IEnumerable<T> items,
			Func<T, string> funcKey,
			Func<T, string> funcValue)
		{
			viewData.SetRegistryList(
				name, items, x => new RegistryItem(
					funcKey(x), funcValue(x), 0, false));
		}


		public static void SetPaginationData(
			this ViewDataDictionary viewData,
			PaginationDataModel pagination)
		{
			viewData["Pagination_Data"] = pagination;
		}


		/* functions */


		public static RegistryList GetRegistryList(
			this ViewDataDictionary viewData,
			string name)
		{
			return viewData[$"Reg_{name}"] as RegistryList;
		}


		public static PaginationDataModel GetPaginationData(
			this ViewDataDictionary viewData)
		{
			return viewData[$"Pagination_Data"] as PaginationDataModel;
		}


		public static PaginationHelper GetPaginationHelper(
			this ViewDataDictionary viewData)
		{
			var data1 = viewData.GetPaginationData();
			return new PaginationHelper(
				data1?.ItemsOnPage ?? 0, data1?.TotalItems ?? 0, data1?.Page ?? 0);
		}


		public static CrudFieldInfo GetFieldInfo(
			this ViewDataDictionary viewData)
		{
			var info1 = (CrudFieldInfo)viewData["FieldInfo"];
			return info1;
		}


		public static string GetString(
			this ViewDataDictionary viewData,
			string name,
			string defaultValue = null)
		{
			return (string)viewData.Eval(name) ?? defaultValue;
		}


		//public static HtmlString GetHtml(
		//	this ViewDataDictionary viewData,
		//	string name,
		//	HtmlString defaultValue = null)
		//{
		//	return (HtmlString)viewData.Eval(name) ?? defaultValue;
		//}


		public static int? GetInt(
			this ViewDataDictionary viewData,
			string name)
		{
			return (int?)viewData.Eval(name);
		}
		public static int GetInt(
			this ViewDataDictionary viewData,
			string name,
			int defaultValue)
		{
			return viewData.GetInt(name) ?? defaultValue;
		}


		public static bool GetBool(
			this ViewDataDictionary viewData,
			string name)
		{
			var v1 = viewData.Eval(name);
			return v1 != null && (bool)v1;
		}


		public static DateTime? GetDateTime(
			this ViewDataDictionary viewData,
			string name)
		{
			return (DateTime?)viewData.Eval(name);
		}
		public static DateTime GetDateTime(
			this ViewDataDictionary viewData,
			string name,
			DateTime defaultValue)
		{
			return viewData.GetDateTime(name) ?? defaultValue;
		}


		public static DateOnly? GetDateOnly(
			this ViewDataDictionary viewData,
			string name)
		{
			return (DateOnly?)viewData.Eval(name);
		}
		public static DateOnly GetDateOnly(
			this ViewDataDictionary viewData,
			string name,
			DateOnly defaultValue)
		{
			return viewData.GetDateOnly(name) ?? defaultValue;
		}


		public static TimeOnly? GetTimeOnly(
			this ViewDataDictionary viewData,
			string name)
		{
			return (TimeOnly?)viewData.Eval(name);
		}
		public static TimeOnly GetTimeOnly(
			this ViewDataDictionary viewData,
			string name,
			TimeOnly defaultValue)
		{
			return viewData.GetTimeOnly(name) ?? defaultValue;
		}

	}

}
