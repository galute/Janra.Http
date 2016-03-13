using System.Net;
using System.Threading.Tasks;

namespace Janra.Http.Internal.Network.Wrappers
{
	public interface IDnsLookup
	{
		IPHostEntry GetIpForUrl(string url);
		Task<IPHostEntry> GetIpForUrlAsync(string url);
	}
}

