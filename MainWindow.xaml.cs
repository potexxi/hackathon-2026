using System.Text;
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
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        MainWindowMap mwm;
        public MainWindow()
        {
            InitializeComponent();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            mwm = new MainWindowMap();
            mwm.ShowDialog();
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
        }

        private void Window_SizeChanged(object sender, SizeChangedEventArgs e)
        {
            TabGuide.Width = (GridMain.ActualWidth - 5) / 2;
            TabMap.Width = (GridMain.ActualWidth - 5) / 2;
            StackPannelGuides.Height = GridMain.ActualHeight - 100;
            ScrawllBarGuides.Height = GridMain.ActualHeight - 100;
        }
    }
}