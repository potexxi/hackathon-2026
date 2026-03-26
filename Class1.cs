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
        public static async Task<(double lat, double lon)> GetCoords()
        {
            using var client = new HttpClient();
            client.DefaultRequestHeaders.Add("User-Agent", "Mozilla/5.0");

            string html = await client.GetStringAsync("https://ipgeolocation.io/what-is-my-ip");

            var htmlDoc = new HtmlDocument();
            htmlDoc.LoadHtml(html);

            var codeBlock = htmlDoc.DocumentNode.SelectSingleNode("//code");

            string jsonText = System.Net.WebUtility.HtmlDecode(codeBlock.InnerText);

            using var data = JsonDocument.Parse(jsonText);
            var location = data.RootElement.GetProperty("location");

            double lat = double.Parse(location.GetProperty("latitude").GetString(),
                                      System.Globalization.CultureInfo.InvariantCulture);
            double lon = double.Parse(location.GetProperty("longitude").GetString(),
                                      System.Globalization.CultureInfo.InvariantCulture);

            return (lat, lon);
        }
    }
}
