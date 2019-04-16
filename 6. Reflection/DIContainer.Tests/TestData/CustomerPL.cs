using DIContainer.Attributes;

namespace DIContainer.Tests.TestData
{
	[ImportConstructor]
	public class CustomerPL
	{
		public CustomerPL(CustomerBLL customerBLL)
		{

		}
	}
}
