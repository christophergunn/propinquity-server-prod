using System;
using System.Collections.Generic;
using System.Configuration;
using System.Diagnostics;
using OpenTokSDK;
using TeamWin.Propinquity.Web.LocationUtils;

namespace TeamWin.Propinquity.Web.OpenTok
{
    public class OpenTokService : IOpenTokService
    {
		private IList<Channel> _channels = new List<Channel>();

        public Session DefaultSession { get; protected set; }

        public OpenTokSDK.OpenTok OpenTok { get; protected set; }

        public OpenTokService()
        {
            int apiKey = 0;
            string apiSecret = null;
            try
            {
                string apiKeyString = ConfigurationManager.AppSettings["API_KEY"];
                apiSecret = ConfigurationManager.AppSettings["API_SECRET"];
                apiKey = Convert.ToInt32(apiKeyString);
            }

            catch (Exception ex)
            {
                if (!(ex is ConfigurationErrorsException || ex is FormatException || ex is OverflowException))
                {
                    throw ex;
                }
            }

            finally
            {
                if (apiKey == 0 || apiSecret == null)
                {
                    Console.WriteLine(
                        "The OpenTok API Key and API Secret were not set in the application configuration. " +
                        "Set the values in App.config and try again. (apiKey = {0}, apiSecret = {1})", apiKey, apiSecret);
                    Console.ReadLine();
                    Environment.Exit(-1);
                }
            }

            this.OpenTok = new OpenTokSDK.OpenTok(apiKey, apiSecret);

            this.DefaultSession = this.OpenTok.CreateSession();
        }

		public void AssignChannel(Client client)
	    {
		    var channel = ChannelCreator.FindChannelFor(client, _channels);

            Trace.WriteLine("Channel is null: " + (channel == null));

			if (channel == null)
			{
				channel = new Channel(client);
				channel.Session = OpenTok.CreateSession();
				_channels.Add(channel);
				client.OpenTokToken = client.CurrentChannel.Session.GenerateToken();
			}
			else
			{
				if (client.CurrentChannel != channel)
				{
					client.CurrentChannel = channel;
					client.OpenTokToken = client.CurrentChannel.Session.GenerateToken();
                    client.CurrentChannel.Users.Add(client);
				}
			}
	    }
    }

    public interface IOpenTokService
    {
    }
}
