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

            if (client.OpenTokToken == null)
            {
                client.OpenTokToken = _openTok.DefaultSession.GenerateToken();
            }

            return new SessionAndToken { SessionId = _openTok.DefaultSession.Id, Token = client.OpenTokToken };
        }

        public string GetAllClients()
        {
            return _clientDataStore.GetAllClients();
        }
    }

    public class SessionAndToken
    {
        public string SessionId { get; set; }
        public string Token { get; set; }
    }
}