using NorthwindListener.DAL.Interface.Models;
using System;
using System.Collections.Generic;

namespace NorthwindListener.Interface.Interfaces
{
	public interface IOrderRepository
	{
		IEnumerable<Order> GetOrders(Func<Order, bool> predicate);
	}
}
