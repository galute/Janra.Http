using System.Threading.Tasks;

namespace Janra.Http.Internal.Network.Api
{
	public interface ITcpStream
	{
		void Write(byte[] message);
		Task WriteAsync(byte[] message);
		byte[] Read(int numberOfBytesToRead);
		Task<byte[]> ReadAsync(int numberOfBytesToRead);
		bool IsSecure();
		void Close();
		void Dispose();
	}
}

