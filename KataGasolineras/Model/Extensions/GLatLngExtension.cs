using System;
using Subgurim.Controles;

namespace KataGasolineras.Model.Extensions
{
    public static class GLatLngExtension
    {
        #region Public
        /// <summary>
        /// Generate a random lattitude
        /// </summary>
        /// <param name="latlon">Lat long from where we want create the random lat</param>
        /// <param name="minDistance">Random min distance</param>
        /// <param name="maxDistance">Random max distance</param>
        /// <returns>Random lat</returns>
        public static GLatLng GetRandomLat(this GLatLng latlon, int minDistance, int maxDistance)
        {
            // Get lat degrees
            var latDegrees = (int)latlon.lat;

            // Get lat minutes
            var latMinutes = latlon.lat - latDegrees;

            // Get lat minutes meters equivalence
            var latLenkmUnit = GetLatMinutesMetersUnit(latlon.lat);

            // Generate random distance
            var rDistance = new Random().Next(minDistance, maxDistance);

            // Calculate random distance in minutes
            var latMinutesMin = rDistance / latLenkmUnit / 60;

            // Add random minutes to inital minutes
            var rLatMinutes = latMinutes + latMinutesMin;

            // Calculate new lat
            var newLat = latDegrees + rLatMinutes;

            return new GLatLng(newLat, latlon.lng);
        }

        /// <summary>
        /// Generate a random Longitude
        /// </summary>
        /// <param name="latlon">Lat long from where we want create the random lon</param>
        /// <param name="minDistance">Random min distance</param>
        /// <param name="maxDistance">Random max distance</param>
        /// <returns>Random lon</returns>
        public static GLatLng GetRandomLon(this GLatLng latlon, int minDistance, int maxDistance)
        {
            // Get lon degrees
            var lonDegrees = (int)latlon.lng;

            // Get lon minutes
            var lonMinutes = latlon.lng - lonDegrees;

            // Get lon minutes meters equivalence
            var latLenkmUnit = GetLonMinutesMetersUnit(latlon.lat);

            // Generate random distance
            var rDistance = new Random().Next(minDistance, maxDistance);

            // Calculate random distance in minutes
            var lonMinutesMin = rDistance / latLenkmUnit / 60;
            
            // Add random minutes to inital minutes
            var rLonMinutes = lonMinutes + lonMinutesMin;

            if (double.IsInfinity(lonMinutesMin))
            {
                throw new ArgumentException("Wrong geographical coordinates");
            }

            // Calculate new lon
            var newLon = lonDegrees + rLonMinutes;

            return new GLatLng(latlon.lat, newLon);
        }
        #endregion

        #region Private
        /// <summary>
        /// Hold lattitude minutes equivalence in meters
        /// </summary>
        /// <param name="lat">Lattitude</param>
        /// <returns>Equivalent lattitude meters</returns>
        private static double GetLatMinutesMetersUnit(double lat)
        {
            double latKmUnit;
            var latDegree = (int)lat;

            if (latDegree < 10)
            {
                latKmUnit = 1840;
            }
            else if (latDegree < 20)
            {
                latKmUnit = 1840;
            }
            else if (latDegree < 30)
            {
                latKmUnit = 1850;
            }
            else if (latDegree < 40)
            {
                latKmUnit = 1850;
            }
            else if (latDegree < 50)
            {
                latKmUnit = 1850;
            }
            else if (latDegree < 60)
            {
                latKmUnit = 1850;
            }
            else if (latDegree < 70)
            {
                latKmUnit = 1860;
            }
            else if (latDegree < 80)
            {
                latKmUnit = 1860;
            }
            else if (latDegree < 90)
            {
                latKmUnit = 1860;
            }
            else
            {
                latKmUnit = 1860;
            }

            return latKmUnit;
        }

        /// <summary>
        /// Hold Longitude minutes equivalence in meters
        /// </summary>
        /// <param name="lat">Longitude</param>
        /// <returns>Equivalent Longitude meters</returns>
        private static double GetLonMinutesMetersUnit(double lat)
        {
            double lonKmUnit;
            var latDegree = (int)lat;

            if (latDegree < 10)
            {
                lonKmUnit = 1860;
            }
            else if (latDegree < 20)
            {
                lonKmUnit = 1830;
            }
            else if (latDegree < 30)
            {
                lonKmUnit = 1740;
            }
            else if (latDegree < 40)
            {
                lonKmUnit = 1610;
            }
            else if (latDegree < 50)
            {
                lonKmUnit = 1420;
            }
            else if (latDegree < 60)
            {
                lonKmUnit = 1190;
            }
            else if (latDegree < 70)
            {
                lonKmUnit = 930;
            }
            else if (latDegree < 80)
            {
                lonKmUnit = 640;
            }
            else if (latDegree < 90)
            {
                lonKmUnit = 320;
            }
            else
            {
                lonKmUnit = 0;
            }

            return lonKmUnit;
        }
        #endregion
    }
}