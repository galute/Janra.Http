using Janra.Http.Internal.Network.Api;
using Janra.Http.Internal.Protocol.Models;

namespace Janra.Http.Internal.Network
{
    public class Session
    {
        private readonly IConnection _connection;
        private readonly LocalUri _uri;

        //TODO do DNS lookup and establish a connection
        public Session(LocalUri uri, IConnection connection)
        {
            _connection = connection;
            _uri = uri;
        }



    }
}