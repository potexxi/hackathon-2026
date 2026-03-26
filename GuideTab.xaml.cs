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
using System.Windows.Shapes;
using static System.Net.Mime.MediaTypeNames;

namespace Hackathon
{
    /// <summary>
    /// Interaktionslogik für GuideTab.xaml
    /// </summary>
    public partial class GuideTab : Window
    {
        string Titel2;
        string SubTitel2;
        string Text2;
        public GuideTab()
        {
            InitializeComponent();
        }

        public GuideTab(string titel, string subtitel, string text)
        {
            InitializeComponent();
            this.Titel2 = titel;
            this.SubTitel2 = subtitel;
            this.Text2 = text;

            Titel.Content = Titel2;
            SubTitel.Content = SubTitel2;
            Text.Content = Text2;
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }
    }
}
