using System;

namespace Janra.Http.Internal.Network
{
	public class StreamNotWriteableException : Exception
	{
		public StreamNotWriteableException(string message) : base(message)
		{
		}
	}
}

