using Crawler.Interface.Models;
using System;

namespace ConsoleUI
{
	public class DomainConstraint : IConstraint
	{
		private readonly string domain;

		public DomainConstraint(string domain)
		{
			this.domain = domain;
		}


		public bool IsAccepted(string url)
		{
			Uri uri = new Uri(url);

			if (uri.Host.Equals(domain))
			{
				return true;
			}

			return false;
		}
	}
}
