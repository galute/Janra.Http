using System.Threading.Tasks;

namespace Janra.Http.Internal.Network
{
	public interface IConnection
	{
		void Connect();
		byte[] Read(int numBytes);
		Task<byte[]> ReadAsync(int numBytes);
		void Write(byte[] bytes);
		Task WriteAsync(byte[] bytes);
	}
}

