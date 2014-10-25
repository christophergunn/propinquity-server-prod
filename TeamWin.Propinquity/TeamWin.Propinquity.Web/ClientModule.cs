using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace TeamWin.Propinquity.Web
{
    public class ClientModule : Nancy.NancyModule
    {
        private readonly GpsUpdater _gps;

        public ClientModule(GpsUpdater gps)
        {
            _gps = gps;

            Get["/client/gps/add/{id}/{lat}/{lon}"] = parameters =>
                {
                    var id = parameters.id;
                    var lat = parameters.lat;
                    var lon = parameters.lon;

                    _gps.UpdateClientPosition(id, lat, lon);

                    return _gps.GetAllClients();
                };

            Post["/client/gps"] = _ =>
                {
                    var id = Request.Form.id;
					var lat = Request.Form.lat;
					var lon = Request.Form.lon;

                    _gps.UpdateClientPosition(id, lat, lon);

                    return 200;
                };

            Get["/client/gps"] = _ => _gps.GetAllClients();
        }
    }

    public class GpsUpdater
    {
        private Dictionary<string, Location> _clients = new Dictionary<string, Location>(); 

        public void UpdateClientPosition(string id, string lat, string lon)
        {
            var latDec = Decimal.Parse(lat);
            var lonDec = Decimal.Parse(lon);

            _clients[id] = new Location(latDec, lonDec);
        }


        public string GetAllClients()
        {
            return String.Concat(from kvp in _clients select "Id: " + kvp.Key + ", At: " + kvp.Value + " <br/>");
        }
    }

    public class Location
    {
        private readonly decimal _lat;
        private readonly decimal _lon;

        public Location(decimal lat, decimal lon)
        {
            _lat = lat;
            _lon = lon;
        }

        public decimal Lat
        {
            get { return _lat; }
        }

        public decimal Lon
        {
            get { return _lon; }
        }

        public override string ToString()
        {
            return "Lat: " + Lat + ", Lon: " + Lon;
        }
    }
}