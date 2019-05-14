using System.Collections.Specialized;
using System.IO;


namespace NorthwindListener.Interface.Interfaces
{
	public interface IRequestParser<T>
	{
		T ParseRequestBody(Stream stream);

		T ParseRequestQueryString(NameValueCollection values);
	}
}
