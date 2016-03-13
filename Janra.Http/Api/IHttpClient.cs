using System;

namespace Janra.Http.Api
{
	public interface IHttpClient
	{
		HttpClientResponse Get(string endpoint);

		HttpClientResponse Get(string protocol, string host, string endpoint, int port = 84);
	}
}
	