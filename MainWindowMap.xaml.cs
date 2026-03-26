using Mapsui;
using Mapsui.Layers;
using Mapsui.Projections;
using Mapsui.Styles;
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
            mapControl.Map.Widgets.Clear();

            Map map = new Map();
            map.Layers.Add(OpenStreetMap.CreateTileLayer());

            mapControl.Map = map;
            var position = SphericalMercator.FromLonLat(9.7415, 47.4125);
            mapControl.Map.Navigator.OverrideZoomBounds = new MMinMax(10, 1000);
            mapControl.Map.Navigator.CenterOn(position.x, position.y);
            mapControl.Map.Navigator.ZoomTo(10);

            
            
           

        }
    }
}
