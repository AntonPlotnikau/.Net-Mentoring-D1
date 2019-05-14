using System;
using System.Collections.Generic;
using System.Linq;
using NorthwindListener.DAL.Interface.Models;
using NorthwindListener.Interface.Interfaces;

namespace NorthwindListener.DAL
{
	public class OrderRepository : IOrderRepository
	{
		public IEnumerable<Order> GetOrders(Func<Order, bool> predicate)
		{
			using (var context = new NorthwindDbContext())
			{
				return context.Orders.Where(predicate);
			}
		}
	}
}
