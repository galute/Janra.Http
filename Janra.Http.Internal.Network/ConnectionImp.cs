using System;
using Janra.Http.Internal.Network;
using Janra.Http.Internal.Network.Wrappers;
using System.Threading.Tasks;

namespace Janra.Http.Internal.Network
{
	public class ConnectionImp : IConnection, IDisposable
	{
		ITcpSocket _socket;
		IEndPoint _endPoint;

		public ConnectionImp(IEndPoint endPoint, ITcpSocket socket)
		{
			_endPoint = endPoint;
			_socket = socket;
		}

		public void Connect()
		{
			_socket.Connect(_endPoint);
		}

		public void Read()
		{
			if (!IsConnected())
			{
				throw new NotConnectedException("Attempt to read when not connected.");
			}
			throw new NotImplementedException();
		}

		public async Task ReadAsync()
		{
			if (!IsConnected())
			{
				throw new NotConnectedException("Attempt to read(async) when not connected.");
			}
			throw new NotImplementedException();
		}

		public void Write()
		{
			if (!IsConnected())
			{
				throw new NotConnectedException("Attempt to write when not connected.");
			}

			throw new NotImplementedException();
		}

		public async Task WriteAsync()
		{
			if (!IsConnected())
			{
				throw new NotConnectedException("Attempt to write(async) when not connected.");
			}

			throw new NotImplementedException();
		}

		public void Close()
		{
			_socket.Close();
		}

		public void Dispose()
		{
			throw new NotImplementedException();
		}

		private bool IsConnected()
		{
			return _socket.IsConnected();
		}
	}
}

