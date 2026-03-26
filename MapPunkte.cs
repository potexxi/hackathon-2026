using Mapsui.Layers;
using Mapsui.Projections;
using Mapsui.Styles;
using Mapsui.UI.Wpf;
using Org.BouncyCastle.Bcpg.Sig;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hackathon
{
    public class MapPunkte
    {
        public double lon = 9.7415;
        public double lat = 47.4125;
        public string color;

        public MapPunkte() {}
        public MapPunkte(double lon, double lat, string color) 
        {
            this.lon = lon;
            this.lat = lat;
            this.color = color;
        }

        public PointFeature PunktErstellen()
        {
            var point = SphericalMercator.FromLonLat(lon, lat);
            PointFeature feature = new PointFeature(point);
            if (color.ToLower() == "red")
            {
                feature.Styles.Add(new SymbolStyle
                {
                    SymbolType = SymbolType.Ellipse,
                    Fill = new Mapsui.Styles.Brush(new Mapsui.Styles.Color(255, 0, 0)),
                    SymbolScale = 0.3,
                    
                });
            }
            else
            {
                feature.Styles.Add(new SymbolStyle
                {
                    SymbolType = SymbolType.Ellipse,
                    Fill = new Mapsui.Styles.Brush(new Mapsui.Styles.Color(0, 0, 255)),
                    SymbolScale = 0.3,
                    
                });
                
            }
            return feature;
        }

    }
}
