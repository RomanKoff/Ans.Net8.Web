﻿using Microsoft.AspNetCore.Mvc.ModelBinding;

namespace Ans.Net8.Web
{

	public static partial class _e
	{

		/*
         *  string[] GetModelErrors(this ModelStateDictionary modelState);
         */


		public static string[] GetModelErrors(
			this ModelStateDictionary modelState)
		{
			return modelState._getErrors(
				x => string.IsNullOrEmpty(x.Key));
		}


		public static string[] GetFieldErrors(
			this ModelStateDictionary modelState,
			string name)
		{
			return modelState._getErrors(
				x => x.Key == name);
		}


		/* privates */


		private static string[] _getErrors(
			this ModelStateDictionary modelState,
			Func<KeyValuePair<string, ModelStateEntry>, bool> func)
		{
			if (modelState.IsValid)
				return null;
			var aa1 = modelState
				.Where(func)
				.Select(x => x.Value.Errors);
			if (!aa1?.Any() ?? true)
				return null;
			var a1 = new List<string>();
			foreach (var item1 in aa1)
				a1.AddRange(item1.Select(x => x.ErrorMessage));
			return [.. a1];
		}

	}

}
