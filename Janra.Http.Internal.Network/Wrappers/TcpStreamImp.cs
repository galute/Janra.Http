using System.Net.Sockets;
using System.Threading.Tasks;
using Janra.Http.Internal.Network.Api;

namespace Janra.Http.Internal.Network.Wrappers
{
	public class TcpStreamImp : ITcpStream
	{
		private readonly NetworkStream _stream;
		private bool _isDisposed = false;

		public TcpStreamImp(Socket socket)
		{
			_stream = new NetworkStream(socket);
		}

		public byte[] Read(int numberOfBytesToRead)
		{
		    if (!_stream.CanRead)
		    {
		        throw new StreamNotReadableException("NetworkStream is not in a readable state.");
		    }

		    var bytesOut = new byte[numberOfBytesToRead];
		    _stream.Read(bytesOut, 0, numberOfBytesToRead);

		    return bytesOut;
		}

		public async Task<byte[]> ReadAsync(int numberOfBytesToRead)
		{
		    if (!_stream.CanRead)
		    {
		        throw new StreamNotReadableException("NetworkStream is not in a readable state.");
		    }

		    var bytesOut = new byte[numberOfBytesToRead];

		    await _stream.ReadAsync(bytesOut, 0, numberOfBytesToRead);

		    return bytesOut;
		}

		public void Write(byte[] message)
		{
			if (_stream.CanWrite)
			{
				_stream.Write(message, 0, message.Length);
			}
			throw new StreamNotWriteableException("NetworkStream is not in a writeable state.");
		}

		public async Task WriteAsync(byte[] message)
		{
			if (_stream.CanWrite)
			{
				await _stream.WriteAsync(message, 0, message.Length);
			}
			throw new StreamNotWriteableException("NetworkStream is not in a writeable state.");
		}

		public bool IsSecure()
		{
			return false;
		}

		public void Close()
		{
			Dispose();
		}

		public void Dispose()
		{
		    if (_isDisposed)
		    {
		        return;
		    }

		    _isDisposed = true;
		    _stream.Dispose();
		}

		~TcpStreamImp()
		{
			Dispose();
		}
	}
}

