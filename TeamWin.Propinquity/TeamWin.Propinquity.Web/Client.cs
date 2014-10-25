using TeamWin.Propinquity.Web.LocationUtils;

namespace TeamWin.Propinquity.Web
{
    public class Client
    {
        private readonly string _id;

        public Client(string id)
        {
            _id = id;
        }

        public string Id
        {
            get { return _id; }
        }

        private Location _location;
        public Location Location
        {
            get { return _forcedLocation ?? _location; }
            set { _location = value; }
        }

        private Location _forcedLocation;
        public Location ForcedLocation
        {
            set { _forcedLocation = value; }
        }

        public string OpenTokToken { get; set; }

	    public Channel CurrentChannel { get; set; }
    }
}