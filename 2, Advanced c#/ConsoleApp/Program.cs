using FileSystemHelper.Models;
using FileSystemHelper.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp
{
	class Program
	{
		static void Main(string[] args)
		{
			var visitor = new FileSystemVisitor(new DirectoryService(), (path) => path == @"D:\.Net-Mentoring-D1\.git\hooks");
			visitor.FilteredDirectoryFinded += (o, e) => e.Status = SearchStatus.Skip;
			visitor.DirectoryFinded += (o, e) => Console.WriteLine(e.Path);
			visitor.GetDirectoryTree(@"‪‪D:\.Net-Mentoring-D1");
		}
	}
}
