using Org.BouncyCastle.Bcpg;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using System.Windows;

namespace Hackathon
{
    public class ServerData
    {
        public WaterEntrys entrys;
        public double lat { get; set; }
        public double lon { get; set; }
        public int radius {  get; set; }
        public ServerData(double lat, double lon, int radius) 
        {
            this.lat = lat;
            this.lon = lon;
            this.radius = radius;
        }

        public async Task GetWater()
        {
            ServerData data = new ServerData(this.lat, this.lon, this.radius);
            var json = JsonSerializer.Serialize(data);
            var content = new StringContent(json, Encoding.UTF8, "application/json");

            var client = new HttpClient();
            var response = await client.PostAsync("http://10.72.8.47:8000/water/nearby", content);

            string result = await response.Content.ReadAsStringAsync();

            var options = new JsonSerializerOptions
            {
                PropertyNameCaseInsensitive = true
            };
            entrys = JsonSerializer.Deserialize<WaterEntrys>(result, options);
        }
    }
}
