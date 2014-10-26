using System;
using System.Web;

namespace TeamWin.Propinquity.Web
{
    public class ClientModule : Nancy.NancyModule
    {
        private readonly LogicLayer _logic;

        public ClientModule(LogicLayer logic)
        {
            _logic = logic;

            Get["/client/gps/add/{id}/{lat}/{lon}"] = parameters =>
                {
                    var id = parameters.id;
                    var lat = parameters.lat;
                    var lon = parameters.lon;

                    _logic.ProcessGpsUpdate(id, lat, lon);

                    return _logic.GetAllClients();
                };

            Get["/client/gps/unforce/{id}/"] = parameters =>
            {
                var id = parameters.id;

                _logic.ProcessGpsUnforce(id);

                return _logic.GetAllClients();
            };

            Get["/client/gps/force/{id}/{lat}/{lon}"] = parameters =>
            {
                var id = parameters.id;
                var lat = parameters.lat;
                var lon = parameters.lon;

                _logic.ProcessGpsForce(id, lat, lon);

                return _logic.GetAllClients();
            };

            Post["/client/gps"] = _ =>
                {
                    var id = Request.Form.id;
					var lat = Request.Form.lat;
					var lon = Request.Form.lon;

                    SessionAndToken st;

                    try
                    {
                        st = _logic.ProcessGpsUpdate(id, lat, lon);

                        return st;
                    }
                    catch (Exception e)
                    {
                        return e.Message;
                    }
                };

            Get["/client/gps"] = _ => _logic.GetAllClients();
        }
    }
}