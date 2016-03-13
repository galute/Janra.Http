using System;

namespace Janra.Http.Internal.Protocol
{
	public class HostParser
	{
		public string Host {get; set;}

		public unsafe char * Parse(char * ptr)
		{
			var retVal = ptr;
			var host = new char[128];
			var idx = 0;
			char * pChar = ptr;
			var isFinished = false;

			while (!isFinished)
			{
				char currentChar = *pChar;

				if (Char.IsNumber(currentChar))
				{
					host[idx++] = currentChar;
				}
				else
				{
					switch (currentChar)
					{
						case '/':
						case ':':
						case '?':
						case '\0':
							retVal = pChar;
							isFinished = true;
							break;
						default:
							retVal = pChar;
							host[idx++] = currentChar;
							break;
					}
				}

				pChar++;
			}

			fixed (char * pHost = host)
			{
				Host = new string(pHost);
			}

			return retVal;
		}
	}
}

