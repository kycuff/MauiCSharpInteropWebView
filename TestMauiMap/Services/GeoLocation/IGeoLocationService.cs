using GeoUK.Coordinates;

namespace TestMauiMap.Services.GeoLocation
{
    public interface IGeoLocationService
    {
        /// <summary>
        /// Returns the GPS position
        /// </summary>
        /// <exception cref="FeatureNotSupportedException">Device doesn't have GPS</exception>
        /// <exception cref="FeatureNotEnabledException">GPS turned off</exception>
        /// <exception cref="PermissionException">Permission not granted to use GPS</exception>
        Task<Location> GetGPSPosition();

        /// <summary>
        /// Returns the default Longitude/Latitude position from AppSettings
        /// </summary>
        Location GetDefaultLongitudeLatitude();

        /// <summary>
        /// Returns the default Easting/ Northing position from AppSettings
        /// </summary>
        EastingNorthing GetDefaultEastingNorthing(bool checkForAddress = true);

        /// <summary>
        /// Converts longitude/ latitude to easting/ northing
        /// </summary>
        /// <param name="lonLat"></param>
        EastingNorthing LonLatToNorthingEasting(LatitudeLongitude lonLat);

        /// <summary>
        /// Converts easting/ northing to longitude/ latitude
        /// </summary>
        LatitudeLongitude NorthingEastingToLonLat(double easting, double northing);
    }
}
