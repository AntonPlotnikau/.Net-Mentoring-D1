using NorthwindListener.Interface.Interfaces;
using NorthwindListener.Interface.RequestModels;
using System;
using System.Collections.Specialized;
using System.IO;

namespace NorthwindListener.BLL.Services
{
	public class RequestParser : IRequestParser<GetOrdersRequest>
	{
		public GetOrdersRequest ParseRequestBody(Stream stream)
		{
			throw new NotImplementedException();
		}

		public GetOrdersRequest ParseRequestQueryString(NameValueCollection values)
		{
			var result = new GetOrdersRequest();

			result.CustomerID = values["customerId"];
			try
			{
				result.From = DateTime.Parse(values["from"]);
			}
			catch (Exception)
			{
				result.From = null;
			}

			try
			{
				result.To = DateTime.Parse(values["to"]);
			}
			catch (Exception)
			{
				result.To = null;
			}

			try
			{
				result.Skip = int.Parse(values["skip"]);
			}
			catch (Exception)
			{
				result.Skip = null;
			}

			try
			{
				result.Take = int.Parse(values["take"]);
			}
			catch (Exception)
			{
				result.Take = null;
			}

			return result;
		}
	}
}
