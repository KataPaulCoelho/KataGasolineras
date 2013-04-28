using Subgurim.Controles;
using System;
using System.Drawing;

namespace KataGasolineras.Model
{
     [Serializable]
    public class GasStation:GMarkerWrapper
    {
        /// <summary>
        /// Hold latlng
        /// </summary>
        public GLatLng LatLng { get; private set; }

        public GasStation(GLatLng latLng, string text)
            : base(latLng, text, Color.Red)
        {
            LatLng = latLng;
        }
    }
}