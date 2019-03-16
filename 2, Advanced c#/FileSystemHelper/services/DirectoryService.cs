using FileSystemHelper.Interfaces;
using System.Collections.Generic;
using System.IO;

namespace FileSystemHelper.Services
{
	public class DirectoryService : IDirectoryService
	{
		public bool DirectoryExists(string path) => Directory.Exists(path);

		public IEnumerable<string> GetDirectories(string path)
		{
			var directories = Directory.GetDirectories(path);

			foreach (var directory in directories)
			{
				yield return directory;
			}
		}

		public IEnumerable<string> GetFiles(string path)
		{
			var files = Directory.GetFiles(path);

			foreach(var file in files)
			{
				yield return file;
			}
		}
	}
}
