using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NorthwindListener.Interface.RequestModels
{
	public class GetOrdersRequest
	{
		public string CustomerID { get; set; }

		public DateTime? From { get; set; }

		public DateTime? To { get; set; }

		public int? Skip { get; set; }

		public int? Take { get; set; }
	}
}
