using PredmetniZadatak2.Controlers;
using PredmetniZadatak2.Models;
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

namespace PredmetniZadatak2
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private double pointX, pointY;
        private List<PointEntity> pointEntities = new List<PointEntity>();
        private List<LineEntity> lineEntities = new List<LineEntity>();

        public MainWindow()
        {
            InitializeComponent();
        }

        private void LoadBtn_Click(object sender, RoutedEventArgs e)
        {
            LoadBtn.IsEnabled = false;

            progressBar.Dispatcher.Invoke(() => progressBar.Value = 20, System.Windows.Threading.DispatcherPriority.Background);
            progressTextBlock.Text = "20%";
            LoadGrid();

            progressBar.Dispatcher.Invoke(() => progressBar.Value = 40, System.Windows.Threading.DispatcherPriority.Background);
            progressTextBlock.Text = "40%";
            XMLParser.LoadSubstations(pointEntities, pointX, pointY);
            XMLParser.LoadNodes();            
            XMLParser.LoadSwitches();

            progressBar.Dispatcher.Invoke(() => progressBar.Value = 60, System.Windows.Threading.DispatcherPriority.Background);
            progressTextBlock.Text = "60%";
            DrawElements();

            progressBar.Dispatcher.Invoke(() => progressBar.Value = 80, System.Windows.Threading.DispatcherPriority.Background);
            progressTextBlock.Text = "80%";

            progressBar.Dispatcher.Invoke(() => progressBar.Value = 100, System.Windows.Threading.DispatcherPriority.Background);
            progressTextBlock.Text = "100%";
        }

        private void LoadGrid()
        {

        }

        private void DrawElements()
        {

        }
    }
}
