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
        GuideTab gt;
        public GuideEntry()
        {
            InitializeComponent();
        }

        private void Rectangle_PreviewMouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            gt = new GuideTab();
            gt.ShowDialog();
        }

        private void Rectangle_PreviewDragEnter(object sender, DragEventArgs e)
        {
            MessageBox.Show("Entry");
        }

        private void Rectangle_PreviewDragLeave(object sender, DragEventArgs e)
        {
            MessageBox.Show("Exit");
        }
    }
}
