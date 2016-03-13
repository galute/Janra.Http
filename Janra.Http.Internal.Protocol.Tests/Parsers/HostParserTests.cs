using System;
using NUnit.Framework;

namespace Janra.Http.Internal.Protocol.Tests
{
	[TestFixture]
	public class HostParserTests
	{
		private HostParser _unitUnderTest;

		[SetUp]
		public void Setup()
		{
			_unitUnderTest = new HostParser();
		}

		[Test]
		[TestCase("my.website.com", "my.website.com", '\0')]
		[TestCase("my.website.com/", "my.website.com", '/')]
		[TestCase("my.website.com:1000", "my.website.com", ':')]
		[TestCase("my.website.com?query", "my.website.com", '?')]
		public unsafe void ShouldExtractHostFromURl(string url, string host, char ptr)
		{
			fixed (char* pChar = url.ToCharArray())
			{
				var result = _unitUnderTest.Parse(pChar);

				Assert.That(_unitUnderTest.Host, Is.EqualTo(host));
				Assert.That(*result, Is.EqualTo(ptr));
			}
		}
	}
}

