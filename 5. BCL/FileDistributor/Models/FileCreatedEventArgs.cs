using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FileDistributor.Models
{
	public class FileCreatedEventArgs: EventArgs
	{
		public FileModel File { get; private set; }

		public FileCreatedEventArgs(FileModel file)
		{
			this.File = file;
		}
	}
}
