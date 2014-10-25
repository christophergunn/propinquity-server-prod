using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NUnit.Framework;
using TeamWin.Propinquity.Web.LocationUtils;

namespace TeamWin.Propinquity.Web.Tests
{
    [TestFixture]
    public class ChannelChangingAlgorithmTests
    {
        [Test]
        public void NoData_ReturnsNoMovements()
        {
            var alg = new ChannelChangingAlgorithm();

            var oldState = new List<Channel>
                {

                };
            var newState = new List<Channel>
                {

                };

            var movements = alg.FindMovements(oldState, newState);

            Assert.That(movements.Count, Is.EqualTo(0));
        }

        [Test]
        public void Tbc()
        {
            var alg = new ChannelChangingAlgorithm();

            var oldState = new List<Channel>
            {
                new Channel(new Client("1") { Location = new Location(1, 1) })
            };
            var newState = new List<Channel>
            {
                new Channel(new Client("1") { Location = new Location(1, 100) })
            };

            var movements = alg.FindMovements(oldState, newState);

            Assert.That(movements.Count, Is.EqualTo(0));
        }
    }

    public class ChannelChangingAlgorithm
    {
        public IList<UserMovement> FindMovements(IList<Channel> oldState, IList<Channel> newState)
        {
            return new List<UserMovement>();
        }
    }

    public class UserMovement
    {

    }
}
