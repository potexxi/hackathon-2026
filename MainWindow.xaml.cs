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
using Org.BouncyCastle.Pqc.Crypto.Hqc;
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
using System.Windows.Navigation;
using System.Windows.Shapes;
using static System.Net.Mime.MediaTypeNames;

namespace Hackathon
{
    public partial class MainWindow : Window
    {
        HandelGuide hq;
        public MicrobitController _microbit = new MicrobitController();
        public List<(double, double)> locations = new List<(double, double)>();
        public int next = 0;
        public int punkte = 0;

        public MainWindow()
        {
            InitializeComponent();
            hq = new HandelGuide(StackPanellGuideEntry);
            LoggingWidget.ShowLoggingInMap = ActiveMode.No;
            mapControl.Map.Widgets.Clear();
            Loaded += async (s, e) =>
            {
                await Window_Loaded_Async(s, e);
            };
        }

        private async Task Window_Loaded_Async(object sender, RoutedEventArgs e)
        {
            hq.Load("survival_tips_english.txt");
            await InitializeAsync();
        }

        private void Window_Loaded(object sender, RoutedEventArgs e) { }

        private void Window_SizeChanged(object sender, SizeChangedEventArgs e)
        {
            TabGuide.Width = (GridMain.ActualWidth - 5) / 2;
            TabMap.Width = (GridMain.ActualWidth - 5) / 2;
            StackPannelGuides.Height = GridMain.ActualHeight - 100;
            ScrawllBarGuides.Height = GridMain.ActualHeight - 100;
        }

        private void ShowEntries(List<GuideEntry> entries)
        {
            StackPanellGuideEntry.Children.Clear();
            foreach (GuideEntry entry in entries)
            {
                entry.Width = GridMain.ActualWidth - 120;
                StackPanellGuideEntry.Children.Add(entry);
            }
        }

        private void TextBoxSearch_TextChanged(object sender, TextChangedEventArgs e)
        {
            string search = TextBoxSearch.Text.Trim().ToLower();
            if (string.IsNullOrWhiteSpace(search))
            {
                ShowEntries(hq.seList);
                return;
            }
            List<GuideEntry> filtered = hq.seList
                .Where(x => x.Titel.ToLower().Contains(search))
                .ToList();
            ShowEntries(filtered);
        }

        private async Task InitializeAsync()
        {
            var (user_lat, user_lon) = await Class1.GetCoords();
            int user_radius = 10000;
            ServerData data = new ServerData(user_lat, user_lon, user_radius);
            await data.GetWater();
            punkte = data.entrys.count;
            foreach (WaterEntry entry in data.entrys.results)
            {
                (double, double) tuple = (entry.lat, entry.lon);
                locations.Add(tuple);
            }
            MapInitializen();

            locations.Insert(0, (user_lat, user_lon));

            double bearing = CalculateBearing(locations[0].Item1, locations[0].Item2,
                                              locations[1].Item1, locations[1].Item2);

            _microbit.Disconnect();
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

            var (user_lat, user_lon) = locations[0];
            locations.RemoveAt(0);

            foreach ((double, double) i in locations)
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
            double bearing = CalculateBearing(locations[0].Item2, locations[0].Item1,
                                              locations[next % (punkte - 1)].Item2,
                                              locations[next % (punkte - 1)].Item1);
            next++;           
            _microbit.Disconnect();
            _microbit.Connect("COM16");
            _microbit.SendAngle(bearing);
        }
    }
}