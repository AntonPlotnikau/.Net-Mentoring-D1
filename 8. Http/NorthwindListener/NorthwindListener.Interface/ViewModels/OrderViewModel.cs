using NorthwindListener.DAL.Interface.Models;
using System;
using System.Xml.Serialization;

namespace NorthwindListener.Interface.ViewModels
{
	[XmlRoot(ElementName = "orders")]
	public class OrderViewModel
	{
		public OrderViewModel(Order order)
		{
			this.OrderDate = order.OrderDate;
			this.OrderID = order.OrderID;
			this.ShipAddress = order.ShipAddress;
			this.ShipCity = order.ShipCity;
			this.ShipCountry = order.ShipCountry;
			this.ShipName = order.ShipName;
			this.ShipPostalCode = order.ShipPostalCode;
			this.ShipRegion = order.ShipRegion;
		}

		[XmlAttribute(AttributeName = "id")]
		public int OrderID { get; set; }

		[XmlElement(ElementName = "orderDate")]
		public DateTime? OrderDate { get; set; }

		[XmlElement(ElementName = "shipName")]
		public string ShipName { get; set; }

		[XmlElement(ElementName = "shipAddress")]
		public string ShipAddress { get; set; }

		[XmlElement(ElementName = "shipCity")]
		public string ShipCity { get; set; }

		[XmlElement(ElementName = "shipRegion")]
		public string ShipRegion { get; set; }

		[XmlElement(ElementName = "shipPpostalCode")]
		public string ShipPostalCode { get; set; }

		[XmlElement(ElementName = "shipCountry")]
		public string ShipCountry { get; set; }
	}
}
