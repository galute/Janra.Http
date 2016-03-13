using NUnit.Framework;
using Janra.Http.Internal.Protocol.Models;
using Janra.Http.Internal.Protocol.Parsers;

namespace Janra.Http.Internal.Protocol.Tests
{
	[TestFixture]
	public class SchemeParserTests
	{
		private SchemeParser _unitUnderTest;

		[SetUp]
		public void Setup()
		{
			_unitUnderTest = new SchemeParser();
		}

		[Test]
		[TestCase("http://my.website.com", SchemeType.Http, 'm')]
		[TestCase("https://my.website.com", SchemeType.Https, 'm')]
		[TestCase("https://my.website.com:1000", SchemeType.Https, 'm')]
		[TestCase("http://my.website.com:1000", SchemeType.Http, 'm')]
		[TestCase("my.website.com:1000", SchemeType.Http, 'm')]
		[TestCase("my.website.com", SchemeType.Http, 'm')]
		[TestCase("abc://my.website.com", SchemeType.Unknown, 'm')]
		public unsafe void ShouldExtractSchemeFromURl(string url, SchemeType protocol, char ptr)
		{
			fixed (char* pChar = url.ToCharArray())
			{
				var result = _unitUnderTest.Parse(pChar);

				Assert.That(_unitUnderTest.Protocol, Is.EqualTo(protocol));
				Assert.That(*result, Is.EqualTo(ptr));
			}
		}
	}
}

