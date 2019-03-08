using System.Collections.Generic;

namespace FileSystemHelper.models
{
	public class DirectoryModel
	{
		public string DirectoryPath { get; set; }

		public IEnumerable<DirectoryModel> ChildrenDirectories { get; set; }

		public IEnumerable<string> Files { get; set; }
	}
}
