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
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace Hackathon
{
    /// <summary>
    /// Interaktionslogik für GuideEntry.xaml
    /// </summary>
    public partial class GuideEntry : UserControl
    {
        string Titel = "Titel";
        string SubTitel = "SubTitel";
        string Text = "Text";
        GuideTab gt;
        public GuideEntry()
        {
            InitializeComponent();
        }
        public GuideEntry(string titel, string subtitel, string text)
        {
            InitializeComponent();
            this.Titel = titel;
            this.SubTitel = subtitel;
            this.Text = text;
            LabelSubtext.Content = SubTitel;
            LabelTitel.Content = Titel;
        }

        private void Rectangle_MouseLeave(object sender, MouseEventArgs e)
        {
            LabelTitel.Content = new TextBlock
            {
                Text = Titel
            };
        }

        private void Rectangle_MouseEnter(object sender, MouseEventArgs e)
        {

            LabelTitel.Content = new TextBlock
            {
                Text = Titel,
                TextDecorations = TextDecorations.Underline
            };
        }

        private void Rectangle_PreviewMouseDown(object sender, MouseButtonEventArgs e)
        {
            gt = new GuideTab(this.Titel, this.SubTitel, this.Text);
            gt.ShowDialog();
        }
    }
}
