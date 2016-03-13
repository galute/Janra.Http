using System;
using Janra.Http.Internal.Protocol.Models;


namespace Janra.Http.Internal.Protocol.Parsers
{
	public class UrlParser
	{
		private enum ParseState
		{
			Unknown,
			ProtocolStarted,
		}
		public int Port { get; set;}
		public SchemeType Protocol { get; set;}
		public string Host { get; set; }
		public string EndPoint { get; set; }

		private ParseState _machineState = ParseState.Unknown;
		private readonly SchemeParser _schemeParser;
		private readonly HostParser _hostParser;
		private readonly PortParser _portParser;

		public UrlParser()
		{
			_schemeParser = new SchemeParser();
			_hostParser = new HostParser();
			_portParser = new PortParser();
		}

		public unsafe void ParseIt(string url)
		{
			var charArray = url.Trim().ToLower().ToCharArray();
			var strLength = url.Length;

			fixed (char * pString = charArray)
			{
				char * pChar = pString;

				pChar = _schemeParser.Parse(pChar);
				Protocol = _schemeParser.Protocol;
				pChar = _hostParser.Parse(pChar);
				Host = _hostParser.Host;


				if (*pChar == ':')
				{
					pChar++;
					pChar = _portParser.Parse(pChar);
					Port = _portParser.Port;
				}
				else
				{
					Port = _schemeParser.DefaultPort;
				}


				for (int i = 0; i < strLength; i++)
				{
					char currentChar = *pChar;

					switch (currentChar)
					{
						case ':':
							break;
						case '/':
							break;
						case '\0':
							break;
						default:
							break;
					}

					pChar++;
				}
			}
		}
	}
}

