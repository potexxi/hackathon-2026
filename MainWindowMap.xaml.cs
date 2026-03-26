using HarfBuzzSharp;
using Mapsui;
using Mapsui.Layers;
using Mapsui.Projections;
using Mapsui.Styles;
using Mapsui.Tiling;
using Mapsui.UI.Wpf;
using Mapsui.Widgets;
using Mapsui.Widgets.InfoWidgets;
using NetTopologySuite.Geometries;
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
        public List<(double, double)> locations = new List<(double, double)>();
        public int next = 1;
        public int punkte = 0;
        public MainWindowMap()
        {
            InitializeComponent();
            LoggingWidget.ShowLoggingInMap = ActiveMode.No;
            mapControl.Map.Widgets.Clear();
<<<<<<< HEAD
            double bearing = CalculateBearing(locations[0].Item2, locations[0].Item1, locations[2].Item2, locations[2].Item1);    
            MicrobitController _microbit = new MicrobitController();         
            _microbit.Connect("COM3");             
            _microbit.SendAngle(bearing); 
=======
            Loaded += async (s, e) => await InitializeAsync();
>>>>>>> 37a7fc0b8f57bc47897aca2dbd5a75eee2828cb7
        }

        private async Task InitializeAsync()
        {
            var (user_lat, user_lon) = await Class1.GetCoords();
            int user_radius = 10000;
            ServerData data = new ServerData(user_lat, user_lon, user_radius);
            await data.GetWater();
            foreach (WaterEntry entry in data.entrys.results)
            {
                (double, double) tuple = (entry.lat, entry.lon);
                locations.Add(tuple);
            }
            MapInitializen();

            locations.Insert(0, (user_lat, user_lon)); // eigene Position als Startpunkt

            double bearing = CalculateBearing(locations[0].Item1, locations[0].Item2,
                                              locations[1].Item1, locations[1].Item2);

            MicrobitController _microbit = new MicrobitController();
            _microbit.Connect("COM16");
            _microbit.SendAngle(bearing);
        }


        public void MapInitializen()
        {
            Map map = new Map();
            map.Layers.Add(OpenStreetMap.CreateTileLayer());

            mapControl.Map = map;
            var position = SphericalMercator.FromLonLat(9.7415, 47.4125);
            mapControl.Map.Navigator.OverrideZoomBounds = new MMinMax(1, 10000);
            mapControl.Map.Navigator.CenterOn(position.x, position.y);
            mapControl.Map.Navigator.ZoomTo(1);
            var features = new List<IFeature>();

            List<MapPunkte> mapPunkte = new List<MapPunkte>();
            var (user_lat, user_lon) = locations[0];
            locations.RemoveAt(0);
            

            foreach ((double,double) i in locations)
            {            
                features.Add(new MapPunkte(i.Item2, i.Item1, "blue").PunktErstellen());
            }
            features.Add(new MapPunkte(user_lon, user_lat, "red").PunktErstellen());



            
            var layer = new MemoryLayer
            {
                Features = features,
                Style = null
            };
            mapControl.Map.Layers.Add(layer);
        }

        public double CalculateBearing(double fromLat, double fromLon, double toLat, double toLon)
        {
            double lat1 = fromLat * Math.PI / 180;
            double lat2 = toLat * Math.PI / 180;
            double dLon = (toLon - fromLon) * Math.PI / 180;

            double x = Math.Sin(dLon) * Math.Cos(lat2);
            double y = Math.Cos(lat1) * Math.Sin(lat2) - Math.Sin(lat1) * Math.Cos(lat2) * Math.Cos(dLon);

            double bearing = Math.Atan2(x, y) * 180 / Math.PI;         
            return (bearing + 360) % 360;
        }

        private void NextButton_Click(object sender, RoutedEventArgs e)
        {
            next++;
            CalculateBearing(locations[0].Item2, locations[0].Item1, locations[next].Item2, locations[next].Item1);
        }
    }
}
