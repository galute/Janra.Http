using System;
using System.Collections.Generic;
using System.Net;
using Janra.Http.Internal.Network.Api;
using Janra.Http.Internal.Network.Wrappers;

namespace Janra.Http.Internal.Network.Models
{
	public class TcpAddress : ITcpHostAddress
	{
		private string _url;
		private readonly IDnsLookup _lookup;

		public int Port { get; set;}

		public TcpAddress(IDnsLookup lookup)
		{
			_lookup = lookup;
		}

		public IEnumerable<IEndPoint> GetAddress(string url)
		{
			_url = url;
			IList<IEndPoint> retval = new List<IEndPoint>();
			IPHostEntry addresses;

			try
			{
				addresses = _lookup.GetIpForUrl(_url);
			}
			catch (Exception ex)
			{
				throw new DnsLookupException(string.Format("DNS Lookup for {0} failed. {1}", _url, ex.Message));
			}

			foreach (IPAddress address in addresses.AddressList)
			{
				retval.Add(new EndPointImp(address, Port, _url)); 
			}

			return retval;
		}
	}
}

