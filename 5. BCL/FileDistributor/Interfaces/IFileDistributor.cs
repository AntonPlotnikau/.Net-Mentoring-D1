using FileDistributor.Models;

namespace FileDistributor.Interfaces
{
	public interface IFileDistributor
	{
		void MoveFile(FileModel file);
	}
}
