using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using ViewModel;
using LiveCharts;
using LiveCharts.Wpf;
using Image = System.Windows.Controls.Image;
using Point = System.Windows.Point;

namespace View
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            DataContext = new MainViewModel();
        }

        
        private double _zoomValueImage1 = 1.0;
        private double _zoomValueImage2 = 1.0;

        Point _selectedPointSource;
        Point _originSource;
        Point _startSource;

        Point _tempSource;

        Point _selectedPointProcessed;
        Point _originProcessed;
        Point _startProcessed;

        private void Image_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            if (sender is Image image)
            {
                image.CaptureMouse();
                _tempSource = e.GetPosition((IInputElement)e.Source);
            }
            
        }

        private void Image_MouseMove(object sender, MouseEventArgs e)
        {
            if (sender is Image image)
            {
                if (image.IsMouseCaptured)
                {
                    if (image.Name == "SourceImage")
                    {
                        _originSource = e.GetPosition((IInputElement)e.Source);
                        PerformScaling(image, _startSource, _originSource, _zoomValueImage1);
                        _selectedPointSource = new Point();
                    }
                    else if (image.Name == "ProcessedImage")
                    {
                        _originProcessed = e.GetPosition((IInputElement)e.Source);
                        PerformScaling(image, _startProcessed, _originProcessed, _zoomValueImage2);
                        _selectedPointProcessed = new Point();
                    }
                }
            }
        }

        private void PerformScaling(Image image, Point start, Point origin, double zoomValue)
        {
            ScaleTransform scale = new ScaleTransform(zoomValue, zoomValue, start.X, start.Y);
            Vector v = start - _tempSource;
            scale.CenterX = origin.X + v.X;
            scale.CenterY = origin.Y + v.Y;
            if (image.Name == "SourceImage")
            {
                _startSource = new Point(scale.CenterX, scale.CenterY);
            }
            else if (image.Name == "ProcessedImage")
            {
                _startProcessed = new Point(scale.CenterX, scale.CenterY);
            }
            _tempSource = new Point(origin.X, origin.Y);
            image.RenderTransform = scale;
        }


        private void Image_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            if (sender is Image image)
            {
                image.ReleaseMouseCapture();
            }
        }

        private void UIElement_OnMouseWheelImage(object sender, MouseWheelEventArgs e)
        {
            if (sender is Image image)
            {
                if (image.Name == "SourceImage")
                {
                    _startSource = e.GetPosition((IInputElement)e.Source);
                    if (e.Delta > 0)
                    {
                        if (_selectedPointSource.Equals(new Point()))
                        {
                            _selectedPointSource = _startSource;
                        }
                        _startSource = _selectedPointSource;
                        _zoomValueImage1 += 0.2;
                    }
                    else
                    {
                        if (_zoomValueImage1 <= 1)
                        {
                            _selectedPointSource = new Point();
                            _zoomValueImage1 = 1;
                            return;
                        }
                        _zoomValueImage1 -= 0.2;
                    }
                    ScaleTransform scale = new ScaleTransform(_zoomValueImage1, _zoomValueImage1, _startSource.X, _startSource.Y);
                    image.RenderTransform = scale;
                }
                else if (image.Name == "ProcessedImage")
                {
                    _startProcessed = e.GetPosition((IInputElement)e.Source);
                    if (e.Delta > 0)
                    {
                        if (_selectedPointProcessed.Equals(new Point()))
                        {
                            _selectedPointProcessed = _startProcessed;
                        }
                        _startProcessed = _selectedPointProcessed;
                        _zoomValueImage2 += 0.2;
                    }
                    else
                    {
                        if (_zoomValueImage2 <= 1)
                        {
                            _selectedPointProcessed = new Point();
                            _zoomValueImage2 = 1;
                            return;
                        }
                        _zoomValueImage2 -= 0.2;
                    }
                    ScaleTransform scale = new ScaleTransform(_zoomValueImage2, _zoomValueImage2, _startProcessed.X, _startProcessed.Y);
                    image.RenderTransform = scale;
                }
            }
        }


        private void UIElement_OnLostFocus(object sender, RoutedEventArgs e)
        {
            PhaseDataGrid.SelectedItem = null;
        }
    }
}
