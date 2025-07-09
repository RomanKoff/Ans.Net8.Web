using Ans.Net8.Common;
using Microsoft.AspNetCore.Mvc.ModelBinding;

namespace Ans.Net8.Web
{

	public static partial class _e_Exception
	{

		/* functions */


		public static bool TestUpdateUnique(
			this Exception exception,
			ModelStateDictionary modelState,
			string fieldName)
		{
			if (!exception.TestContains("UNIQUE KEY"))
				return false;
			if (exception.TestContains($"_{fieldName}'."))
				modelState.AddModelError(
					fieldName, Common.Resources.Form.Text_RequiresAUniqueValue);
			modelState.AddModelError("", Common.Resources.Form.Text_SuchAnObjectExists);
			return true;
		}


		public static bool TestDuplicateKey(
			this Exception exception,
			ModelStateDictionary modelState)
		{
			//	psql 23505:
			//	duplicate key value violates unique constraint
			//	IX_GuapApplicationUsers_MasterPtr_GuapUserPtr"

			if (exception.TestStartsWith("23505: "))
			{
				modelState.AddModelError("", Common.Resources.Form.Text_RequiresAUniqueValue);
				return true;
			}
			return false;
		}


		public static bool TestDeleteReference(
			this Exception exception,
			ModelStateDictionary modelState)
		{
			//	mssql:
			//	if (!exception.TestContains("REFERENCE"))

			//	psql 23503:
			//	update or delete on table "GuapApplications" violates foreign key constraint
			//	"FK_GuapApplicationUsers_GuapApplications_MasterPtr"
			//	on table "GuapApplicationUsers"

			if (exception.TestStartsWith("23503: "))
			{
				modelState.AddModelError("", Common.Resources.Form.Text_ObjectContainsData);
				return true;
			}
			return false;
		}

	}

}
