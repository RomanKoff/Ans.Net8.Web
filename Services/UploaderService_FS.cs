using Ans.Net8.Common;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;

namespace Ans.Net8.Web.Services
{

	/*
     *  builder.Services.AddSingleton<IUploaderService, UploaderService_FS>();
     */



	public interface IUploaderService
	{
		string BasePath { get; set; }
		Func<string, string> FilenameHandler { get; set; }

		IActionResult Upload(IFormFile file, string localPath, string name, int chunk);
	}



	public class UploaderService_FS
		: IUploaderService
	{

		/* properties */


		public string BasePath { get; set; }

		public Func<string, string> FilenameHandler { get; set; }
			= new(x => SuppIO.GetSafeFilename(x));


		/* methods */


		public IActionResult Upload(
			IFormFile file,
			string localPath,
			string name,
			int chunk)
		{
			var path1 = Path.Combine(BasePath, localPath);
			var name1 = FilenameHandler == null
				? name
				: FilenameHandler(name);
			var filename1 = Path.Combine(path1, name1);
			var mode1 = chunk == 0
				? FileMode.Create
				: FileMode.Append;
			Debug.WriteLine($"ANS: Upload({chunk}, \"{filename1}\")");
			SuppIO.CreateDirectoryIfNotExists(path1);
			SuppIO.FileWrite(filename1, getFormFileContent(file), mode1);
			return new OkResult();
		}


		/* privates */


		private static byte[] getFormFileContent(
			IFormFile file)
		{
			var buffer1 = new byte[file.Length];
			using var src1 = file.OpenReadStream();
			src1.Read(buffer1, 0, buffer1.Length);
			return buffer1;
		}

	}

}
