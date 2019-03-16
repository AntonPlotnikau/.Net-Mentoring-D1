using System;

namespace FileSystemHelper.Models
{
	public class FileSystemEventArgs : EventArgs
	{
		public string Path { get; private set; }

		public SearchStatus Status { get; set; }

		public FileSystemEventArgs(string path)
		{
			this.Path = path;
		}
	}
}
