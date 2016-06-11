using System;
using FakeItEasy;
using Janra.Http.Internal.Network.Models;
using Janra.Http.Internal.Network.Wrappers;
using NUnit.Framework;

namespace Janra.Http.Internal.Network.Tests.Models
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

