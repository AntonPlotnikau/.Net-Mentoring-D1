using FileDistributor.Models;
using System;

namespace FileDistributor.Interfaces
{
	public interface IDirectoryWatcher
	{
		event EventHandler<FileCreatedEventArgs> FileCreated;
	}
}
