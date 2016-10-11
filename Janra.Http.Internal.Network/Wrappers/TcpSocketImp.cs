using System.Net.Sockets;
using Janra.Http.Internal.Network.Api;

namespace Janra.Http.Internal.Network.Wrappers
{
	public class TcpSocketImp : ITcpSocket
	{
		private readonly Socket _socket;
		private ITcpStream _stream;

		public TcpSocketImp()
		{
			_socket = new Socket(AddressFamily.InterNetwork,
				SocketType.Stream,
				ProtocolType.Tcp);
			_stream = null;
		}

		public void Connect(IEndPoint endPoint)
		{
			_socket.Connect(endPoint.Value);

			if(IsConnected())
			{
				_stream = new TcpStreamImp(_socket);
			}
		}

		public bool IsConnected()
		{
			return _socket.Connected;
		}

		public ITcpStream Stream()
		{
			return _stream;
		}

		public void Close()
		{
			_socket.Shutdown(SocketShutdown.Both);
		    _stream.Dispose();
		    _stream = null;
		}

		public void Dispose()
		{
		    // The stream will call Dispose() on it's associated socket
			_stream.Dispose();
		}

		~TcpSocketImp()
		{
			Close();
		}
	}
}

