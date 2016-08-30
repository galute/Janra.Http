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
			var protocol = new char[1024];
			var idx = 0;
			var pChar = ptr;
			var correctSeparator = 0;
			var isFinished = false;

			while (!isFinished)
			{
				var currentChar = *pChar;

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
						switch (correctSeparator)
						{
						    case 1:
						        correctSeparator++;
						        break;
						    case 2:
						        isFinished = true;
						        retVal = ++pChar;
						        break;
						    default:
						        // makes no sense so return
						        isFinished = true;
						        break;
						}

						break;
					case '\0':
						protocol = "http".ToCharArray();
						retVal = ptr;
						isFinished = true;
					    break;
					default:
					    // Valid scheme characters are alphanumeric or +, - or . as defined rfc3986 para 3.1
					    if (currentChar == '+' ||
					        currentChar == '.' ||
					        currentChar == '-' ||
					        char.IsLetterOrDigit(currentChar))
						{
							retVal = pChar;
							protocol [idx++] = currentChar;
						}
						
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
				DefaultPort = 80;

				if (protocolTxt.Equals("http"))
				{
					return SchemeType.Http;
				}

			    if (!protocolTxt.Equals("https"))
			    {
			        return SchemeType.Unknown;
			    }
			    DefaultPort = 443;
			    return SchemeType.Https;
			}
		}
	}
}
