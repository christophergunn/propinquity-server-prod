using System.Collections.Generic;

namespace TeamWin.Propinquity.Web.LocationUtils
{
    public class Channel
    {
        private List<Client> _users = new List<Client>();

        public Channel(Client theFirstUser)
        {
            _users.Add(theFirstUser);
        }

        public List<Client> Users { get { return _users; } }
    }
}