using System.Collections.Generic;
using System.Linq;

namespace TeamWin.Propinquity.Web.LocationUtils
{
    public class ChannelCreator
    {
	    private const double CHANNEL_INCLUSION_THRESHOLD_KM = 0.1;

	    public IList<Channel> Create(IEnumerable<Client> clients)
        {
            var toReturn = new List<Channel>();

            foreach (var client in clients)
            {
                bool foundChannel = false;

                foreach (var existingChannel in toReturn)
                {
                    foreach (var channelUser in existingChannel.Users.ToArray())
                    {
                        if (channelUser.Location.DistanceToInKm(client.Location) < CHANNEL_INCLUSION_THRESHOLD_KM)
                        {
                            existingChannel.Users.Add(client);
                            foundChannel = true;
                        }
                    }

                    if (foundChannel) break;
                }

                if (!foundChannel)
                {
                    toReturn.Add(new Channel(client));
                }
            }

            return toReturn;
        }

	    public static Channel FindChannelFor(Client client, IList<Channel> currentChannels)
	    {
		    foreach (var channel in currentChannels)
		    {
				foreach (var channelUser in channel.Users.Where(x => x != client))
			    {
					if (channelUser.Location.DistanceToInKm(client.Location) < CHANNEL_INCLUSION_THRESHOLD_KM)
					{
						return channel;
					}
			    }
		    }

		    return null;
	    }
    }
}