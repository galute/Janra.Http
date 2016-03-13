using System.Net;
using Janra.Http.Internal.Network.Api;

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

		public IPEndPoint Value
		{
			get
			{
				return _endpoint;
			}
		}

		public string ServerName
		{
			get
			{
				return _url;
			}
		}
	}
}

