using System;

namespace Janra.Http.Internal.Network
{
	public class StreamNotReadableException : Exception
	{
		public StreamNotReadableException(string message) : base(message)
		{
		}
	}
}

