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
}