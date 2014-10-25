using NUnit.Framework;
using TeamWin.Propinquity.Web.LocationUtils;

namespace TeamWin.Propinquity.Web.Tests
{
    [TestFixture]
    public class LocationDistanceCalculatorTests
    {
        [Test]
        [TestCase(52.758, -3.259, 
                  53.758, -3.259, 
                  111)]
        [TestCase(53.47506, -2.25121,
                  53.47641, -2.25371,
                  0.22132)]
        public void ShouldBeAbleToCalculateDistance(double lat1, double lon1, double lat2, double lon2, double distance)
        {

            Assert.That(distance, Is.EqualTo(LocationDistanceCalculator.DistanceTo(lat1, lon1, lat2, lon2)).Within(0.2));
        }
    }
}
