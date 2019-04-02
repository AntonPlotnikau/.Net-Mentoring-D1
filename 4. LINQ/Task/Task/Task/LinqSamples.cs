// Copyright © Microsoft Corporation.  All Rights Reserved.
// This code released under the terms of the 
// Microsoft Public License (MS-PL, http://opensource.org/licenses/ms-pl.html.)
//
//Copyright (C) Microsoft Corporation.  All rights reserved.

using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Windows.Forms;
using System.Xml.Linq;
using SampleSupport;
using Task.Data;

// Version Mad01

namespace SampleQueries
{
	[Title("LINQ Module")]
	[Prefix("Linq")]
	public class LinqSamples : SampleHarness
	{

		private DataSource dataSource = new DataSource();

		[Category("Restriction Operators")]
		[Title("Where - Task 1")]
		[Description("This sample uses the where clause to find all elements of an array with a value less than 5.")]
		public void Linq1()
		{
			int[] numbers = { 5, 4, 1, 3, 9, 8, 6, 7, 2, 0 };

			var lowNums =
				from num in numbers
				where num < 5
				select num;

			Console.WriteLine("Numbers < 5:");
			foreach (var x in lowNums)
			{
				Console.WriteLine(x);
			}
		}

		[Category("Restriction Operators")]
		[Title("Where - Task 2")]
		[Description("This sample return return all presented in market products")]

		public void Linq2()
		{
			var products =
				from p in dataSource.Products
				where p.UnitsInStock > 0
				select p;

			foreach (var p in products)
			{
				ObjectDumper.Write(p);
			}
		}

		[Category("Task")]
		[Title("Task 1")]
		[Description("Get a list of all customers whose total turnover (the sum of all orders) exceeds some value of X")]
		public void Linq001()
		{
			decimal x = 20000;

			var customers = dataSource.Customers.Where(c => c.Orders.Sum(o => o.Total) > x);

			foreach (var customer in customers)
			{
				ObjectDumper.Write($"{customer.CustomerID} {customer.CompanyName} {customer.Orders.Sum(o => o.Total)}");
			}
		}

		[Category("Task")]
		[Title("Task 2")]
		[Description("Make a list of suppliers located in the same country and the same city for each client")]
		public void Linq002()
		{
			var customers = dataSource.Customers
				.Select(c => new
				{
					c.CustomerID,
					c.Country,
					c.City,
					Suppliers = dataSource.Suppliers.Where(s => s.Country == c.Country && s.City == c.City)
				});

			foreach (var customer in customers)
			{
				foreach (var supplier in customer.Suppliers)
				{
					ObjectDumper.Write($"{customer.CustomerID} {customer.Country} {customer.City} {supplier.SupplierName} {supplier.Country} {supplier.City} ");
				}
			}
		}

		[Category("Task")]
		[Title("Task 3")]
		[Description("Find all customers who have orders that exceed the sum of X")]
		public void Linq003()
		{
			decimal x = 10000;

			var customers = dataSource.Customers.Where(c => c.Orders.Any(o => o.Total > x));

			foreach (var customer in customers)
			{
				ObjectDumper.Write($"{customer.CustomerID} {customer.CompanyName} {customer.Orders.Max(o => o.Total)}");
			}
		}

		[Category("Task")]
		[Title("Task 4")]
		[Description("Get a list of customers including the month of the year they became clients (consider client first order month as a required date)")]
		public void Linq004()
		{
			var customers = dataSource.Customers
				.Where(c => c.Orders != null && c.Orders.Length > 0)
				.Select(c => new
				{
					c.CustomerID,
					c.CompanyName,
					FirstOrderDate = c.Orders.OrderBy(o => o.OrderDate).Select(o => o.OrderDate).First()
				}
				);

			foreach (var customer in customers)
			{
				ObjectDumper.Write($"{customer.CustomerID} {customer.CompanyName} {customer.FirstOrderDate}");
			}
		}

		[Category("Task")]
		[Title("Task 5")]
		[Description("Do the previous task, but get the list sorted by year, month, customer turnover (from the maximum to the minimum), and the client's name")]
		public void Linq005()
		{
			var customers = dataSource.Customers
				.Where(c => c.Orders != null && c.Orders.Length > 0)
				.Select(c => new
				{
					c.CustomerID,
					c.CompanyName,
					Turnover = c.Orders.Sum(o => o.Total),
					FirstOrderDate = c.Orders.OrderBy(o => o.OrderDate).Select(o => o.OrderDate).First()
				}
				)
				.OrderBy(c => c.FirstOrderDate.Year)
				.ThenBy(c => c.FirstOrderDate.Month)
				.ThenByDescending(c => c.Turnover)
				.ThenBy(c => c.CustomerID);

			foreach (var customer in customers)
			{
				ObjectDumper.Write($"{customer.CustomerID} {customer.CompanyName} {customer.FirstOrderDate} {customer.Turnover}");
			}
		}

		[Category("Task")]
		[Title("Task 6")]
		[Description("Specify all customers who have a non-digital postal code, or the region is not filled, or the operator code is not specified in the phone")]
		public void Linq006()
		{
			var customers = dataSource.Customers
				.Where(
					c => (c.PostalCode != null && c.PostalCode.Any(p => p < '0' || p > '9'))
					|| string.IsNullOrWhiteSpace(c.Region)
					|| c.Phone.FirstOrDefault() != '('
				);

			foreach (var customer in customers)
			{
				ObjectDumper.Write($"Id: {customer.CustomerID} Company: {customer.CompanyName} Code: {customer.PostalCode} Region: {customer.Region} Phone: {customer.Phone}");
			}
		}

