using Subgurim.Controles;
using System;
using System.Collections.Generic;
using System.Drawing;

namespace KataGasolineras.Model
{
    [Serializable]
    public class Route : GPolyline
    {
        #region Properties
        /// <summary>
        /// Hold LatLongs and the remaining distance in each lat lon
        /// </summary>
        public Dictionary<GLatLng, double> LatLongs = new Dictionary<GLatLng, double>();

        /// <summary>
        /// Hold current remaining distance
        /// </summary>
        private double RemainingDistance { get; set; }

        #endregion

        #region Public
        public Route(List<GLatLng> points, double liters, double consumption)
            : base(points, Color.Blue, 4)
        {
            RemainingDistance = CalculateInitialRemainigDistance(liters, consumption);
        }

        public void AddPoint(GLatLng latLon)
        {
            points.Add(latLon);
        }

        /// <summary>
        /// Calculate remaining distance in a point
        /// </summary>
        /// <param name="previous">Previous Lat lon</param>
        /// <param name="current">Current Lat lon</param>
        public void CalculateRemainigDistance(GLatLng previous, GLatLng current)
        {
            var distanceToRest = current.distanceFrom(previous);

            RemainingDistance -= distanceToRest;
        }

        /// <summary>
        /// Save remaining distance for a latlon
        /// </summary>
        /// <param name="latLon"></param>
        public void SaveRemainingDistance(GLatLng latLon)
        {
            LatLongs.Add(latLon, RemainingDistance);
        }

        #endregion

        /// <summary>
        ///     Calculate the remaining distance taking into account the consumption
        /// </summary>
        /// <param name="liters">Gas liters of the vehicle</param>
        /// <param name="consumption">Consumption of the vehicle</param>
        /// <returns>Distance in meters</returns>
        private static double CalculateInitialRemainigDistance(double liters, double consumption)
        {
            return ((liters * 100) / consumption) * 1000;
        }
    }
}