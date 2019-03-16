using FileSystemHelper.Models;
using System.Collections.Generic;

namespace FileSystemHelper.Interfaces
{
	public interface IDirectoryService
	{
		bool DirectoryExists(string path);

		IEnumerable<string> GetDirectories(string path);

		IEnumerable<string> GetFiles(string path);
	}
}
