using Ans.Net8.Common;
using Microsoft.AspNetCore.Mvc.ModelBinding;

namespace Ans.Net8.Web
{

    public static partial class _e
    {

        /*
         * bool TestUpdateUnique(this Exception exception, ModelStateDictionary modelState, string fieldName);
         * bool TestDeleteReference(this Exception exception, ModelStateDictionary modelState);
         */


        public static bool TestUpdateUnique(
            this Exception exception,
            ModelStateDictionary modelState,
            string fieldName)
        {
            if (!exception.TestContains("UNIQUE KEY"))
                return false;
            if (exception.TestContains($"_{fieldName}'."))
                modelState.AddModelError(
                    fieldName, Common.Resources.Validation.Text_RequiresAUniqueValue);
            modelState.AddModelError("", Common.Resources.Validation.Text_SuchAnObjectExists);
            return true;
        }


        public static bool TestDeleteReference(
            this Exception exception,
            ModelStateDictionary modelState)
        {
            if (!exception.TestContains("REFERENCE"))
                return false;
            modelState.AddModelError("", Common.Resources.Validation.Text_ObjectContainsData);
            return true;
        }

    }

}
