using System;
using System.Collections.Generic;
using System.Linq;

namespace TeamWin.Propinquity.Web
{
    public class ClientDataStore
    {
        private readonly Dictionary<string, Client> _clients = new Dictionary<string, Client>(); 

        public Client UpdateClientPosition(string id, string lat, string lon)
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

        public string GetAllClients()
        {
            return String.Concat(from kvp in _clients select "Id: " + kvp.Key + ", At: " + kvp.Value.Location + " <br/>");
        }
    }
}