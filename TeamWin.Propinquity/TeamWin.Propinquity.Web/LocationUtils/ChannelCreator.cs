using System;
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
                var foundChannel = false;

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
            // Special case: only one channel, with only me in it - return that channel
            if (client == null)
                throw new ArgumentNullException("client");
            if (currentChannels == null)
                throw new ArgumentNullException("currentChannels");

            if (currentChannels.Count == 1 && currentChannels.First().Users.Count() == 1 && currentChannels.First().Users.First() == client)
            {
                return client.CurrentChannel;
            }

            // otherwise look at other channels to join them
            foreach (var channel in currentChannels.Where(c => c != client.CurrentChannel))
            {
                foreach (var channelUser in channel.Users.Where(x => x != client))
                {
                    if (channelUser.Location == null)
                        throw new InvalidOperationException("Mid loop: channelUser.Location == null");
                    if (client.Location == null)
                        throw new InvalidOperationException("Mid loop: client.Locationn == null");

                    if (channelUser.Location.DistanceToInKm(client.Location) < CHANNEL_INCLUSION_THRESHOLD_KM)
                    {
                        // Remove myself from the old channel
                        if (client.CurrentChannel == null)
                            throw new InvalidOperationException("Mid loop: client.CurrentChannel == null");

                        client.CurrentChannel.Users.Remove(client);

                        // limbo <here>

                        // Join the other channel
                        return channel;
                    }
                }
            }

            // Do I have a channel? If so, am I allowed to stay in my channel? If so, return that...
            if (client.CurrentChannel != null)
            {
                // Only one person in this channel
                if (client.CurrentChannel.Users.Count == 1)
                {
                    return client.CurrentChannel;
                }

                foreach (var channelUser in client.CurrentChannel.Users.Where(x => x != client).ToArray())
                {
                    if (channelUser.Location == null)
                        throw new InvalidOperationException("Last loop: channelUser.Location == null");

                    if (channelUser.Location.DistanceToInKm(client.Location) < CHANNEL_INCLUSION_THRESHOLD_KM)
                    {
                        return client.CurrentChannel;
                    }
                }

                if (client.CurrentChannel == null)
                    throw new InvalidOperationException("Last loop: client.CurrentChannel == null");

                // Remove myself from the old channel
                client.CurrentChannel.Users.Remove(client);
            }

            // otherwise can't find one, so create me a new channel
            return null;
        }
    }
}