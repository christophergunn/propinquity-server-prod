using System.Collections.Generic;
using System.Linq;
using NUnit.Framework;
using OpenTokSDK;
using TeamWin.Propinquity.Web.LocationUtils;

namespace TeamWin.Propinquity.Web.Tests
{
    [TestFixture]
    public class ChannelCreatorTests
    {
        [Test]
        public void GivenOneUser_ShoudlReturnOneChannelWithThatUser()
        {
            var channelCreator = new ChannelCreator();
            var userWithLocation = new Client("1") { Location = new Location(1, 2) };

            var channels = channelCreator.Create(new[] { userWithLocation });

            Assert.That(channels.Count, Is.EqualTo(1));
        }

        [Test]
        public void GivenTwoUsers_CloseEnough_ShouldReturnOneChannelWithTwoUsers()
        {
            var channelCreator = new ChannelCreator();
            var users = new List<Client> 
            {
                new Client("1") { Location = new Location(1, 2) },
                new Client("2") { Location = new Location(1, 2.0001) }
            };

            var channels = channelCreator.Create(users);

            Assert.That(channels.Count, Is.EqualTo(1));
            Assert.That(channels.First().Users.Count, Is.EqualTo(2));
        }

        [Test]
        public void GivenThreeUsers_TwoCloseEnough_AndOneLoser_ShouldReturnTwoChannels()
        {
            var channelCreator = new ChannelCreator();
            var users = new List<Client> 
            {
                new Client("1") { Location = new Location(1, 2) },
                new Client("2") { Location = new Location(1, 2.0001) },
                new Client("Loser") { Location = new Location(1, 2.1) }
            };

            var channels = channelCreator.Create(users);

            Assert.That(channels.Count, Is.EqualTo(2));
            var channelsWithTwoUsers = channels.Where(c => c.Users.Count == 2);
            Assert.That(channelsWithTwoUsers.Count(), Is.EqualTo(1));
            var channelsWithOneUser = channels.Where(c => c.Users.Count == 1);
            Assert.That(channelsWithOneUser.Count(), Is.EqualTo(1));
        }

        [Test]
        public void GivenFiveUsers_TwoWithFriends_AndOneLoser_ShouldReturnThreeChannels()
        {
            var channelCreator = new ChannelCreator();
            var users = new List<Client> 
            {
                new Client("1") { Location = new Location(1, 2) },
                new Client("2") { Location = new Location(1, 2.0001) },
                new Client("3") { Location = new Location(2, 2) },
                new Client("4") { Location = new Location(2, 2.0001) },
                new Client("Loser") { Location = new Location(1, 2.1) }
            };

            var channels = channelCreator.Create(users);

            Assert.That(channels.Count, Is.EqualTo(3));
            var channelsWithTwoUsers = channels.Where(c => c.Users.Count == 2);
            Assert.That(channelsWithTwoUsers.Count(), Is.EqualTo(2));
            var channelsWithOneUser = channels.Where(c => c.Users.Count == 1);
            Assert.That(channelsWithOneUser.Count(), Is.EqualTo(1));
        }

        [Test]
        public void FindChannelFor_GivenTwoUsersInDifferentLocations_ShouldReturnTheCorrectChannelss()
        {
            var client1 = new Client("1") { Location = new Location(1, 1) };
            var client2 = new Client("2") { Location = new Location(2, 2) };

            var channel1 = new Channel(client1);
            var channel2 = new Channel(client2);

            var channels = new List<Channel>
            {
                channel1,
                channel2
            };

            var foundChannelForUser1 = ChannelCreator.FindChannelFor(client1, channels);
            var foundChannelForUser2 = ChannelCreator.FindChannelFor(client2, channels);

            Assert.That(foundChannelForUser1, Is.EqualTo(channel1));
            Assert.That(foundChannelForUser2, Is.EqualTo(channel2));
        }

        [Test]
        public void FindChannelFor_GivenTwoUsersInDifferentRooms_WhenOneUserJoinsTheOther_TheyShouldBeInTheSameRoom_SoReturnTheSameChannel()
        {
            var client1 = new Client("1") { Location = new Location(1, 1) };
            var client2 = new Client("2") { Location = new Location(1, 1) };

            var channel1 = new Channel(client1);
            var channel2 = new Channel(client2);

            var channels = new List<Channel>
            {
                channel1,
                channel2
            };

            var foundChannelForUser1 = ChannelCreator.FindChannelFor(client1, channels);
            var foundChannelForUser2 = ChannelCreator.FindChannelFor(client2, channels);

            Assert.That(foundChannelForUser1, Is.EqualTo(foundChannelForUser2));
        }
    }
}