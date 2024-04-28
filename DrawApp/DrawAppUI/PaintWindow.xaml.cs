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
        private bool isFullScreen = false;
        public PaintWindow()
        {
            InitializeComponent();
            InitializeScreen();
        }

        private void InitializeScreen()
        {
            if (!isFullScreen)
            {
                WindowState = WindowState.Maximized;
                ResizeMode = ResizeMode.NoResize;
                
            }
            isFullScreen = !isFullScreen;
        }


        #region çizim ve silme işlemleri
        public Polyline currentPolyline;
        public bool isEraseMode = false;
        public bool isDrawMode = false;
        private void Canvas_MouseDown(object sender, MouseButtonEventArgs e)
        {
            if (!isEraseMode && e.LeftButton == MouseButtonState.Pressed && isDrawMode)
            {
                currentPolyline = new Polyline();
                currentPolyline.Stroke = PenSettings.currentColorAsBrush;
                currentPolyline.StrokeThickness = PenSettings.currentStrokeThickness;
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
            if (!isEraseMode && e.LeftButton == MouseButtonState.Pressed && currentPolyline != null && isDrawMode)
            {
                Point currentPoint = e.GetPosition(paintSurface);
                currentPolyline.Points.Add(currentPoint);
            }
        }
        
        #endregion
    }
}
        


