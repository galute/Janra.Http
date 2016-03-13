using System;

namespace Janra.Http.Api
{
	public class InvalidUrlException : Exception
	{
		public InvalidUrlException(string message) : base(message)
		{
		}
	}
}

