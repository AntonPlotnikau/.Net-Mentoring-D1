using FileSystemHelper.interfaces;
using FileSystemHelper.models;
using System;

namespace FileSystemHelper.services
{
	public class FileSystemVisitor
	{
		private readonly IDirectoryService directoryService;

		public FileSystemVisitor(IDirectoryService directoryService)
		{
			this.directoryService = directoryService ?? throw new ArgumentNullException(nameof(directoryService));
		}

		public DirectoryModel GetDirectoryTree(string path)
		{
			throw new NotImplementedException();
		}
	}
}
