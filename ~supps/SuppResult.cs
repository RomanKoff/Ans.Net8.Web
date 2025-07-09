using Ans.Net8.Common;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Net.Http.Headers;

namespace Ans.Net8.Web
{

	public static class SuppResult
	{

		public static PhysicalFileResult GetPhysicalFileResult(
			string physicalPath,
			string contentType)
		{
			var last1 = SuppIO.GetFileLastModified(physicalPath);
			var tag1 = new EntityTagHeaderValue($"\"{last1.Ticks}\"");
			return new PhysicalFileResult(physicalPath, contentType)
			{
				//LastModified = last1,
				EntityTag = tag1,
			};
		}


		public static IActionResult GetPhysicalFileOrNotFoundResult(
			string path,
			string contentType)
		{
			return (File.Exists(path))
				? GetPhysicalFileResult(path, contentType)
				: new NotFoundResult();
		}

	}

}
