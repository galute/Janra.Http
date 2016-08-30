using System;
using Janra.Http.Internal.Network.Wrappers;
using System.Threading.Tasks;
using Janra.Http.Internal.Network.Api;

namespace Janra.Http.Internal.Network
{
	public class ConnectionImp : IConnection, IDisposable
	{
		private readonly ITcpSocket _socket;
		private readonly IEndPoint _endPoint;

		public ConnectionImp(IEndPoint endPoint, ITcpSocket socket)
		{
			_endPoint = endPoint;
			_socket = socket;
		}

		public void Connect()
		{
			_socket.Connect(_endPoint);
		}

		public byte[] Read(int numBytes)
		{
			if (!IsConnected())
			{
				throw new NotConnectedException("Attempt to read when not connected.");
			}

			if (numBytes == 0)
			{
				return new byte[0];
			}
			return _socket.Stream().Read(numBytes);
		}

		public async Task<byte[]> ReadAsync(int numBytes)
		{
			if (!IsConnected())
			{
				throw new NotConnectedException("Attempt to read(async) when not connected.");
			}

			if (numBytes == 0)
			{
				return new byte[0];
			}
			return await _socket.Stream().ReadAsync(numBytes);
		}

		public void Write(byte[] bytes)
		{
			if (!IsConnected())
			{
				throw new NotConnectedException("Attempt to write when not connected.");
			}

			if (bytes.GetLength(0) == 0)
			{
				return;
			}

			_socket.Stream().Write(bytes);
		}

		public async Task WriteAsync(byte[] bytes)
		{
			if (!IsConnected())
			{
				throw new NotConnectedException("Attempt to write(async) when not connected.");
			}

			if (bytes.GetLength(0) == 0)
			{
				return;
			}

			await _socket.Stream().WriteAsync(bytes);
		}

		public void Close()
		{
			Dispose();
		}

		public void Dispose()
		{
			_socket.Dispose();
		}

		~ConnectionImp()
		{
			Dispose();
		}

		private bool IsConnected()
		{
			return _socket.IsConnected();
		}
	}
}

