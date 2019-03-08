using FileSystemHelper.models;

namespace FileSystemHelper.interfaces
{
	public interface IDirectoryService
	{
		bool DirectoryExists(string path);

		string[] GetDirectories(string path);

		string[] GetFiles(string path);
	}
}
