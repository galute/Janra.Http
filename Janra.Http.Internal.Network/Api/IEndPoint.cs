using System.Net;

namespace Janra.Http.Internal.Network
{
	public interface IEndPoint
	{
		IPEndPoint Value { get;}
		string ServerName { get;}
	}
}

