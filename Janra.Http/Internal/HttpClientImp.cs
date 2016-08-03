using Janra.Http.Api;
using Janra.Http.Internal.Protocol.Models;
using Janra.Http.Internal.Protocol.Parsers;

namespace Janra.Http.Internal
{
	public class HttpClientImp : IHttpClient
	{
		UrlParser _parser;

		public HttpClientImp()
		{
			_parser = new UrlParser ();
		}

		public HttpClientResponse Get(string endpoint)
		{
			if (endpoint == null)
			{
				throw new InvalidUrlException("Endpoint cannot be null for GET operation");
			}

			_parser.ParseIt(endpoint);
			var request = _parser.GetUri();

			return new HttpClientResponse(HttpStatus.OK);
		}

		public HttpClientResponse Get(string protocol, string host, string endpoint, int port = 84)
		{
			if (protocol == null || endpoint == null || host == null)
			{
				throw new InvalidUrlException("Parameter(s) cannot be null for GET operation");
			}

			var schemeParser = new SchemeParser();

			var scheme = schemeParser.Compare(protocol.ToCharArray());

			if (port == 84)
			{
				port = schemeParser.DefaultPort;
			}

			var url = new LocalUri(){Scheme = scheme, Host = host, EndPoint = endpoint, Port = port};

			return new HttpClientResponse(HttpStatus.OK);
		}
	}
}

