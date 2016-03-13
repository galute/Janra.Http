using NUnit.Framework;
using Janra.Http.Internal.Protocol.Parsers;

namespace Janra.Http.Internal.Protocol.Tests
{
	[TestFixture]
	public class PortParserTests
	{
		private PortParser _unitUnderTest;

		[SetUp]
		public void Setup()
		{
			_unitUnderTest = new PortParser();
		}

		[Test]
		[TestCase("80/my/endpoint", 80, '/')]
		[TestCase("443/my/endpoint", 443, '/')]
		[TestCase("1000/", 1000, '/')]
		[TestCase("80a/", 80, '8')]
		[TestCase("1000.", 80, '1')]
		[TestCase("abc", 80, 'a')]
		[TestCase("80", 80, '\0')]
		public unsafe void Test(string url, int port, char ptr)
		{
			fixed (char* pChar = url.ToCharArray())
			{
				var result = _unitUnderTest.Parse(pChar);

				Assert.That(_unitUnderTest.Port, Is.EqualTo(port));
				Assert.That(*result, Is.EqualTo(ptr));
			}
		}
	}
}

