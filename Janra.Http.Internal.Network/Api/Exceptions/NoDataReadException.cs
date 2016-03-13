using System;

namespace Janra.Http.Internal.Network
{
	public class NoDataReadException : Exception
	{
		public NoDataReadException(string message) : base(message)
		{
		}
	}
}

