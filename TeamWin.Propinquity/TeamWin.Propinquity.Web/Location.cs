namespace TeamWin.Propinquity.Web
{
    public class Location
    {
        private readonly double _lat;
        private readonly double _lon;

        public Location(double lat, double lon)
        {
            _lat = lat;
            _lon = lon;
        }

        public double Lat
        {
            get { return _lat; }
        }

        public double Lon
        {
            get { return _lon; }
        }

        public double DistanceToInKm(Location other)
        {
            return LocationUtils.LocationDistanceCalculator.DistanceTo(Lat, Lon, other.Lat, other.Lon);}

        public override string ToString()
        {
            return "Lat: " + Lat + ", Lon: " + Lon;
        }
    }
}