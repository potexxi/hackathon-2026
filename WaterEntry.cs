using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hackathon
{
    public class WaterEntry
    {
        public long id {  get; set; }
        public string name { get; set; }
        public string wasser_typ {  get; set; }
        public string ist_trinkwasser { get; set; }
        public double lat {  get; set; }
        public double lon { get; set; }
        public double distanz_meter {  get; set; }
        public WaterEntry() { }
    }
}
