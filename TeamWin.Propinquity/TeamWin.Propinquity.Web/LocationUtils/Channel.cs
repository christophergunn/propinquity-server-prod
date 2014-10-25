using System.Collections.Generic;
using OpenTokSDK;

namespace TeamWin.Propinquity.Web.LocationUtils
{
    public class Channel
    {
        private List<Client> _users = new List<Client>();

        public Channel(Client theFirstUser)
        {
            _users.Add(theFirstUser);
	        theFirstUser.CurrentChannel = this;
        }

        public List<Client> Users { get { return _users; } }

		public Session Session { get; set; }
    }
}