using NUnit.Framework;
using FakeItEasy;
using Janra.Http.Internal.Network.Wrappers;
using System;
using System.Threading.Tasks;
using Janra.Http.Internal.Network.Api;

namespace Janra.Http.Internal.Network.Tests
{
	[TestFixture]
	public class ConnectionImpTests
	{
		private ITcpSocket _socket;
		private IEndPoint _endpoint;
		private IConnection _connection;
		private ITcpStream _stream;

		[SetUp]
		public void Setup()
		{
			_socket = A.Fake<ITcpSocket>();
			_endpoint = A.Fake<IEndPoint>();
			_stream = A.Fake<ITcpStream>();
			_connection = new ConnectionImp(_endpoint, _socket);

			A.CallTo(() => _socket.IsConnected()).Returns(true);
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
			Assert.That(() => _connection.Read(1), Throws.InstanceOf<NotConnectedException>());
		}

		[Test]
		public async Task ReadAsyncThrowsExceptionIfNotConnected()
		{
			A.CallTo(() => _socket.IsConnected()).Returns(false);

			await AssertThrowsAsync<NotConnectedException>(() => _connection.ReadAsync(1));
		}

		[Test]
		public void WriteThrowsExceptionIfNotConnected()
		{
			A.CallTo(() => _socket.IsConnected()).Returns(false);
			Assert.That(() => _connection.Write(null), Throws.InstanceOf<NotConnectedException>());
		}

		[Test]
		public async Task WriteAsyncThrowsExceptionIfNotConnected()
		{
			A.CallTo(() => _socket.IsConnected()).Returns(false);

			await AssertThrowsAsync<NotConnectedException>(() => _connection.WriteAsync(null));
		}

		[Test]
		public void ReadReturnsZeroBytesWithoutCallingStreamIfZeroRequested()
		{
			var returnedBytes = _connection.Read(0);
			A.CallTo(() => _socket.Stream()).MustNotHaveHappened();
			Assert.That(returnedBytes.GetLength(0), Is.EqualTo(0));
		}

		[Test]
		public async Task ReadAsyncReturnsZeroBytesWithoutCallingStreamIfZeroRequested()
		{
			var returnedBytes = await _connection.ReadAsync(0);
			A.CallTo(() => _socket.Stream()).MustNotHaveHappened();
			Assert.That(returnedBytes.GetLength(0), Is.EqualTo(0));
		}

		[Test]
		public void WriteReturnsWithoutCallingStreamIfPassedEmptyArray()
		{
			_connection.Write(new byte[0]);
			A.CallTo(() => _socket.Stream()).MustNotHaveHappened();
		}

		[Test]
		public async Task WriteAsyncReturnsWithoutCallingStreamIfPassedEmptyArray()
		{
			await _connection.WriteAsync(new byte[0]);
			A.CallTo(() => _socket.Stream()).MustNotHaveHappened();
		}

		[Test]
		[TestCase(3)]
		[TestCase(5)]
		public void ReadReturnsCorrectNumberOfBytesFromStream(int requested)
		{
			var bytesRequested = new byte[requested];

			for(int i = 0; i < bytesRequested.GetLength(0); i++)
			{
				bytesRequested[i] = 0xaa;
			}

			A.CallTo(() => _socket.Stream()).Returns(_stream);
			A.CallTo(() => _stream.Read(requested)).Returns(bytesRequested);

			var bytesReturned = _connection.Read(requested);

			Assert.That(bytesReturned.GetLength(0), Is.EqualTo(requested));
			foreach(byte val in bytesReturned)
			{
				Assert.That(val, Is.EqualTo(0xaa));
			}
		}

		[Test]
		[TestCase(3)]
		[TestCase(5)]
		public async Task ReadAsyncReturnsCorrectNumberOfBytesFromStream(int requested)
		{
			var bytesRequested = new byte[requested];

			for(int i = 0; i < bytesRequested.GetLength(0); i++)
			{
				bytesRequested[i] = 0xaa;
			}

			A.CallTo(() => _socket.Stream()).Returns(_stream);
			A.CallTo(() => _stream.ReadAsync(requested)).Returns(bytesRequested);

			var bytesReturned = await _connection.ReadAsync(requested);

			Assert.That(bytesReturned.GetLength(0), Is.EqualTo(requested));
			foreach(byte val in bytesReturned)
			{
				Assert.That(val, Is.EqualTo(0xaa));
			}
		}

		[Test]
		[TestCase(3)]
		[TestCase(5)]
		public void WritePassesCorrectNumberOfBytestoStream(int numBytesToWrite)
		{
			var bytesToWrite = new byte[numBytesToWrite];

			for(int i = 0; i < bytesToWrite.GetLength(0); i++)
			{
				bytesToWrite[i] = 0xaa;
			}

			A.CallTo(() => _socket.Stream()).Returns(_stream);

			_connection.Write(bytesToWrite);

			A.CallTo(() => _stream.Write(bytesToWrite)).MustHaveHappened();
		}

		[Test]
		[TestCase(3)]
		[TestCase(5)]
		public async Task WriteAsyncPassesCorrectNumberOfBytestoStream(int numBytesToWrite)
		{
			var bytesToWrite = new byte[numBytesToWrite];

			for(int i = 0; i < bytesToWrite.GetLength(0); i++)
			{
				bytesToWrite[i] = 0xaa;
			}

			A.CallTo(() => _socket.Stream()).Returns(_stream);

			await _connection.WriteAsync(bytesToWrite);

			A.CallTo(() => _stream.WriteAsync(bytesToWrite)).MustHaveHappened();
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

