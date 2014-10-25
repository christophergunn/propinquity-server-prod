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

        public Location Location { get; set; }

        public string OpenTokToken { get; set; }

	    public Channel CurrentChannel { get; set; }
    }
}