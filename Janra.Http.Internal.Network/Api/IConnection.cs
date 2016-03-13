using System.Threading.Tasks;

namespace Janra.Http.Internal.Network
{
	public interface IConnection
	{
		void Connect();
		void Read();
		Task ReadAsync();
		void Write();
		Task WriteAsync();
	}
}

