namespace Janra.Http.Internal.Protocol.Parsers
{
	public class HostParser
	{
		public string Host {get; set;}

		public unsafe char * Parse(char * ptr)
		{
			var retVal = ptr;
			var host = new char[128];
			var idx = 0;
			var pChar = ptr;
			var isFinished = false;

			while (!isFinished)
			{
				var currentChar = *pChar;

				if (char.IsNumber(currentChar))
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
