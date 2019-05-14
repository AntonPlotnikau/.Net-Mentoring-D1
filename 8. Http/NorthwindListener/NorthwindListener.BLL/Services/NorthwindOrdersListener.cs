using NorthwindListener.Interface.Interfaces;
using NorthwindListener.Interface.RequestModels;
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
			this.StartRequestProcessing();
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

		private void StartRequestProcessing()
		{
			while (true)
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


			}
		}
	}
}
