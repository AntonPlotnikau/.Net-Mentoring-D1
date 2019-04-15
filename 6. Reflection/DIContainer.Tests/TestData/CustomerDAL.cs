using DIContainer.Attributes;

namespace DIContainer.Tests.TestData
{
	[Export(typeof(ICustomerDAL))]
	public class CustomerDAL: ICustomerDAL
	{
	}
}
