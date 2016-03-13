using System;

namespace Janra.Http.Internal.Network
{
	public class NotConnectedException : Exception
	{
		public NotConnectedException(string message) : base(message)
		{
			
		}
	}

}

