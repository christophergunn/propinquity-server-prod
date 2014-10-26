using System;
using System.Collections.Generic;
using System.Linq;

namespace TeamWin.Propinquity.Web
{
    public class ClientDataStore
    {
        private readonly Dictionary<string, Client> _clients = new Dictionary<string, Client>(); 
        private readonly object _syncLock = new object();

        public Client UpdateClientPosition(string id, string lat, string lon)
        {
            lock (_syncLock)
            {
                var latDec = double.Parse(lat);
                var lonDec = double.Parse(lon);

                Client client = null;
                if (!_clients.TryGetValue(id, out client))
                {
                    client = new Client(id);
                    _clients[id] = client;
                }

                client.Location = new Location(latDec, lonDec);

                return client;
            }
        }

        public string GetAllClients()
        {
			return String.Concat(from kvp in _clients where kvp.Value.CurrentChannel != null select "Id: " + kvp.Key + ", At: " + kvp.Value.Location + " Session: " + kvp.Value.CurrentChannel.Session.Id + " Token: " + kvp.Value.OpenTokToken + "<br/>");
        }

        public void ForceClientPosition(string id, string lat, string lon)
        {
            Client client = null;
            if (!_clients.TryGetValue(id, out client))
                return;

            var latDec = double.Parse(lat);
            var lonDec = double.Parse(lon);

            client.ForcedLocation = new Location(latDec, lonDec);
        }

        public void UnForceClientPosition(string id)
        {
            Client client = null;
            if (!_clients.TryGetValue(id, out client))
                return;

            client.ForcedLocation = null;
        }
    }
}