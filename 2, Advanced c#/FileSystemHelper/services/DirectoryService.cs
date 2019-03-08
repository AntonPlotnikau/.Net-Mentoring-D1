using FileSystemHelper.interfaces;
using System.IO;

namespace FileSystemHelper.services
{
	public class DirectoryService : IDirectoryService
	{
		public bool DirectoryExists(string path) => Directory.Exists(path);

		public string[] GetDirectories(string path) => Directory.GetDirectories(path);

		public string[] GetFiles(string path) => Directory.GetFiles(path);
	}
}
