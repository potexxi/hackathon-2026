using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace Hackathon
{
    public class ServerData
    {
        public double Lat { get; set; }
        public double Lon { get; set; }
        public string Name { get; set; }
        public string Id { get; set; }
        public string Watertype { get; set; }
        public ServerData() { }

        public static async Task SendData()
        {
            var data = new ServerData
            {
                Lat = 47.4208,
                Lon = 9.7306
            };

            var json = JsonSerializer.Serialize(data);
            var content = new StringContent(json, Encoding.UTF8, "application/json");

            using var client = new HttpClient();
            var response = await client.PostAsync("http://10.72.8.55:8000/", content);

            string result = await response.Content.ReadAsStringAsync();
            Console.WriteLine(result);
        }

        public static async Task GetData()
        {
            using var client = new HttpClient();
            var response = await client.GetAsync("http://10.72.8.55:8000/data"); // URL anpassen

            response.EnsureSuccessStatusCode(); // wirft bei Fehler

            var json = await response.Content.ReadAsStringAsync();
            var data = JsonSerializer.Deserialize<ServerData>(json);

            Console.WriteLine($"Lat: {data.Lat}, Lon: {data.Lon}");
        }
    }
}
