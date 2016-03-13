using Janra.Http.Internal.Protocol.Models;

namespace Janra.Http.Internal.Protocol.Parsers
{
	public class SchemeParser
	{
		public int DefaultPort { get; private set;}
		public SchemeType Protocol {get; set;}

		public unsafe char * Parse(char * ptr)
		{
			var retVal = ptr;
			var protocol = new char[6];
			var idx = 0;
			char * pChar = ptr;
			var correctSeparator = 0;
			var isFinished = false;

			while (!isFinished)
			{
				char currentChar = *pChar;

				switch (currentChar)
				{
					case ':':
						retVal = pChar;
						if (correctSeparator == 0)
						{
							correctSeparator++;
						}
						else
						{
							isFinished = true;
						}

						break;
					case '/':
						if (correctSeparator == 1)
						{
							correctSeparator++;
						}
						else if (correctSeparator == 2)
						{
							isFinished = true;
							retVal = ++pChar;
						}

						break;
					case '.':
						protocol = "http".ToCharArray();
						retVal = ptr;
						isFinished = true;
						break;
					default:
						retVal = pChar;
						protocol[idx++] = currentChar;
						break;
				}
						
				pChar++;
			}

			Protocol = Compare(protocol);

			return retVal;
		}

		public unsafe SchemeType Compare(char[] protocolArray)
		{
			fixed (char * pProtocol = protocolArray)
			{
				var protocolTxt = new string(pProtocol);

				if (protocolTxt.Equals("http"))
				{
					DefaultPort = 80;
					return SchemeType.Http;
				}

				if (protocolTxt.Equals("https"))
				{
					DefaultPort = 443;
					return SchemeType.Https;
				}
			}

			return SchemeType.Unknown;
		}
	}
}
