using System.Collections.Generic;
using OpenTokSDK;

namespace TeamWin.Propinquity.Web.LocationUtils
{
    public class Channel
    {
<<<<<<< HEAD
        private readonly List<Client> _users = new List<Client>();
=======
        private List<Client> _users = new List<Client>();
>>>>>>> origin/master

        public Channel(Client theFirstUser)
        {
            _users.Add(theFirstUser);
<<<<<<< HEAD
            theFirstUser.CurrentChannel = this;
=======
	        theFirstUser.CurrentChannel = this;
>>>>>>> origin/master
        }

        public List<Client> Users { get { return _users; } }

<<<<<<< HEAD
        public Session Session { get; set; }
=======
		public Session Session { get; set; }
>>>>>>> origin/master
    }
}