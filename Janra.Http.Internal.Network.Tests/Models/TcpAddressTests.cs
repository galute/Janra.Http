using NUnit.Framework;
using FakeItEasy;
using Janra.Http.Internal.Network.Wrappers;
using Janra.Http.Internal.Network.Models;
using System;

namespace Janra.Http.Internal.Network.Tests
{
	[TestFixture]
	public class TcpAddressTests
	{
		private TcpAddress _unitUnderTest;
		private IDnsLookup _lookup;

		[SetUp]
		public void Setup()
		{
			_lookup = A.Fake<IDnsLookup>();
			_unitUnderTest = new TcpAddress(_lookup);
		}

		[Test]
		public void ShouldThrowExceptionIfUrlInvalid()
		{
			A.CallTo(() => _lookup.GetIpForUrl(A<string>.Ignored)).Throws<ArgumentNullException>().Once();

			Assert.That(() => _unitUnderTest.GetAddress("www.myaddr.com"), Throws.InstanceOf<DnsLookupException>());
		}
	}
}

