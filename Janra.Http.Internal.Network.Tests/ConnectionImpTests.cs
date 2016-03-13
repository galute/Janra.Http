using NUnit.Framework;
using FakeItEasy;
using Janra.Http.Internal.Network.Wrappers;
using System;
using System.Threading.Tasks;

namespace Janra.Http.Internal.Network.Tests
{
	[TestFixture]
	public class ConnectionImpTests
	{
		private ITcpSocket _socket;
		private IEndPoint _endpoint;
		private IConnection _connection;

		[SetUp]
		public void Setup()
		{
			_socket = A.Fake<ITcpSocket>();
			_endpoint = A.Fake<IEndPoint>();
			_connection = new ConnectionImp(_endpoint, _socket);
		}

		[Test]
		public void ConnectCallsIntoSocketConnect()
		{
			_connection.Connect();
			A.CallTo(() => _socket.Connect(A<IEndPoint>.Ignored)).MustHaveHappened();
		}

		[Test]
		public void ReadThrowsExceptionIfNotConnected()
		{
			A.CallTo(() => _socket.IsConnected()).Returns(false);
			Assert.That(_connection.Read, Throws.InstanceOf<NotConnectedException>());
		}

		[Test]
		public async Task ReadAsyncThrowsExceptionIfNotConnected()
		{
			A.CallTo(() => _socket.IsConnected()).Returns(false);

			await AssertThrowsAsync<NotConnectedException>(_connection.ReadAsync);
		}

		[Test]
		public void WriteThrowsExceptionIfNotConnected()
		{
			A.CallTo(() => _socket.IsConnected()).Returns(false);
			Assert.That(_connection.Write, Throws.InstanceOf<NotConnectedException>());
		}

		[Test]
		public async Task WriteAsyncThrowsExceptionIfNotConnected()
		{
			A.CallTo(() => _socket.IsConnected()).Returns(false);

			await AssertThrowsAsync<NotConnectedException>(_connection.WriteAsync);
		}


		private async Task AssertThrowsAsync<TException>(Func<Task> func)
		{
			var expected = typeof (TException);

			Type actual = null;

			try
			{
				await func();
			}
			catch (Exception ex)
			{
				actual = ex.GetType();
			}

			Assert.NotNull(actual);
			Assert.AreEqual(expected, actual);
		}

	}
}

