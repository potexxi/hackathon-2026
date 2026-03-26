using Mapsui;
using Mapsui.Projections;
using Mapsui.Tiling;
using Mapsui.UI.Wpf;
using Mapsui.Widgets;
using Mapsui.Widgets.InfoWidgets;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;


namespace Hackathon
{
    /// <summary>
    /// Interaktionslogik für MainWindowMap.xaml
    /// </summary>
    public partial class MainWindowMap : Window
    {
        public MainWindowMap()
        {
            InitializeComponent();
            LoggingWidget.ShowLoggingInMap = ActiveMode.No;
            Map map = new Map();
            // OSM-Tile-Layer
            map.Layers.Add(OpenStreetMap.CreateTileLayer());

            map.Layers.Add(OpenStreetMap.CreateTileLayer());

            // Map dem Control zuweisen
            mapControl.Map = map;

            // Wien Koordinaten
            var position = SphericalMercator.FromLonLat(9.7415, 47.4125);

            mapControl.Map.Navigator.OverrideZoomBounds = new MMinMax(10, 1000);
            // 👉 WICHTIG: Navigator benutzen (ohne Resolutions!)
            mapControl.Map.Navigator.CenterOn(position.x, position.y);

            // Zoom setzen (z. B. 1000 = näher, 10000000 = weiter raus)
            mapControl.Map.Navigator.ZoomTo(10);



        }
    }
}
