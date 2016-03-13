using Janra.Http.Internal.Network.Api;

namespace Janra.Http.Internal.Network.Wrappers
{
	public interface ITcpSocket
	{
		void Connect(IEndPoint endPoint);
		bool IsConnected();
		ITcpStream Stream();
		void Close();
	}
}

