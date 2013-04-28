using Subgurim.Controles;
using System;
using System.Drawing;

namespace KataGasolineras.Model
{
    [Serializable]
    public class GMarkerWrapper : GMarker
    {
        public GMarkerWrapper(GLatLng latLng, string text, Color markerColor)
            : base(latLng, CreateRepresentation(text, markerColor))
        {
        }

        /// <summary>
        /// Create the visual style of the marker
        /// </summary>
        /// <param name="text">Text to be show</param>
        /// <param name="markerColor">Primary color of marker</param>
        private static GIcon CreateRepresentation(string text, Color markerColor)
        {
            return new GIcon
                 {
                     flatIconOptions = new FlatIconOptions(32, 32, markerColor, Color.Black, text, Color.White, 15,
                                                           FlatIconOptions.flatIconShapeEnum.roundedrect)
                 };
        }
    }
}