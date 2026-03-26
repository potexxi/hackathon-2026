using Renci.SshNet;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Text.Json;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Threading.Tasks;
using Geolocation;
using Windows.Devices.Geolocation;

namespace Hackathon
{
    public class ServerData
    {
        public string Id {  get; private set; }
        public string Name { get; private set; }
        public string Watertype { get; private set; }
        public double Lat {  get; private set; }
        public double Lon { get; private set; }
        public ServerData() { }

        public class GeoIp
        {
            public double latitude { get; set; }
            public double longitude { get; set; }
        }

        async public static void SendData()
        {
            //var client = new HttpClient();
            //var json = await client.GetStringAsync("https://ipapi.co/json/");
            //var data = JsonSerializer.Deserialize<GeoIp>(json);

            var accessStatus = await Geolocator.RequestAccessAsync();

            if (accessStatus == GeolocationAccessStatus.Allowed)
            {
                var geolocator = new Geolocator { DesiredAccuracyInMeters = 10 };
                Geoposition pos = await geolocator.GetGeopositionAsync();

                Console.WriteLine($"Latitude: {pos.Coordinate.Point.Position.Latitude}");
                Console.WriteLine($"Longitude: {pos.Coordinate.Point.Position.Longitude}");
            }
            else
            {
                Console.WriteLine("Kein Zugriff auf Standort erlaubt.");
            }
        }

        public static ServerData GetData()
        {
            string host = "potexxi.duckdns.org";
            int port = 10220;
            string username = "sftpuser";
            string password = "password";

            string remoteDir = "/files";
            string localDir = @"C:\hackathon-2026\data";

            using (var sftp = new SftpClient(host, port, username, password))
            {
                sftp.Connect();

                var files = sftp.ListDirectory(remoteDir);

                foreach (var file in files)
                {
                    if (!file.Name.StartsWith("."))
                    {
                        string localFile = Path.Combine(localDir, file.Name);

                        using (var fs = new FileStream(localFile, FileMode.Create))
                        {
                            sftp.DownloadFile(file.FullName, fs);
                        }
                    }
                }

                sftp.Disconnect();
            }
            using (StreamReader reader = new StreamReader(localDir))
            {
                return JsonSerializer.Deserialize<ServerData>(reader.ReadToEnd());
            }
        }
    }
}
