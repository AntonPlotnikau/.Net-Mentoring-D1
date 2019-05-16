using NorthwindListener.BLL.Services;
using NorthwindListener.DAL;
using NorthwindListener.Interface.Interfaces;
using NorthwindListener.Interface.RequestModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NorthwindConsole
{
	class Program
	{
		static void Main(string[] args)
		{
			IOrderRepository repository = new OrderRepository();
			IRequestParser<GetOrdersRequest> parser = new RequestParser();
			IXmlConverter converter = new XmlConverter();
			INortwindOrdersListener listener = new NorthwindOrdersListener(parser, repository, converter);

			Console.CancelKeyPress += (o, e) => listener.StopListen();

			listener.StartListen(@"http://localhost:90/");

			while (listener.IsStarted)
			{
				listener.ProcessRequest();
			}

		}
	}
}
