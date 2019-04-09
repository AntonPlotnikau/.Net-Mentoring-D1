using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FileDistributor.Models
{
	public class Destination
	{
		public string SearchPattern { get; set; }
		
		public string DestinationFolder { get; set; }

		public bool AddDate { get; set; }

		public bool AddNumber { get; set; }
	}
}
