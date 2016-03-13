using System.Collections.Generic;

namespace Janra.Http.Internal.Network.Api
{
	public interface ITcpHostAddress
	{
		int Port { get; set;}

		IEnumerable<IEndPoint> GetAddress(string url);
	}
}

