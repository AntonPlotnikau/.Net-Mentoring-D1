using FileDistributor.Interfaces;
using FileDistributor.LocalizationResources;
using FileDistributor.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace FileDistributor.Services
{
	public class DirectoryWatcher: IDirectoryWatcher
	{
		private IEnumerable<FileSystemWatcher> fileSystemWatchers;
		private readonly ILogger logger;

		public DirectoryWatcher(IEnumerable<string> directories, ILogger logger)
		{
			this.fileSystemWatchers = this.CreateFileSystemWatchers(directories).ToList();
			this.logger = logger;
		}

		public event EventHandler<FileCreatedEventArgs> FileCreated;

		private void OnFileCreated(FileModel file)
		{
			FileCreated?.Invoke(this, new FileCreatedEventArgs(file));
		}

		private IEnumerable<FileSystemWatcher> CreateFileSystemWatchers(IEnumerable<string> directories)
		{
			foreach(var directory in directories)
			{
				FileSystemWatcher watcher = new FileSystemWatcher(directory)
				{
					NotifyFilter = NotifyFilters.FileName,
					IncludeSubdirectories = false,
					EnableRaisingEvents = true
				};

				watcher.Created += (sender, eventArgs) =>
				{
					logger.Log(string.Format(Resource.FileCreated, eventArgs.FullPath));
					OnFileCreated(new FileModel { Name = eventArgs.Name, FullPath = eventArgs.FullPath });
				};

				yield return watcher;
			}
		}
	}
}
