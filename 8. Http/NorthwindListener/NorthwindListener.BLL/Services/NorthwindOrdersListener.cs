using NorthwindListener.DAL.Interface.Models;
using NorthwindListener.Interface.Interfaces;
using NorthwindListener.Interface.RequestModels;
using NorthwindListener.Interface.ViewModels;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using System.Web;

namespace NorthwindListener.BLL.Services
{
	public class NorthwindOrdersListener : INortwindOrdersListener
	{
		private readonly HttpListener listener;
		private readonly IRequestParser<GetOrdersRequest> parser;
		private readonly IOrderRepository repository;
		private readonly IXmlConverter converter;

		public bool IsStarted { get; private set; } = false;

		public NorthwindOrdersListener(IRequestParser<GetOrdersRequest> parser, IOrderRepository repository, IXmlConverter converter)
		{
			this.listener = new HttpListener();
			this.parser = parser;
			this.repository = repository;
			this.converter = converter;
		}

		public void StartListen(string prefix)
		{
			if (IsStarted)
			{
				this.StopListen();
			}

			listener.Prefixes.Add(prefix);
			listener.Start();
			IsStarted = true;
		}

		public void StopListen()
		{
			if (!IsStarted)
			{
				return;
			}

			listener.Stop();
			IsStarted = false;
		}

		public void ProcessRequest()
		{
			var context = listener.GetContext();
			var request = context.Request;
			var response = context.Response;

			var requestModel = new GetOrdersRequest();

			if (request.HttpMethod == "POST" && request.InputStream != null)
			{
				using(var reader = new StreamReader(request.InputStream))
				{
					var data = reader.ReadToEnd();

					var values = HttpUtility.ParseQueryString(data);
					requestModel = parser.ParseRequestQueryString(values);
				}
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
					.OrderBy(o => o.OrderID)
					.ToList();
			}

			if (requestModel.Skip.HasValue)
			{
				return orders
					.Skip(requestModel.Skip.Value)
					.Select(o => new OrderViewModel(o))
					.OrderBy(o => o.OrderID)
					.ToList();
			}

			if (requestModel.Take.HasValue)
			{
				return orders
					.Take(requestModel.Take.Value)
					.Select(o => new OrderViewModel(o))
					.OrderBy(o => o.OrderID)
					.ToList();
			}

			return orders.Select(o => new OrderViewModel(o)).OrderBy(o => o.OrderID).ToList();
		}

		private void SendResponce(HttpListenerResponse response, IEnumerable<OrderViewModel> views, string[] accepts)
		{
			using(var stream = new MemoryStream())
			{
				if (accepts.Any(a => a == @"text/xml"))
				{
					converter.ConvertToXml(views, stream);
					response.AppendHeader("Content-Type", "text/xml");
				}
				else if (accepts.Any(a => a == @"application/xml"))
				{
					converter.ConvertToXml(views, stream);
					response.AppendHeader("Content-Type", "application/xml");
				}
				else{
					converter.ConvertToExcel(views, stream);
					response.AppendHeader("Content-Type", "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet");
				}


				response.StatusCode = (int)HttpStatusCode.OK;

				stream.Seek(0, SeekOrigin.Begin);
				stream.WriteTo(response.OutputStream);
				response.OutputStream.Flush();
				response.OutputStream.Close();
			}
		}
	}
}
