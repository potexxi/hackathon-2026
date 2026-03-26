using Mapsui;
using Mapsui.Projections;
using Mapsui.Tiling;
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
            // Karte erstellen
            mapControl.Map = new Map();

            // OpenStreetMap Layer hinzufügen
            mapControl.Map.Layers.Add(OpenStreetMap.CreateTileLayer());

            // Startposition (Wien)
            var position = SphericalMercator.FromLonLat(16.3738, 48.2082);
            
            

        }
    }
}
