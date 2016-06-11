using NUnit.Framework;
using Janra.Http.Internal.Protocol.Models;
using Janra.Http.Internal.Protocol.Parsers;

namespace Janra.Http.Internal.Protocol.Tests.Parsers
{
	[TestFixture]
	public class UrlParserTests
	{
		[TestCase("http://www.mysite.com:400/my/end/point", SchemeType.Http, "www.mysite.com", 400, "my/end/point")]
		[TestCase ("http://www.mysite.com/my/end/point", SchemeType.Http, "www.mysite.com", 80, "my/end/point")]
		[TestCase ("https://www.mysite.com:400/my/end/point", SchemeType.Https, "www.mysite.com", 400, "my/end/point")]
		[TestCase ("https://www.mysite.com/my/end/point", SchemeType.Https, "www.mysite.com", 443, "my/end/point")]
		[TestCase ("hrps://www.mysite.com/my/end/point", SchemeType.Unknown, "www.mysite.com", 80, "my/end/point")]
		public void Test(string url, SchemeType scheme, string site, int port, string endpoint)
		{
			var unitUnderTest = new UrlParser();
			unitUnderTest.ParseIt(url);

			Assert.That (unitUnderTest.Port, Is.EqualTo (port));
			Assert.That (unitUnderTest.Protocol, Is.EqualTo (scheme));
			Assert.That (unitUnderTest.Host, Is.EqualTo (site));
			Assert.That (unitUnderTest.EndPoint, Is.EqualTo (endpoint));
		}
	}
}

