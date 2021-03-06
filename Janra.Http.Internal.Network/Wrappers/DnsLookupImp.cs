﻿using System.Net;
using System.Threading.Tasks;

namespace Janra.Http.Internal.Network.Wrappers
{
	public class DnsLookupImp :IDnsLookup
	{
		public IPHostEntry GetIpForUrl(string url)
		{
			return Dns.GetHostEntry(url);
		}

		public async Task<IPHostEntry> GetIpForUrlAsync(string url)
		{
			return await Dns.GetHostEntryAsync(url);
		}

	}
}

