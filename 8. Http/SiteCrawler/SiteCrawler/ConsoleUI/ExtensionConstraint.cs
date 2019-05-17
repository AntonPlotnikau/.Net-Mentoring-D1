using Crawler.Interface.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleUI
{
	public class ExtensionConstraint : IConstraint
	{
		private readonly string[] extensions;

		public ExtensionConstraint(string[] extensions)
		{
			this.extensions = extensions;
		}

		public bool IsAccepted(string url)
		{
			Uri uri = new Uri(url);

			var lastSegment = uri.Segments.Last();

			var index = lastSegment.LastIndexOf('.');

			if(index == -1 || lastSegment.Length == index + 1)
			{
				return true;
			}

			var currentExtension = lastSegment.Substring(index + 1);

			return !extensions.Any(ex => ex.Equals(currentExtension));
		}
	}
}
