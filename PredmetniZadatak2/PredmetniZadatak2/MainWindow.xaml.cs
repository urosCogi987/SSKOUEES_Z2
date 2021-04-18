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
using System.Windows.Media.Animation;
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
        private HashSet<PointEntity> pointEntities = new HashSet<PointEntity>();
        private HashSet<LineEntity> lineEntities = new HashSet<LineEntity>();
        private HashSet<SubstationEntity> substationEntities = new HashSet<SubstationEntity>();
        private HashSet<NodeEntity> nodeEntities = new HashSet<NodeEntity>();
        private HashSet<SwitchEntity> switchEntities = new HashSet<SwitchEntity>();
       
        private static Dictionary<long, Point> entitiesOnCanvas = new Dictionary<long, Point>();
        private static List<Shape> coloredNodes = new List<Shape>();

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
            

            progressBar.Dispatcher.Invoke(() => progressBar.Value = 40, System.Windows.Threading.DispatcherPriority.Background);
            progressTextBlock.Text = "40%";
            XMLParser.LoadSubstations(pointEntities, substationEntities, FilePath);
            XMLParser.LoadNodes(pointEntities, nodeEntities, FilePath);
            XMLParser.LoadSwitches(pointEntities, switchEntities, FilePath);
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
            Calculator.CalculateMinMax(pointEntities, out maxLatitude, out minLatitude, out maxLongitude, out minLongitude);

            progressBar.Dispatcher.Invoke(() => progressBar.Value = 100, System.Windows.Threading.DispatcherPriority.Background);
            progressTextBlock.Text = "100%";
        }        

        private void DrawBtn_Click(object sender, RoutedEventArgs e)
        {
            progressBar.Dispatcher.Invoke(() => progressBar.Value = 20, System.Windows.Threading.DispatcherPriority.Background);
            progressTextBlock.Text = "20%";

            Point point = new Point();
            foreach (SubstationEntity entity in substationEntities)
            {
                point = Calculator.GetCoordinates(entity, maxLatitude, minLatitude, maxLongitude, minLongitude, mapCanvas.Width, mapCanvas.Height);
                Painter.DrawPointEntities(entity, entity.Color, point, mapCanvas);
                entitiesOnCanvas.Add(entity.Id, point);
            }
            progressBar.Dispatcher.Invoke(() => progressBar.Value = 40, System.Windows.Threading.DispatcherPriority.Background);
            progressTextBlock.Text = "40%";

            foreach (NodeEntity entity in nodeEntities)
            {
                point = Calculator.GetCoordinates(entity, maxLatitude, minLatitude, maxLongitude, minLongitude, mapCanvas.Width, mapCanvas.Height);
                Painter.DrawPointEntities(entity, entity.Color, point, mapCanvas);
                entitiesOnCanvas.Add(entity.Id, point);
            }
            progressBar.Dispatcher.Invoke(() => progressBar.Value = 60, System.Windows.Threading.DispatcherPriority.Background);
            progressTextBlock.Text = "60%";

            foreach (SwitchEntity entity in switchEntities)
            {
                point = Calculator.GetCoordinates(entity, maxLatitude, minLatitude, maxLongitude, minLongitude, mapCanvas.Width, mapCanvas.Height);
                Painter.DrawPointEntities(entity, entity.Color, point, mapCanvas);
                entitiesOnCanvas.Add(entity.Id, point);
            }
            progressBar.Dispatcher.Invoke(() => progressBar.Value = 80, System.Windows.Threading.DispatcherPriority.Background);
            progressTextBlock.Text = "80%";

            Painter.LineEntitiesCalculations(lineEntities, mapCanvas, entitiesOnCanvas);
            progressBar.Dispatcher.Invoke(() => progressBar.Value = 100, System.Windows.Threading.DispatcherPriority.Background);
            progressTextBlock.Text = "100%";
        }

        private void mapCanvas_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            Shape clickedEntity = e.OriginalSource as Shape;

            if ((clickedEntity != null) && (clickedEntity is Line))
            {                
                string[] toolTip = clickedEntity.ToolTip.ToString().Split('\n');
                double startNodeId = double.Parse((toolTip[3].Split(':'))[1]);
                double endNodeId = double.Parse((toolTip[4].Split(':'))[1]);

                foreach (Shape node in mapCanvas.Children)
                {
                    if (node.GetType().Name.ToString() == "Ellipse")
                    {
                        double nodeId = double.Parse(node.ToolTip.ToString().Split('\n')[1].Split(':')[1]);                        

                        if (nodeId == startNodeId)
                        {
                            node.Fill = Brushes.Purple;
                            coloredNodes.Add(node);
                            AnimateNodes(node);
                        }
                        else if (nodeId == endNodeId)
                        {
                            node.Fill = Brushes.Purple;
                            coloredNodes.Add(node);
                            AnimateNodes(node);
                        }
                    }                    
                }                                   
            }                    
        }

        private void mapCanvas_MouseRightButtonDown(object sender, MouseButtonEventArgs e)
        {            
            foreach (Shape node in coloredNodes)
            {
                string nodeType = node.ToolTip.ToString().Split('\n')[0];

                if (nodeType == "Substation")
                {
                    node.Fill = Brushes.Green;
                }
                else if (nodeType == "Node")
                {
                    node.Fill = Brushes.Blue;
                }
                else if (nodeType == "Switch")
                {
                    node.Fill = Brushes.Orange;
                }                
            }

            coloredNodes.Clear();
        }
        
        private void AnimateNodes(Shape node1)
        {
            DoubleAnimation widthAnimation = new DoubleAnimation
            {
                From = 9,
                To = 90,
                Duration = TimeSpan.FromSeconds(1.5)
            };
            DoubleAnimation heightAnimation = new DoubleAnimation
            {
                From = 9,
                To = 90,
                Duration = TimeSpan.FromSeconds(1.5)
            };

            Storyboard.SetTargetProperty(widthAnimation, new PropertyPath(Ellipse.WidthProperty));
            Storyboard.SetTarget(widthAnimation, node1);

            Storyboard.SetTargetProperty(heightAnimation, new PropertyPath(Ellipse.HeightProperty));
            Storyboard.SetTarget(heightAnimation, node1);

            Storyboard s1 = new Storyboard();
            s1.Children.Add(widthAnimation);
            s1.Children.Add(heightAnimation);

            s1.Completed += (t, r) => StoryboardCompleted(node1);            
            s1.Begin();           
        }

        private void StoryboardCompleted(Shape e)
        {
            DoubleAnimation myDoubleAnimation2 = new DoubleAnimation();
            myDoubleAnimation2.From = 90;
            myDoubleAnimation2.To = 9;
            myDoubleAnimation2.Duration = new Duration(TimeSpan.FromSeconds(1.5));
            e.BeginAnimation(Ellipse.WidthProperty, myDoubleAnimation2);
            e.BeginAnimation(Ellipse.HeightProperty, myDoubleAnimation2);
        }
    }
}
