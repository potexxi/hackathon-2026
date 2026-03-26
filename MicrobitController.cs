using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO.Ports;

namespace Hackathon
{
    public class MicrobitController
    {
        private SerialPort _port;

        public bool Connect(string portName = "COM3")
        {
            try
            {
                _port = new SerialPort(portName, 115200);
                _port.Open();
                return true;
            }
            catch { return false; }
        }

        public void SendAngle(double angle)
        {
            if (_port?.IsOpen == true)
                _port.WriteLine(((int)angle).ToString());
        }

        public void Disconnect() => _port?.Close();
    }
}
