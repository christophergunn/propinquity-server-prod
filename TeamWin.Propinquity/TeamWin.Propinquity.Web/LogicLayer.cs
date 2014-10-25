using TeamWin.Propinquity.Web.OpenTok;

namespace TeamWin.Propinquity.Web
{
    public class LogicLayer
    {
        private readonly ClientDataStore _clientDataStore;
        private readonly OpenTokService _openTok;

        public LogicLayer(ClientDataStore clientDataStore, OpenTokService openTok)
        {
            _clientDataStore = clientDataStore;
            _openTok = openTok;
        }

        public SessionAndToken ProcessGpsUpdate(string id, string lat, string lon)
        {
            var client = _clientDataStore.UpdateClientPosition(id, lat, lon);

	        _openTok.AssignChannel(client);

            return new SessionAndToken { SessionId = client.CurrentChannel.Session.Id, Token = client.OpenTokToken };
        }

        public string GetAllClients()
        {
            return _clientDataStore.GetAllClients();
        }

        public void ProcessGpsForce(string id, string lat, string lon)
        {
            _clientDataStore.ForceClientPosition(id, lat, lon);
        }

        public void ProcessGpsUnforce(string id)
        {
            _clientDataStore.UnForceClientPosition(id);
        }
    }

    public class SessionAndToken
    {
        public string SessionId { get; set; }
        public string Token { get; set; }
    }
}