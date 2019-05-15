using NorthwindListener.DAL.Interface.Models;
using NorthwindListener.Interface.Interfaces;
using NorthwindListener.Interface.RequestModels;
using NorthwindListener.Interface.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace NorthwindListener.BLL.Services
{
	public class NorthwindOrdersListener : INortwindOrdersListener
	{
		private readonly HttpListener listener;
		private readonly IRequestParser<GetOrdersRequest> parser;
		private readonly IOrderRepository repository;
		private readonly IXmlConverter converter;
		private bool isStarted = false;

		public NorthwindOrdersListener(IRequestParser<GetOrdersRequest> parser, IOrderRepository repository, IXmlConverter converter)
		{
			this.listener = new HttpListener();
			this.parser = parser;
			this.repository = repository;
			this.converter = converter;
		}

		public void StartListen(string prefix)
		{
			if (isStarted)
			{
				this.StopListen();
			}

			listener.Prefixes.Add(prefix);
			listener.Start();
			this.ProcessRequest();
		}

		public void StopListen()
		{
			if (!isStarted)
			{
				return;
			}

			listener.Stop();
			isStarted = false;
		}

		public void ProcessRequest()
		{
			var context = listener.GetContext();
			var request = context.Request;
			var response = context.Response;

			var requestModel = new GetOrdersRequest();

			if (request.HttpMethod == "POST" && request.InputStream != null)
			{
				requestModel = parser.ParseRequestBody(request.InputStream);
			}
			else
			{
				var dataFromQuery = request.Url.ParseQueryString();
				requestModel = parser.ParseRequestQueryString(dataFromQuery);
			}

			var views = GetOrderViews(requestModel);

			SendResponce(response, views, request.AcceptTypes);
		}

		private IEnumerable<OrderViewModel> GetOrderViews(GetOrdersRequest requestModel)
		{
			var orders = repository.GetOrders(o => 
			{
				bool result = o.CustomerID == requestModel.CustomerID;
				if (requestModel.From.HasValue)
				{
					result = result && o.OrderDate > requestModel.From;
				}
				else if(requestModel.To.HasValue)
				{
					result = result && o.OrderDate < requestModel.To;
				}

				return result;
			});

			if (requestModel.Skip.HasValue && requestModel.Take.HasValue)
			{
				return orders
					.Skip(requestModel.Skip.Value)
					.Take(requestModel.Take.Value)
					.Select(o => new OrderViewModel(o))
					.OrderBy(o => o.OrderID);
			}

			if (requestModel.Skip.HasValue)
			{
				return orders
					.Skip(requestModel.Skip.Value)
					.Select(o => new OrderViewModel(o))
					.OrderBy(o => o.OrderID);
			}

			if (requestModel.Take.HasValue)
			{
				return orders
					.Take(requestModel.Skip.Value)
					.Select(o => new OrderViewModel(o))
					.OrderBy(o => o.OrderID);
			}

			return orders.Select(o => new OrderViewModel(o)).OrderBy(o => o.OrderID);
		}

		private void SendResponce(HttpListenerResponse response, IEnumerable<OrderViewModel> views, string[] accepts)
		{
			if(accepts.Any(a => a == @"text/xml"))
			{
				response.AppendHeader("Content-Type", "text/xml");
			}

			if (accepts.Any(a => a == @"text/xml"))
			{
				response.AppendHeader("Content-Type", "text/xml");
			}
		}
	}
}