		[Category("Task")]
		[Title("Task 7")]
		[Description("Group all products by category, inside - by stock, within the last group sort by cost")]
		public void Linq007()
		{
			var groups = dataSource.Products
				.GroupBy(products => products.Category)
				.Select(group => new
				{
					CategoryName = group.Key,
					ProductsByStock = group.GroupBy(productsInGroup => productsInGroup.UnitsInStock > 0)
						.Select(productGroup => new
						{
							IsInStock = productGroup.Key,
							Products = productGroup.OrderBy(prod => prod.UnitPrice)
						})
				});

			foreach (var productsByCategory in groups)
			{
				ObjectDumper.Write($"Category: {productsByCategory.CategoryName}");
				foreach (var productsByStock in productsByCategory.ProductsByStock)
				{
					ObjectDumper.Write($"Is in stock: {productsByStock.IsInStock}");
					foreach (var product in productsByStock.Products)
					{
						ObjectDumper.Write($"Product: {product.ProductName} Price: {product.UnitPrice}");
					}
				}
			}
		}

		[Category("Task")]
		[Title("Task 8")]
		[Description("Group the goods into groups cheap, average price, expensive")]
		public void Linq008()
		{
			var productGroups = dataSource.Products
				.GroupBy(p => p.UnitPrice < 15 ? "cheap" :
							p.UnitPrice > 30 ?	"expensive" :
												"average price"
				);

			foreach (var productGroup in productGroups)
			{
				ObjectDumper.Write(productGroup.Key);
				foreach (var product in productGroup) 
				{
					ObjectDumper.Write($"Name: {product.ProductName} UnitPrice: {product.UnitPrice}");
				}
			}
		}

		[Category("Task")]
		[Title("Task 9")]
		[Description("Calculate the average profitability of each city and the average intensity ")]
		public void Linq009()
		{
			var statistics = dataSource.Customers.GroupBy(c => c.City)
				.Select(c => new
				{
					City = c.Key,
					AverageProfitability = c.Average(p => p.Orders.Sum(o => o.Total)),
					AverageIntensity = c.Average(p => p.Orders.Count())
				}
				);

			foreach (var item in statistics)
			{
				ObjectDumper.Write($"City: {item.City} AverageProfitability: {item.AverageProfitability} AverageIntensity: {item.AverageIntensity}");
			}
		}

		[Category("Task")]
		[Title("Task 10")]
		[Description("Make the average annual activity statistics of clients by month (excluding the year), statistics by year, by year and by month")]
		public void Linq0010()
		{
			var activityByMonth = dataSource.Customers
				.SelectMany(c => c.Orders
					.Select(o => new
					{
						Customer = c,
						Order = o
					})
				)
				.GroupBy(c => new { c.Order.OrderDate.Month, c.Customer.CustomerID })
				.Select(c => new
				{
					c.Key.Month,
					OrdersCount = c.Count()
				})
				.GroupBy(c => c.Month)
				.Select(c => new
				{
					Month = c.Key,
					AverageOrdersCount = c.Average(o => o.OrdersCount)
				})
				.OrderBy(c => c.Month);

			var activityByYear = dataSource.Customers
				.SelectMany(c => c.Orders
					.Select(o => new
					{
						Customer = c,
						Order = o
					})
				)
				.GroupBy(c => new { c.Order.OrderDate.Year, c.Customer.CustomerID })
				.Select(c => new
				{
					c.Key.Year,
					OrdersCount = c.Count()
				})
				.GroupBy(c => c.Year)
				.Select(c => new
				{
					Year = c.Key,
					AverageOrdersCount = c.Average(o => o.OrdersCount)
				})
				.OrderBy(c => c.Year);

			var activityByMonthAndYear = dataSource.Customers
				.SelectMany(c => c.Orders
					.Select(o => new
					{
						Customer = c,
						Order = o
					})
				)
				.GroupBy(c => new { c.Order.OrderDate.Year, c.Order.OrderDate.Month, c.Customer.CustomerID })
				.Select(c => new
				{
					c.Key.Year,
					c.Key.Month,
					OrdersCount = c.Count()
				})
				.GroupBy(c => new { c.Year, c.Month })
				.Select(c => new
				{
					c.Key.Year,
					c.Key.Month,
					AverageOrdersCount = c.Average(o => o.OrdersCount)
				})
				.OrderBy(c => c.Year)
				.ThenBy(c => c.Month);

			ObjectDumper.Write("-----------ActivityByMonth------------");
			foreach (var monthActivity in activityByMonth)
			{
				ObjectDumper.Write($"Month: {monthActivity.Month} AverageOrdersCount: {monthActivity.AverageOrdersCount}");
			}

			ObjectDumper.Write("-----------ActivityByYear------------");
			foreach (var yearActivity in activityByYear)
			{
				ObjectDumper.Write($"Year: {yearActivity.Year} AverageOrdersCount: {yearActivity.AverageOrdersCount}");
			}

			ObjectDumper.Write("-----------ActivityByMonthAndYear------------");
			foreach (var yearAndMonthActivity in activityByMonthAndYear)
			{
				ObjectDumper.Write($"Year: {yearAndMonthActivity.Year} Month: {yearAndMonthActivity.Month} AverageOrdersCount: {yearAndMonthActivity.AverageOrdersCount}");
			}
		}
	}
}
