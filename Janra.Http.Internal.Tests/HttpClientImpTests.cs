using NUnit.Framework;
using Janra.Http.Api;

namespace Janra.Http.Internal.Tests
{
	[TestFixture]
	public class HttpClientImpTests
	{
		private HttpClientImp _unitUnderTest;

		[SetUp]
		public void Setup()
		{
			_unitUnderTest = new HttpClientImp();
		}

		[Test]
		public void GetOperationThrowsExceptionWhenUriIsNull()
		{
			Assert.That(() => _unitUnderTest.Get(null), Throws.InstanceOf<InvalidUrlException>());
		}

		[Test]
		public void GetOperationReturnsHttpClientResponse()
		{
			var result = _unitUnderTest.Get("http://www.mysite.com:999");
			Assert.That(result, Is.InstanceOf<HttpClientResponse>());
		}
	}
}

