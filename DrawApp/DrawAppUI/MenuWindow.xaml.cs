using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Interop;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using Xceed.Wpf.Toolkit;

namespace DrawAppUI
{
    public partial class MenuWindow : Window
    {
        List<Button> buttons = new List<Button>();
        PaintWindow _paintWindow;
        public MenuWindow()
        {
            InitializeComponent();
            InitializeButtons();
            _paintWindow = new PaintWindow();
            _paintWindow.ShowInTaskbar = false;


        }
        private void InitializeButtons()
        {
            buttons.Add(btnCursor);
            buttons.Add(btnPen);
            buttons.Add(btnEraser);
            buttons.Add(btnClearAll);
            buttons.Add(btnScreenShot);
            buttons.Add(btnColorPalette);
            foreach (Button button in buttons)
            {
                button.Background = Brushes.Transparent;
            }


            btnCursor.Background = Brushes.Green;
            rbtnThickness_3.IsChecked = true;
        }

        private void ActiveButton(object sender)
        {
            foreach (Button button in buttons)
            {
                button.Background = Brushes.Transparent;
            }
            Button btn = sender as Button;
            btn.Background = Brushes.Green;
            
        }

        private void btnCursor_Click(object sender, RoutedEventArgs e)
        {
            ActiveButton(sender);
            _paintWindow.Hide();
        }

        private void btnPen_Click(object sender, RoutedEventArgs e)
        {
            ActiveButton(sender);
            _paintWindow.isDrawMode = true;
            _paintWindow.isEraseMode = false;
            _paintWindow.Show();
        }

        private void btnEraser_Click(object sender, RoutedEventArgs e)
        {
            ActiveButton(sender);
            _paintWindow.isEraseMode = true;
            _paintWindow.isDrawMode = false;
            _paintWindow.Show();
        }

        private void btnClearAll_Click(object sender, RoutedEventArgs e)
        {
            foreach (var line in _paintWindow.paintSurface.Children.OfType<Polyline>().ToList())
            {
                _paintWindow.paintSurface.Children.Remove(line);
            }
        }

        private void btnExit_Click(object sender, RoutedEventArgs e)
        {
            Application.Current.Shutdown();
            
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            ColorPicker picker = new ColorPicker();
            picker.SelectedColor = PenSettings.currentColorAsColor;
            picker.SelectedColorChanged += Picker_SelectedColorChanged;

            Label label = new Label();
            label.Content = "\nRenk Paleti";

            Button closeButton = new Button();
            closeButton.Content = "Uygula";
            closeButton.Click += CloseButton_Click;

            StackPanel panel = new StackPanel();

            panel.Children.Add(label);
            panel.Children.Add(picker);
            panel.Children.Add(closeButton);

            Window colorPickerWindow = new Window
            {
                Title = "Renk Seç",
                Content = panel,
                SizeToContent = SizeToContent.Manual,
                Height = 90,
                Width = 200,
                ResizeMode = ResizeMode.NoResize,
                WindowStyle = WindowStyle.None,
                Background = Brushes.OrangeRed,
                WindowStartupLocation = System.Windows.WindowStartupLocation.CenterScreen,
      
            };
            
            colorPickerWindow.ShowDialog();
        }

        private void CloseButton_Click(object sender, RoutedEventArgs e)
        {
            StackPanel panel = (StackPanel)((Button)sender).Parent;
            Window window = (Window)panel.Parent;
            window.Close();

        }

        private void Picker_SelectedColorChanged(object sender, RoutedPropertyChangedEventArgs<Color?> e)
        {
            Color? selectedColor = e.NewValue;

            if (selectedColor != null)
            {
                Color color = selectedColor.Value;
                SolidColorBrush brush = new SolidColorBrush(color);
                PenSettings.currentColorAsBrush = brush;
                PenSettings.currentColorAsColor = color;
            }
        }

        #region radiobuttonschecked
        private void rbtnThickness_10_Checked(object sender, RoutedEventArgs e)
        {
            PenSettings.currentStrokeThickness = 10;
        }
        private void rbtnThickness_5_Checked(object sender, RoutedEventArgs e)
        {
            PenSettings.currentStrokeThickness = 5;
        }

        private void rbtnThickness_3_Checked(object sender, RoutedEventArgs e)
        {
            PenSettings.currentStrokeThickness = 3;
        }

        private void rbtnThickness_1_Checked(object sender, RoutedEventArgs e)
        {
            PenSettings.currentStrokeThickness = 1;
        }

        #endregion
        #region ekran fotografı (hard)
        
        #endregion

        private void Window_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            DragMove();
        }

        private void btnGoWebsite_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                Process.Start(new ProcessStartInfo
                {
                    FileName = "chrome.exe", // Chrome'un yolu
                    Arguments = "https://www.denizvarici.com.tr" // Açılacak URL
                });
            }
            catch (Exception)
            {
                System.Windows.MessageBox.Show("Websitemi ziyaret edip benim hakkımda bilgi edinebilirsiniz :)\nDeniz Varıcı");
            }
            
        }
    }
}
