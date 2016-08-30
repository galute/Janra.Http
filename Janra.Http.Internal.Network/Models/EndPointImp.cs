using System.Net;

namespace Janra.Http.Internal.Network.Models
{
	public class EndPointImp : IEndPoint
	{
		private readonly IPEndPoint _endpoint;
		private readonly string _url;

		public EndPointImp(IPAddress address, int port, string url)
		{
			_endpoint = new IPEndPoint(address, port);
			_url = url;
		}

	    IPEndPoint IEndPoint.Value => _endpoint;

	    string IEndPoint.ServerName => _url;
	}
}

