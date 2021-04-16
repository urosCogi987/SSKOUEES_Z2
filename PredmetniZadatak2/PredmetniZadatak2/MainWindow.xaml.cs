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

        private HashSet<Point> gridPoints = new HashSet<Point>();

        private double minLatitude, maxLatitude, minLongitude, maxLongitude;

       



        #region onPropertyChanged
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
        #endregion

        public MainWindow()
        {
            InitializeComponent();

            DrawBtn.IsEnabled = false;
        }

        private void LoadBtn_Click(object sender, RoutedEventArgs e)
        {

            progressBar.Dispatcher.Invoke(() => progressBar.Value = 0, System.Windows.Threading.DispatcherPriority.Background);            

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
                DrawBtn.IsEnabled = true;
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
            double localLatitude = 0;
            double localLongitude = 0;
            foreach (SubstationEntity entity in substationEntities)
            {
                LatLonConverter.ToLatLon(entity.PointX, entity.PointY, 34, out localLatitude, out localLongitude);
                entity.Latitude = localLatitude;
                entity.Longitude = localLongitude;
            }
            foreach (NodeEntity entity in nodeEntities)
            {
                LatLonConverter.ToLatLon(entity.PointX, entity.PointY, 34, out localLatitude, out localLongitude);
                entity.Latitude = localLatitude;
                entity.Longitude = localLongitude;
            }
            foreach (SwitchEntity entity in switchEntities)
            {
                LatLonConverter.ToLatLon(entity.PointX, entity.PointY, 34, out localLatitude, out localLongitude);
                entity.Latitude = localLatitude;
                entity.Longitude = localLongitude;
            }            

            progressBar.Dispatcher.Invoke(() => progressBar.Value = 80, System.Windows.Threading.DispatcherPriority.Background);
            progressTextBlock.Text = "80%";
            Calculator.CalculateMinMax(pointEntities, maxLatitude, minLatitude, maxLongitude, minLongitude);

            progressBar.Dispatcher.Invoke(() => progressBar.Value = 100, System.Windows.Threading.DispatcherPriority.Background);
            progressTextBlock.Text = "100%";
        }

        private void LoadGrid()
        {
            Point p;
            HashSet<Point> points = new HashSet<Point>();

            int iovi = 1000;
            int jotovi = 1000;

            for (int i = 0; i < iovi; i = i + 3) 
            {
                for (int j = 0; j < jotovi; j = j + 2) 
                {
                    p = new Point(i, j);
                    points.Add(p);
                }
            }

            gridPoints = points;
        }

        private void DrawBtn_Click(object sender, RoutedEventArgs e)
        {
            progressBar.Dispatcher.Invoke(() => progressBar.Value = 20, System.Windows.Threading.DispatcherPriority.Background);
            progressTextBlock.Text = "33%";

            foreach (SubstationEntity entity in substationEntities)
            {

            }

            progressBar.Dispatcher.Invoke(() => progressBar.Value = 66, System.Windows.Threading.DispatcherPriority.Background);
            progressTextBlock.Text = "66%";

            foreach (NodeEntity entity in nodeEntities)
            {

            }

            progressBar.Dispatcher.Invoke(() => progressBar.Value = 90, System.Windows.Threading.DispatcherPriority.Background);
            progressTextBlock.Text = "90%";

            foreach (SwitchEntity entity in switchEntities)
            {

            }

            //double valOfSingleLongitude = (maxLongitude - minLongitude) / 1000;
            //double valOfSingleLatitude = (maxLatitude - minLatitude) / 1000;

            //double x = Math.Round(longi)
        }        
    }
}
