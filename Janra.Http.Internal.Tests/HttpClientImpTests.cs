using NUnit.Framework;
using FakeItEasy;
using Janra.Http.Api;
using Janra.Http.Internal.Network.Wrappers;

namespace Janra.Http.Internal.Tests
{
	[TestFixture()]
	public class HttpClientImpTests
	{
		private HttpClientImp unitUnderTest;

		[SetUp]
		public void Setup()
		{
			unitUnderTest = new HttpClientImp();
		}

		[Test]
		public void GetOperationThrowsExceptionWhenUriIsNull()
		{
			Assert.That(() => unitUnderTest.Get(null), Throws.InstanceOf<InvalidUrlException>());
		}

		[Test]
		public void GetOperationReturnsHttpClientResponse()
		{
			var result = unitUnderTest.Get("http://www.mysite.com:999");
			Assert.That(result, Is.InstanceOf<HttpClientResponse>());
		}
	}
}

