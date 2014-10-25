using System;
using System.Collections.Generic;
using System.Linq;

namespace TeamWin.Propinquity.Web
{
    public class GpsUpdater
    {
        private readonly Dictionary<string, Client> _clients = new Dictionary<string, Client>(); 

        public void UpdateClientPosition(string id, string lat, string lon)
        {
            var latDec = double.Parse(lat);
            var lonDec = double.Parse(lon);

            _clients[id] = new Client(id) { Location = new Location(latDec, lonDec) };
        }

        public string GetAllClients()
        {
            return String.Concat(from kvp in _clients select "Id: " + kvp.Key + ", At: " + kvp.Value.Location + " <br/>");
        }
    }
}