using FileSystemHelper.interfaces;
using FileSystemHelper.models;
using System;
using System.Collections.Generic;

namespace FileSystemHelper.services
{
	public class FileSystemVisitor
	{
		private readonly IDirectoryService directoryService;
		private readonly Predicate<string> searchPattern;

		public event EventHandler<EventArgs> Start;
		public event EventHandler<EventArgs> Finish;
		public event EventHandler<FileSystemEventArgs> FileFinded;
		public event EventHandler<FileSystemEventArgs> DirectoryFinded;
		public event EventHandler<FileSystemEventArgs> FilteredFileFinded;
		public event EventHandler<FileSystemEventArgs> FilteredDirectoryFinded;

		public FileSystemVisitor(IDirectoryService directoryService)
		{
			this.directoryService = directoryService ?? throw new ArgumentNullException(nameof(directoryService));
		}

		public FileSystemVisitor(IDirectoryService directoryService, Predicate<string> searchPattern)
		{
			this.directoryService = directoryService ?? throw new ArgumentNullException(nameof(directoryService));
			this.searchPattern = searchPattern;
		}

		public DirectoryModel GetDirectoryTree(string path)
		{
			if (path == null)
			{
				throw new ArgumentNullException(nameof(path));
			}

			if (!directoryService.DirectoryExists(path))
			{
				throw new ArgumentException($"It is not valid directory path: {path}");
			}

			Start?.Invoke(this, new EventArgs());
			var directory = GetDirectory(path);
			Finish?.Invoke(this, new EventArgs());

			return GetDirectory(path);
		}

		private DirectoryModel GetDirectory(string path)
		{
			DirectoryModel directory = new DirectoryModel
			{
				DirectoryPath = path,
				ChildrenDirectories = GetChildrenDirectories(path),
				Files = GetFiles(path)
			};

			return directory;
		}

		private IEnumerable<DirectoryModel> GetChildrenDirectories(string path)
		{
			var directories = directoryService.GetDirectories(path);

			foreach (var directory in directories) 
			{
				var findArgs = new FileSystemEventArgs(directory);
				DirectoryFinded?.Invoke(this, findArgs);
				if (findArgs.Status == SearchStatus.Cancel)
				{
					break;
				}

				if (findArgs.Status == SearchStatus.Skip)
				{
					continue;
				}

				if (searchPattern != null && searchPattern(directory))
				{
					var filterArgs = new FileSystemEventArgs(directory);
					FilteredDirectoryFinded?.Invoke(this, filterArgs);
					if (filterArgs.Status == SearchStatus.Cancel)
					{
						break;
					}

					if (filterArgs.Status == SearchStatus.Skip)
					{
						continue;
					}
				}

				yield return GetDirectory(directory);
			}
		}

		private IEnumerable<string> GetFiles(string path)
		{
			var files = directoryService.GetFiles(path);

			foreach (var file in files) 
			{
				var findArgs = new FileSystemEventArgs(file);
				FileFinded?.Invoke(this, findArgs);
				if (findArgs.Status == SearchStatus.Cancel) 
				{
					break;
				}

				if (findArgs.Status == SearchStatus.Skip)
				{
					continue;
				}

				if (searchPattern != null && searchPattern(file))
				{
					var filterArgs = new FileSystemEventArgs(file);
					FilteredFileFinded?.Invoke(this, filterArgs);
					if (filterArgs.Status == SearchStatus.Cancel)
					{
						break;
					}

					if (filterArgs.Status == SearchStatus.Skip)
					{
						continue;
					}
				}

				yield return file ;
			}
		}
	}
}
