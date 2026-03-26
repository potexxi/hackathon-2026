using HtmlAgilityPack;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace Hackathon
{
    internal class Class1
    {
        public static async Task<Tuple<double, double>> GetCoords()
        {
            using var client = new HttpClient();
            client.DefaultRequestHeaders.Add("User-Agent", "Mozilla/5.0");

            string html = await client.GetStringAsync("https://ipgeolocation.io/what-is-my-ip");

            var htmlDoc = new HtmlDocument();
            htmlDoc.LoadHtml(html);

            var codeBlock = htmlDoc.DocumentNode.SelectSingleNode("//code");
            string jsonText = codeBlock.InnerText;

            using var data = JsonDocument.Parse(jsonText);
            var root = data.RootElement;

            double lat = double.Parse(root.GetProperty("location").GetProperty("latitude").ToString());
            double lon = double.Parse(root.GetProperty("location").GetProperty("longitude").ToString());

            Tuple<double, double> returning = new Tuple<double, double>(lon, lat);
            return returning;
        }
    }
}
