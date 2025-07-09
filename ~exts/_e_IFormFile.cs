using Ans.Net8.Common;
using Microsoft.AspNetCore.Http;

namespace Ans.Net8.Web
{

	public static partial class _e_IFormFile
	{

		public static void Upload(
			this IFormFile file,
			string filename,
			int chunk,
			string path)
		{
			var filepath1 = Path.Combine(path, filename);
			var mode1 = (chunk == 0)
				? FileMode.Create
				: FileMode.Append;
			SuppIO.FileWrite(filepath1, file.GetContentInBytes(), mode1);
		}


		public static byte[] GetContentInBytes(
			this IFormFile file)
		{
			var buffer1 = new byte[file.Length];
			using var stream1 = file.OpenReadStream();
			stream1.Read(buffer1, 0, buffer1.Length);
			return buffer1;
		}

	}

}
