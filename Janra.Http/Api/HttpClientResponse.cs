namespace Janra.Http.Api
{
	public class HttpClientResponse
	{
		public HttpStatus Status { get; private set;}

		public HttpClientResponse(HttpStatus status)
		{
			Status = status;
		}
	}
}
