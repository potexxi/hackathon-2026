using System;
using System.Windows;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.IO;

namespace Hackathon
{
    public class HandelGuide
    {
        List<GuideEntry> se;
        StackPanel sp = new StackPanel();

        public HandelGuide(StackPanel stackpannel)
        {
            this.sp = stackpannel;
        }

        public void Load(string path)
        {
            if (File.Exists(path))
            {
                using (StreamReader sr = new StreamReader(path))
                {
                    while (!sr.EndOfStream)
                    {
                        string line = sr.ReadLine();
                        string[] splitted = line.Split("|");
                        GuideEntry se = new GuideEntry(splitted[0], splitted[1], splitted[2]);
                        se.Margin = new Thickness(10, 10, 10, 0);
                        sp.Children.Add(se);
                    }
                }
            }
        }
    }
}
