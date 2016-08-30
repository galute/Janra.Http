using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using Janra.Http.Internal.Network.Api;
using Janra.Http.Internal.Network.Api.Exceptions;
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
		    IPHostEntry addresses;

			try
			{
				addresses = _lookup.GetIpForUrl(_url);
			}
			catch (Exception ex)
			{
				throw new DnsLookupException($"DNS Lookup for {_url} failed. {ex.Message}");
			}

		    return addresses.AddressList.Select(address => new EndPointImp(address, Port, _url)).Cast<IEndPoint>().ToList();
		}
	}
}

