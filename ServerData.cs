using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using Renci.SshNet;

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
