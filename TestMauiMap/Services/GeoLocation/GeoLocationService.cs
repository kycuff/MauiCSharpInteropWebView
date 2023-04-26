using GeoUK;
using GeoUK.Coordinates;
using GeoUK.Ellipsoids;
using GeoUK.Projections;
using System.Diagnostics.CodeAnalysis;

namespace TestMauiMap.Services.GeoLocation
{
    public class GeoLocationService : IGeoLocationService
    {
        [SuppressMessage("Style", "IDE0046:Convert to conditional expression", Justification = "<Pending>")]
        public async Task<Location> GetGPSPosition()
        {
            Location position = await Geolocation.GetLocationAsync(new GeolocationRequest(GeolocationAccuracy.Default, TimeSpan.FromSeconds(1.5))).ConfigureAwait(false);

            if (position == null)
                throw new FeatureNotEnabledException();

            return position;
        }

        public Location GetDefaultLongitudeLatitude()
        {
            return new Location
            {
                Latitude = 51.481510,
                Longitude = -3.1793253
            };
        }

        public EastingNorthing GetDefaultEastingNorthing(bool checkForAddress = true)
        {
            return new EastingNorthing(318202, 176499);
        }

        public EastingNorthing LonLatToNorthingEasting(LatitudeLongitude lonLat)
        {
            Cartesian cartesian = GeoUK.Convert.ToCartesian(new Wgs84(), lonLat);
            Cartesian bngCartesian = Transform.Etrs89ToOsgb36(cartesian);

            return GeoUK.Convert.ToEastingNorthing(new Airy1830(), new BritishNationalGrid(), bngCartesian);
        }

        public LatitudeLongitude NorthingEastingToLonLat(double easting, double northing)
        {
            Cartesian cartesian = GeoUK.Convert.ToCartesian(new Airy1830(),
                new BritishNationalGrid(),
                new EastingNorthing(easting, northing));

            Cartesian wgsCartesian = Transform.Osgb36ToEtrs89(cartesian);

            return GeoUK.Convert.ToLatitudeLongitude(new Wgs84(), wgsCartesian);
        }
    }
}
