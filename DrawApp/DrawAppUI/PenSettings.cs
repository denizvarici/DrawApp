using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media;

namespace DrawAppUI
{
    public static class PenSettings
    {
        public static SolidColorBrush currentColorAsBrush = Brushes.White;
        public static Color currentColorAsColor = Colors.White;
        public static int currentStrokeThickness = 3;
    }
}
