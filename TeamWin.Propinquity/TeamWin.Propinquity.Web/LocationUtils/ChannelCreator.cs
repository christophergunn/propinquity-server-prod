using System.Collections.Generic;

namespace TeamWin.Propinquity.Web.LocationUtils
{
    public class ChannelCreator
    {
        private readonly double _channelInclusionThresholdKm;

        public ChannelCreator(double channelInclusionThresholdKm = 0.1)
        {
            _channelInclusionThresholdKm = channelInclusionThresholdKm;
        }

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
                        if (channelUser.Location.DistanceToInKm(client.Location) < _channelInclusionThresholdKm)
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
    }
}