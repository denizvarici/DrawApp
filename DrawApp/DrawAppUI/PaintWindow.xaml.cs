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

namespace DrawAppUI
{
    public partial class PaintWindow : Window
    { 
        public PaintWindow()
        {
            InitializeComponent();
        }


        #region çizim ve silme işlemleri
        private Polyline currentPolyline;
        private bool isEraseMode = false;
        private void Canvas_MouseDown(object sender, MouseButtonEventArgs e)
        {
            if (!isEraseMode && e.LeftButton == MouseButtonState.Pressed)
            {
                currentPolyline = new Polyline();
                currentPolyline.Stroke = new SolidColorBrush(Colors.AliceBlue);
                currentPolyline.StrokeThickness = 3;

                Point startPoint = e.GetPosition(paintSurface);
                currentPolyline.Points.Add(startPoint);
                currentPolyline.MouseMove += CurrentPolyline_MouseMove;
                paintSurface.Children.Add(currentPolyline);
            }
        }

        private void CurrentPolyline_MouseMove(object sender, MouseEventArgs e)
        {
            if (isEraseMode && e.LeftButton == MouseButtonState.Pressed)
            {
                Polyline clickedLine = sender as Polyline;
                if (clickedLine != null)
                {
                    paintSurface.Children.Remove(clickedLine);
                }
            }
        }


        private void Canvas_MouseMove(object sender, MouseEventArgs e)
        {
            if (!isEraseMode && e.LeftButton == MouseButtonState.Pressed && currentPolyline != null)
            {
                Point currentPoint = e.GetPosition(paintSurface);
                currentPolyline.Points.Add(currentPoint);
            }
        }
        private void EraserButton_Click(object sender, RoutedEventArgs e)
        {
            isEraseMode = !isEraseMode;
        }

        private void btnClearAll_Click(object sender, RoutedEventArgs e)
        {
            foreach (var line in paintSurface.Children.OfType<Polyline>().ToList())
            {
                paintSurface.Children.Remove(line);
            }
        }
        #endregion
    }
}
        


