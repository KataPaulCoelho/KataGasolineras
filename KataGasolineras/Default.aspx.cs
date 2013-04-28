using KataGasolineras.Model;
using KataGasolineras.Model.Extensions;
using Subgurim.Controles;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Web.UI;

namespace KataGasolineras
{
    public partial class Default : Page
    {
        #region Fields
        /// <summary>
        /// Contains searched latlon
        /// </summary>
        private GLatLng _initLatLon = new GLatLng(41, -3.2);

        /// <summary>
        /// Hold all gas stations in order to calculate the nearest
        /// </summary>
        private readonly List<GasStation> _gasStations = new List<GasStation>();

        /// <summary>
        /// Random route
        /// </summary>
        private Route _route;

        #endregion

        #region Private
        /// <summary>
        ///     fill map with gas stations, route, ...
        /// </summary>
        private void FillMap()
        {
            CreateRoute();

            CreateGasStations();

            CheckNearestMarker();
        }

        /// <summary>
        ///     Configure GMap properties
        /// </summary>
        private void ConfigureGMap()
        {
            // Map language
            GMapControl.Language = "es";

            // Map backcolor
            GMapControl.BackColor = Color.White;

            // Zoom control
            GMapControl.Add(new GControl(GControl.preBuilt.SmallZoomControl));

            // Add text with coordinate of clicked point
            GMapControl.Add(new GControl(GControl.extraBuilt.TextualOnClickCoordinatesControl));

            // Set center
            GMapControl.setCenter(_initLatLon, 10);
        }

        /// <summary>
        /// Clear route, gas stations, ...
        /// </summary>
        private void Clear()
        {
            // Clear gas stations
            GMapControl.resetMarkerClusterer();

            // Clear gas route markers
            GMapControl.resetMarkers();

            // Clear map route
            GMapControl.resetPolylines();
        }

        /// <summary>
        /// Generate random route
        /// </summary>
        private void CreateRoute()
        {
            var latLon = _initLatLon;
            var previousLatLon = _initLatLon;

            // Get liters
            var liters = double.Parse(TxLiters.Text, System.Globalization.NumberStyles.Any);

            // Get consumption
            var consumption = double.Parse(TxConsumption.Text, System.Globalization.NumberStyles.Any);

            // Route instance
            _route = new Route(new List<GLatLng>(), liters, consumption);

            // Create A point
            GMapControl.Add(new GMarkerWrapper(latLon, "A", Color.DarkMagenta));

            // Add initial point to route
            _route.AddPoint(latLon);

            // Save initial distance in the current latlon
            _route.SaveRemainingDistance(latLon);

            var i = 0;
            do
            {
                // Generate random route changing his lat or lan depending the condition
                latLon = i % 2 == 0 ? latLon.GetRandomLon(20000, 40000) : latLon.GetRandomLat(20000, 40000);

                // Add to points list
                _route.AddPoint(latLon);

                // Get remaining distance in this latlon
                _route.CalculateRemainigDistance(latLon, previousLatLon);

                // Save remaining distance in the current latlon
                _route.SaveRemainingDistance(latLon);

                previousLatLon = latLon;

                i++;
            } while (_initLatLon.distanceFrom(latLon) <= 230000);

            // Create B point
            GMapControl.Add(new GMarkerWrapper(latLon, "B", Color.DarkMagenta));

            // Create route
            GMapControl.Add(_route);
        }

        /// <summary>
        /// Calculate the nearest gas station according the remaining distance
        /// </summary>
        private void CheckNearestMarker()
        {
            // To save route LatLon posible gas stations
            var distancesToGasStation = new Dictionary<GLatLng, Dictionary<GasStation, double>>();

            foreach (var routeLatLon in _route.LatLongs)
            {
                var enoughGas = false;

                distancesToGasStation[routeLatLon.Key] = new Dictionary<GasStation, double>();

                // Check posible gas stations and remaining distance to each
                foreach (var gasStation in _gasStations)
                {
                    var distanceToGasStation = routeLatLon.Key.distanceFrom(gasStation.LatLng);

                    if (!(distanceToGasStation < routeLatLon.Value))
                    {
                        break;
                    }

                    // Save possible gas station
                    distancesToGasStation[routeLatLon.Key].Add(gasStation, distanceToGasStation);

                    enoughGas = true;
                }

                if (!enoughGas)
                {
                    // Get the previous route lat long index
                    var i = _route.LatLongs.Keys.ToList().IndexOf(routeLatLon.Key) - 1;

                    // Check if we have enough gasoline to start
                    if (i == -1)
                    {
                        GMapControl.Add(new GInfoWindow(_route.LatLongs.Keys.First(), "You haven't enough gasoline to start!!!"));
                        return;
                    }

                    // Get the previous route lat long
                    var validRouteLatLong = _route.LatLongs.Keys.ToList()[i];

                    // get the nearest gas station according to the minimum distance to
                    var nearestMarker =
                        distancesToGasStation[validRouteLatLong].Aggregate((l, r) => l.Value < r.Value ? l : r);

                    // Create route to gas station
                    GMapControl.Add(new GPolyline(new List<GLatLng> { validRouteLatLong, nearestMarker.Key.LatLng }, Color.Green, 3));

                    // Show gas station
                    var window = new GInfoWindow(nearestMarker.Key.LatLng,
                                                 string.Format("This is the nearest gas station." +
                                                               "<br/>" +
                                                               "The distance to gas station is: <b>{0} meters</b>",
                                                               nearestMarker.Value));
                    GMapControl.Add(window);

                    break;
                }

                GMapControl.Add(new GInfoWindow(_gasStations.Last().LatLng, "Still has plenty of gas, " +
                                                                       "<br/>" +
                                                                       "you can refuel at the last gas station"));
            }
        }

        /// <summary>
        ///  Generate random gas stations
        /// </summary>
        private void CreateGasStations()
        {
            var latLon = _initLatLon;

            for (var i = 1; i < 11; i++)
            {
                // Generate random gas station changing his lat or lan depending the condition
                latLon = i % 2 == 0 ? latLon.GetRandomLat(5000, 30000) : latLon.GetRandomLon(5000, 30000);

                // Create the gas station
                var gasStation = new GasStation(latLon, string.Format("G{0}", i));

                // Save marker in order to use it later in line creation
                _gasStations.Add(gasStation);
            }

            // Add to map
            GMapControl.markerClusterer = new MarkerClusterer(_gasStations.OfType<GMarker>().ToList());
        }

        private void GetCoordonates()
        {
            // Get searched lat
            var lat = double.Parse(TxLattitude.Text, System.Globalization.NumberStyles.Any);

            // Get searched lon
            var lon = double.Parse(TxLongitude.Text, System.Globalization.NumberStyles.Any);

            // Save in field
            _initLatLon = new GLatLng(lat, lon);

            // center map
            GMapControl.setCenter(_initLatLon);
        }
        #endregion

        #region Protected

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
            {
                ConfigureGMap();

                FillMap();
            }
        }

        /// <summary>
        /// In order to show marker lat lon
        /// </summary>
        /// <param name="s"></param>
        /// <param name="e"></param>
        /// <returns></returns>
        protected string MarkerClick(object s, GAjaxServerEventArgs e)
        {
            return string.Format("alert('{0}')", e.point);
        }

        protected void BFillClick(Object sender,
                                 EventArgs e)
        {
            Clear();

            GetCoordonates();

            FillMap();
        }

        #endregion
    }
}