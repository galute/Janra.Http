using System;
using Janra.Http.Internal.Protocol.Models;


namespace Janra.Http.Internal.Protocol.Parsers
{
	public class UrlParser
	{
		public int Port { get; set;}
		public SchemeType Protocol { get; set;}
		public string Host { get; set; }
		public string EndPoint { get; set; }

		private readonly SchemeParser _schemeParser;
		private readonly HostParser _hostParser;
		private readonly PortParser _portParser;
		private readonly EndPointParser _endPointParser;

		public UrlParser()
		{
			_schemeParser = new SchemeParser();
			_hostParser = new HostParser();
			_portParser = new PortParser();
			_endPointParser = new EndPointParser ();
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
				pChar = _portParser.Parse (pChar);
				Port = _portParser.Port;
				if (Port == -1)
				{
					Port = _schemeParser.DefaultPort;
				}
				pChar = _endPointParser.Parse (pChar);
				EndPoint = _endPointParser.EndPoint;
			}
		}

		public LocalUri GetUri()
		{
			return new LocalUri { Host = Host, Port = Port, EndPoint = EndPoint, Scheme = Protocol };
		}
	}
}

