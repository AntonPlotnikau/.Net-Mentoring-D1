
using NorthwindListener.Interface.ViewModels;
using System.Collections.Generic;
using System.IO;

namespace NorthwindListener.Interface.Interfaces
{
	public interface IXmlConverter
	{
		void ConvertToXml(IEnumerable<OrderViewModel> orderViews, Stream stream);

		void ConvertToExcel(IEnumerable<OrderViewModel> orderViews, Stream stream);
	}
}
