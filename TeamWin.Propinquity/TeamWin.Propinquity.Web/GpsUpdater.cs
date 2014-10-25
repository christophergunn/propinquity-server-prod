using System;
using System.Collections.Generic;
using System.Linq;

namespace TeamWin.Propinquity.Web
{
    public class GpsUpdater
    {
        private Dictionary<string, Location> _clients = new Dictionary<string, Location>(); 

        public void UpdateClientPosition(string id, string lat, string lon)
        {
            var latDec = double.Parse(lat);
            var lonDec = double.Parse(lon);

            _clients[id] = new Location(latDec, lonDec);
        }


        public string GetAllClients()
        {
            return String.Concat(from kvp in _clients select "Id: " + kvp.Key + ", At: " + kvp.Value + " <br/>");
        }
    }
}