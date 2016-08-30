using System;

namespace Janra.Http.Internal.Protocol.Parsers
{
	public class EndPointParser
	{
		public string EndPoint {get; set;}

		public unsafe char * Parse(char * ptr)
		{
			var retVal = ptr;
			var endPoint = new char[2048];
			var idx = 0;
			var pChar = ptr;
			var isFinished = false;

			if (*pChar == '/')
			{
				pChar++;
			}

			while (!isFinished)
			{
				var currentChar = *pChar;

				if (char.IsNumber(currentChar))
				{
					endPoint[idx++] = currentChar;
				}
				else
				{
					switch (currentChar)
					{
						case '\0':
							retVal = pChar;
							isFinished = true;
							break;
						default:
							retVal = pChar;
							endPoint[idx++] = currentChar;
							break;
					}
				}

				pChar++;
			}

			fixed (char * pHost = endPoint)
			{
				EndPoint = new string(pHost);
			}

			return retVal;
		}
	}
}

