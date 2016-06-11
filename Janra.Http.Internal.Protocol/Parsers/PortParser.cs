using System;

namespace Janra.Http.Internal.Protocol.Parsers
{
	public class PortParser
	{
		public const int Default = 80;
		public int Port {get; set;}

		public unsafe char * Parse(char * ptr)
		{
			var retVal = ptr;
			var port = new char[10];
			var idx = 0;
			char * pChar = ptr;
			var isFinished = false;
			var parseValue = true;

			while (!isFinished)
			{
				char currentChar = *pChar;

				if (Char.IsNumber(currentChar))
				{
					port[idx++] = currentChar;
				}
				else
				{
					switch (currentChar)
					{
						case '/':
						case '\0':
							retVal = pChar;
							isFinished = true;
							break;
					    case ':':
						    break;
						default:
							Port = Default;
							retVal = ptr;
							parseValue = false;
							isFinished = true;
							break;
					}
				}

				pChar++;
			}

			if (parseValue)
			{
				fixed (char * pPort = port)
				{
					if (*pPort == '\0')
					{
						Port = -1;
					}
					else
					{
						var portTxt = new string (pPort);

						Port = int.Parse (portTxt);
					}

				}
			}

			return retVal;
		}
	}
}

