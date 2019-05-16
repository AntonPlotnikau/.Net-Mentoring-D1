using NorthwindListener.Interface.Interfaces;
using NorthwindListener.Interface.ViewModels;
using OfficeOpenXml;
using System.Collections.Generic;
using System.IO;
using System.Xml.Serialization;

namespace NorthwindListener.BLL.Services
{
	public class XmlConverter : IXmlConverter
	{
		public void ConvertToExcel(IEnumerable<OrderViewModel> orderViews, Stream stream)
		{
			using (var excel = new ExcelPackage())
			{
				var writer = excel.Workbook.Worksheets.Add("Order List");
				writer.Cells.LoadFromCollection(orderViews, true);
				writer.Cells.AutoFitColumns();
				excel.SaveAs(stream);
			}
		}	

		public void ConvertToXml(IEnumerable<OrderViewModel> orderViews, Stream stream)
		{
			var serializer = new XmlSerializer(typeof(List<OrderViewModel>));
			serializer.Serialize(stream, orderViews);
		}
	}
}
