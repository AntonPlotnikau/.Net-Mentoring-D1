using DIContainer.Tests.TestData;
using NUnit.Framework;
using System;
using System.Reflection;

namespace DIContainer.Tests
{
	[TestFixture]
	public class ContainerTests
	{
		private Container container;

		[SetUp]
		public void Initialize()
		{
			container = new Container();
		}

		[Test]
		public void CreateInstance_TypeIsNotInTypesRepository_ArgumentException()
			=> Assert.Throws<ArgumentException>(() => container.CreateInstance(typeof(CustomerBLL)));

		[Test]
		public void CreateInstance_AddingTypesUsingAssembly_CorrectResultType()
		{
			container.AddAssembly(Assembly.GetExecutingAssembly());

			var customerBll = container.CreateInstance(typeof(CustomerBLL));

			Assert.NotNull(customerBll);
			Assert.True(customerBll.GetType() == typeof(CustomerBLL));
		}

		[Test]
		public void CreateInstance_AddingTypesDirectly_CorrectResultType()
		{
			container.AddType(typeof(CustomerBLL));
			container.AddType(typeof(Logger));
			container.AddType(typeof(CustomerDAL), typeof(ICustomerDAL));

			var customerBll = container.CreateInstance(typeof(CustomerBLL));

			Assert.NotNull(customerBll);
			Assert.True(customerBll.GetType() == typeof(CustomerBLL));
		}
	}
}
