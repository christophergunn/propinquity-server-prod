using TeamWin.Propinquity.Web.LocationUtils;

namespace TeamWin.Propinquity.Web
{
    public class Client
    {
        private readonly string _id;
		private Location _forcedLocation;
		private string _avatarName;

        public Client(string id)
        {
            _id = id;
	        _avatarName = "AVATAR";
        }

        public string Id
        {
            get { return _id; }
        }

	    public string AvatarName
	    {
		    get { return _avatarName; }
	    }

        private Location _location;
        public Location Location
        {
            get { return _forcedLocation ?? _location; }
            set { _location = value; }
        }


	    public Location ForcedLocation
        {
            set { _forcedLocation = value; }
        }

        public string OpenTokToken { get; set; }

	    public Channel CurrentChannel { get; set; }
    }
}