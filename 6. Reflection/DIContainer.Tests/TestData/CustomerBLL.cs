using DIContainer.Attributes;

namespace DIContainer.Tests.TestData
{
	[ImportConstructor]
	public class CustomerBLL
	{
		public CustomerBLL(ICustomerDAL customerDAL, Logger logger)
		{

		}
	}
}
