using Microsoft.Win32;
using PredmetniZadatak2.Controlers;
using PredmetniZadatak2.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel;
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
    public partial class MainWindow : Window, INotifyPropertyChanged
    {
        private double pointX, pointY;
        private HashSet<PointEntity> pointEntities = new HashSet<PointEntity>();
        private HashSet<LineEntity> lineEntities = new HashSet<LineEntity>();
        private HashSet<SubstationEntity> substationEntities = new HashSet<SubstationEntity>();
        private HashSet<NodeEntity> nodeEntities = new HashSet<NodeEntity>();
        private HashSet<SwitchEntity> switchEntities = new HashSet<SwitchEntity>();

        private string filePath;         
        public string FilePath
        {
            get { return filePath; }
            set {
                if (value != filePath)
                {
                    filePath = value;
                    OnPropertyChanged("FilePath");
                }
            }
        }        

        public event PropertyChangedEventHandler PropertyChanged;
        public void OnPropertyChanged(String propertyName)
        {
            if (PropertyChanged != null)
                PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
        }


        public MainWindow()
        {
            InitializeComponent();
        }

        private void LoadBtn_Click(object sender, RoutedEventArgs e)
        {

            progressBar.Dispatcher.Invoke(() => progressBar.Value = 0, System.Windows.Threading.DispatcherPriority.Background);
            //progressTextBlock.Text = "20%";

            OpenFileDialog ofd = new OpenFileDialog();
            ofd.Title = "Find file";
            ofd.Filter = "XML Document|*.xml";

            if (ofd.ShowDialog() == true)
            {
                var onlyFileName = System.IO.Path.GetFileName(ofd.FileName);
                FilePath = onlyFileName;
            }

            if (FilePath != null)
            {
                LoadBtn.IsEnabled = false;
            }

            progressBar.Dispatcher.Invoke(() => progressBar.Value = 20, System.Windows.Threading.DispatcherPriority.Background);
            progressTextBlock.Text = "20%";
            LoadGrid();

            progressBar.Dispatcher.Invoke(() => progressBar.Value = 40, System.Windows.Threading.DispatcherPriority.Background);
            progressTextBlock.Text = "40%";
            XMLParser.LoadSubstations(pointEntities, substationEntities, pointX, pointY, FilePath);
            XMLParser.LoadNodes(pointEntities, nodeEntities, pointX, pointY, FilePath);
            XMLParser.LoadSwitches(pointEntities, switchEntities, pointX, pointY, FilePath);
            XMLParser.LoadLines(lineEntities, FilePath);

            progressBar.Dispatcher.Invoke(() => progressBar.Value = 60, System.Windows.Threading.DispatcherPriority.Background);
            progressTextBlock.Text = "60%";
            

            progressBar.Dispatcher.Invoke(() => progressBar.Value = 80, System.Windows.Threading.DispatcherPriority.Background);
            progressTextBlock.Text = "80%";

            progressBar.Dispatcher.Invoke(() => progressBar.Value = 100, System.Windows.Threading.DispatcherPriority.Background);
            progressTextBlock.Text = "100%";
        }

        private void LoadGrid()
        {

        }

        private void DrawBtn_Click(object sender, RoutedEventArgs e)
        {

        }        
    }
}
